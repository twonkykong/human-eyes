using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

namespace HumanEyes.Scripts.Eyes
{
    public class EyesDepthOfField : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume postProcessVolume;
        [SerializeField] private float duration;

        private DepthOfField _depthOfField;

        private void Start()
        {
            postProcessVolume.profile.TryGetSettings(out _depthOfField);
        }

        public void ChangeFocusDistance(float value)
        {
            DOTween.To(() => _depthOfField.focusDistance.value, x => _depthOfField.focusDistance.value = x, value, duration);
        }
    }
 
}

