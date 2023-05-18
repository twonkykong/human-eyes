using UnityEngine;

namespace HumanEyes.Scripts.Eyes
{
    public class EyesMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Transform _thisObjectTransform;

        private void Awake()
        {
            _thisObjectTransform = transform;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _thisObjectTransform.position = target.position;
        }
    }
}


