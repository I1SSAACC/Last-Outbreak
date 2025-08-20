using UnityEngine;

namespace JUTPS.Utilities
{
    [AddComponentMenu(Constants.ComponentMenuNames.ShootTargetMovement)]
    public class ShotTargetMover : MonoBehaviour
    {
        private const float DetectDistance = 0.5f;

        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private float _heightOffset = 0.5f;

        private bool _isRightMovement;

        private void Update()
        {
            Vector3 origin = transform.position + transform.up * _heightOffset;

            if (Physics.Raycast(origin, transform.right, DetectDistance) || Physics.Raycast(origin, -transform.right, DetectDistance))
                _isRightMovement = !_isRightMovement;

            float deltaSpeed = _speed * Time.deltaTime;

            if (_isRightMovement == true)
                transform.Translate(deltaSpeed, 0, 0);
            else
                transform.Translate(-deltaSpeed, 0, 0);
        }
    }
}