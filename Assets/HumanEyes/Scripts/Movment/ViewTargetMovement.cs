using UnityEngine;

namespace HumanEyes.Scripts.Movment
{
    public class ViewTargetMovement : MonoBehaviour
    {
        [SerializeField] private Transform head;
    
        private Transform _thisObjectTransform;
    
        private void Awake()
        {
            _thisObjectTransform = transform;
        }
    
        public void MoveTarget(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Transform hitObject = hitInfo.transform;
                _thisObjectTransform.position = hitInfo.point;
                _thisObjectTransform.SetParent(hitObject);
            }
            else
            {
                _thisObjectTransform.position = ray.origin + ray.direction * 100;
                _thisObjectTransform.SetParent(null);
            }
        }
    
        public void Reset()
        {
            _thisObjectTransform.position = head.position + head.forward * 100f;
            _thisObjectTransform.SetParent(null);
        }
    }
}


