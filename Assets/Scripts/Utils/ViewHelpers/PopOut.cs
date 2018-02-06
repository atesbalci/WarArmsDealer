using DG.Tweening;
using UnityEngine;

namespace Utils.ViewHelpers
{
    public class PopOut : MonoBehaviour
    {
        private Tweener _curTween;

        private void OnEnable()
        {
            if (_curTween != null)
            {
                _curTween.Kill();
            }
            transform.localScale = Vector3.zero;
            _curTween = transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuart);
        }
    }
}
