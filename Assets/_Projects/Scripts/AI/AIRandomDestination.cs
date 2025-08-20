using UnityEngine;

namespace JUTPS.AI
{
    [RequireComponent(typeof(JUCharacterArtificialInteligenceBrain))]
    public class AIRandomDestination : MonoBehaviour
    {
        [SerializeField] private CustomRange _time = new(3, 10);
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _area = 100;

        private JUCharacterArtificialInteligenceBrain _brain;
        private float _remainingTime;

        private void Awake() =>
            _brain = GetComponent<JUCharacterArtificialInteligenceBrain>();

        private void Update()
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                _remainingTime = _time.RandomValue;
                _brain.Destination = GetNewRandomPosition();
            }
        }

        private Vector3 GetNewRandomPosition() =>
            _offset + new Vector3(Random.Range(-_area, _area), 0, Random.Range(-_area, _area));
    }
}