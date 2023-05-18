using UnityEngine;
using DG.Tweening;

namespace HumanEyes.Scripts.Eyes
{
    public class EyesRotation : MonoBehaviour
    {
        [SerializeField] private EyesDepthOfField eyesDepthOfField;

        [SerializeField] private Transform leftEye, rightEye, crosshairCameraTransform;
        [SerializeField] private Transform body;

        [SerializeField] private float eyeLookAtDuration = 0.5f;

        [HideInInspector] public bool IsRelaxed = false;

        public void RotateEyes(Vector3 lastPos, Vector3 nextPos)
        {
            float duration = 0;
            float lastPosDistance = Vector3.Distance(crosshairCameraTransform.position, lastPos);
            float nextPosDistance = Vector3.Distance(crosshairCameraTransform.position, nextPos);
            if (Mathf.Abs(lastPosDistance - nextPosDistance) >= 2f) duration = eyeLookAtDuration;

            leftEye.DOLookAt(nextPos, duration).SetEase(Ease.OutQuint);
            rightEye.DOLookAt(nextPos, duration).SetEase(Ease.OutQuint);
            crosshairCameraTransform.DOLookAt(nextPos, duration).SetEase(Ease.OutQuint);

            body.eulerAngles = new Vector3(0, crosshairCameraTransform.eulerAngles.y, 0);

            ChangeFocusDistance(nextPos);
        }

        private void ChangeFocusDistance(Vector3 focusPosition)
        {
            float nextFocusDistanceValue = Vector3.Distance(crosshairCameraTransform.position, focusPosition);
            eyesDepthOfField.ChangeFocusDistance(nextFocusDistanceValue);
        }
    }

}

