using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace HumanEyes.Scripts.Head
{
    public class HeadRotation : MonoBehaviour
    {
        [SerializeField] private Transform viewTargetPoint;
        [SerializeField] private Transform crosshairCameraTransform;
    
        [SerializeField] private float rotationDistance;
        [SerializeField] private float headResetTime = 10f;
        [SerializeField] private float rotationDuration = 0.5f;
    
        private Transform _thisObjectTransform;
        private Coroutine _resetHeadCoroutine;
    
        private void Awake()
        {
            _thisObjectTransform = transform;
        }
    
        private void Update()
        {
            RotateHeadCheck();
        }
    
        private void RotateHeadCheck()
        {
            Vector3 eyesLookPos = crosshairCameraTransform.forward * 100;
            Vector3 headLookPos = _thisObjectTransform.forward * 100;
    
            if (Vector3.Distance(eyesLookPos, headLookPos) >= rotationDistance)
            {
                RotateHead();
            }
        }
    
        private void RotateHead()
        {
            if (Vector3.Distance(crosshairCameraTransform.position, viewTargetPoint.position) < 2f) return;
            _thisObjectTransform.DORotate(crosshairCameraTransform.eulerAngles, rotationDuration);
        }
    
        public void StartResetTimer()
        {
            if (_resetHeadCoroutine != null)
            {
                StopCoroutine(_resetHeadCoroutine);
                _resetHeadCoroutine = null;
            }
    
            _resetHeadCoroutine = StartCoroutine(ResetHead());
        }
    
        private IEnumerator ResetHead()
        {
            yield return new WaitForSeconds(headResetTime);
            RotateHead();
        }
    }

}


