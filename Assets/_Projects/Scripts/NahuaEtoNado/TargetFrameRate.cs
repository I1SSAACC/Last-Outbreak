using UnityEngine;

namespace JUTPS.Utilities
{
    public class TargetFrameRate : MonoBehaviour
    {
        [SerializeField] private int _targetFPS = -1;
        
        private void Start() =>
            Application.targetFrameRate = _targetFPS;
    }
}