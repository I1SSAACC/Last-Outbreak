using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace NoCheatUtilite
{
    [Serializable]
    public struct NoCheatInt : ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] private int _encryptedValue;
        [SerializeField, HideInInspector] private int _encryptionKey;
        [SerializeField, HideInInspector] private byte[] _signature;

        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        private const int Dummy = 0;
        private bool _isHack;

        public NoCheatInt(int value)
        {
            _encryptionKey = GenerateKey();
            _encryptedValue = value ^ _encryptionKey;
            _signature = ComputeSignature(_encryptedValue, _encryptionKey);
            _isHack = false;
        }

        public int Value
        {
            get
            {
                if (VerifySignature(_encryptedValue, _encryptionKey, _signature) == false)
                    _isHack = true;

                return _encryptedValue ^ _encryptionKey;
            }
            set
            {
                _encryptionKey = GenerateKey();
                _encryptedValue = value ^ _encryptionKey;
                _signature = ComputeSignature(_encryptedValue, _encryptionKey);
            }
        }

        public bool IsHack => _isHack;

        public static implicit operator int(NoCheatInt s) => s.Value;

        public static implicit operator NoCheatInt(int i) => new(i);

        public void OnBeforeSerialize() =>
            _signature = ComputeSignature(_encryptedValue, _encryptionKey);

        public void OnAfterDeserialize()
        {
            if (VerifySignature(_encryptedValue, _encryptionKey, _signature) == false)
                _isHack = true;
        }

        public override string ToString() => Value.ToString();

        private static int GenerateKey()
        {
            Span<byte> buf = stackalloc byte[4];
            Rng.GetBytes(buf);
            return BitConverter.ToInt32(buf);
        }

        private static byte[] ComputeSignature(int encrypted, int key)
        {
            using var hmac = new HMACSHA256(BitConverter.GetBytes(key));
            return hmac.ComputeHash(BitConverter.GetBytes(encrypted));
        }

        private static bool VerifySignature(int encrypted, int key, byte[] signature)
        {
            if (signature == null || signature.Length == 0)
                return false;

            byte[] expected = ComputeSignature(encrypted, key);
            return expected.SequenceEqual(signature);
        }
    }
}