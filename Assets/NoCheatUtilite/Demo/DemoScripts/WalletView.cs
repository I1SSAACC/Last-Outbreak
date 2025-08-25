using TMPro;
using UnityEngine;

namespace NoCheatUtilite.Demo
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        
        private TextMeshProUGUI _balance;

        private void Awake() =>
            _balance = GetComponent<TextMeshProUGUI>();

        private void Start() =>
            _balance.text = _wallet.Balance.ToString();

        private void OnEnable() =>
            _wallet.Changed += OnBalanceChanged;

        private void OnDisable() =>
            _wallet.Changed -= OnBalanceChanged;

        private void OnBalanceChanged(int newBalance) =>
            _balance.text = newBalance.ToString();
    }
}