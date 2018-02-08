using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.Views
{
    public enum CameraSpotType
    {
        Initial, Design, Research, Sale
    }

    public class CameraView : ViewBase
    {
        //Warning disabled since Unity editor sets these variables
        [Serializable]
        private class CameraSpot
        {
#pragma warning disable 649
            public CameraSpotType SpotType;
            public Transform Transform;
#pragma warning restore 649
        }
        [SerializeField]
        private CameraSpot[] _spotValues;
        
        private Sequence _tweenSequence;

        public void Init()
        {
            var init = _spotValues.First(x => x.SpotType == CameraSpotType.Initial).Transform;
            transform.position = init.position;
            transform.rotation = init.rotation;
        }

        public void Move(CameraSpotType spot, Action completeCallback = null, Action killCallback = null)
        {
            if (_tweenSequence != null)
                _tweenSequence.Kill();
            if (completeCallback == null)
                completeCallback = () => { };
            if (killCallback == null)
                killCallback = () => { };

            var init = _spotValues.First(x => x.SpotType == CameraSpotType.Initial).Transform;
            var target = _spotValues.First(x => x.SpotType == spot).Transform;
            _tweenSequence = DOTween.Sequence();
            _tweenSequence.Append(transform.DOMove(init.position, 0.5f));
            _tweenSequence.Append(transform.DORotateQuaternion(target.rotation, 0.5f));
            _tweenSequence.Append(transform.DOMove(target.position, 0.5f));
            _tweenSequence.onComplete = () => completeCallback();
            _tweenSequence.onKill = () => killCallback();
        }
    }
}
