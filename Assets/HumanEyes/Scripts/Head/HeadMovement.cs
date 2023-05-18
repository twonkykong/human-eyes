using UnityEngine;

namespace HumanEyes.Scripts.Head
{
    public class HeadMovement : MonoBehaviour
    {
        [SerializeField] private Transform headTarget;
        private Transform _thisObjectTransform;

        private void Awake()
        {
            _thisObjectTransform = transform;
        }

        private void Update()
        {
            MoveHead();
        }

        private void MoveHead()
        {
            _thisObjectTransform.position = headTarget.position;
        }
    }
}


