using UnityEngine;
using UnityEngine.UI;

namespace NoCheatUtilite.Demo
{
    [RequireComponent(typeof(Button))]
    public class ButtonIncreaseBalance : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private int _amount;

        private Button _button;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClick);

        private void OnClick() =>
            _wallet.Increase(_amount);
    }
}