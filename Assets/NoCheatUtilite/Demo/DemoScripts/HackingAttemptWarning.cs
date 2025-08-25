using UnityEngine;

namespace NoCheatUtilite.Demo
{
    public class HackingAttemptWarning : MonoBehaviour
    {
        public void Show() =>
            gameObject.SetActive(true);
    }
}