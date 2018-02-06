using DG.Tweening;
using UnityEngine;

namespace Utils.ViewHelpers
{
    public class PanelGroupElement : MonoBehaviour
    {
        private Vector3 _defaultPos;
        private Tweener _tweener;
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    if (_tweener != null)
                    {
                        _tweener.Kill();
                    }
                    _tweener = transform.DOLocalMoveX(value ? _defaultPos.x : _defaultPos.x + 2000f, 0.5f);
                }
                _active = value;
            }
        }

        private void Start()
        {
            _defaultPos = transform.localPosition;
            _active = true;
            Active = false;
            _tweener.Kill(true);
        }

        public void Toggle()
        {
            Active = !Active;
        }
    }
}
