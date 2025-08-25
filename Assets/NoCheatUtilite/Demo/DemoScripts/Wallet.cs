using System;
using UnityEngine;

namespace NoCheatUtilite.Demo
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private HackingAttemptWarning _hackingAttemptWarning;
        [SerializeField] private NoCheatInt _balance = 0;

        public event Action<int> Changed;

        public int Balance => _balance;

        public void Increase(int amount)
        {
            _balance += amount;
            HandleChange();
        }

        public void Reduce(int amount)
        {
            _balance -= amount;
            HandleChange();
        }

        private void HandleChange()
        {
            if (_balance.IsHack)
                _hackingAttemptWarning.Show();

            Changed?.Invoke(_balance);
        }
    }
}