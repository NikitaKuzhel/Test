using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _defaultCornerOffset;
        [SerializeField] private Vector3 _defaultRotationOffset;
        [SerializeField] private Vector3 _followOffset;
        [SerializeField] private Vector3 _followRotationOffset;
        [SerializeField] private float _transitionDuration = 1f;
        [SerializeField] private float _followSmoothTime = 0.1f;
    
        private Transform _target;
        private Tween _transitionTween;
        private Vector3 _currentVelocity;
        private bool _isFollowing = false;

        public void InitTarget(Transform target)
        {
            _target = target;
        }
    
        public void SetDefaultPosition()
        {
            _transitionTween?.Kill();
            _isFollowing = false;
    
            transform.position = _target.position + _defaultCornerOffset;
            transform.rotation = Quaternion.Euler(_defaultRotationOffset);
        }
    
        public void StartAnimation(System.Action onComplete = null)
        {
            _transitionTween?.Kill();
            _isFollowing = false;
    
            Vector3 targetPos = _target.position + _followOffset;
            Quaternion targetRot = Quaternion.Euler(_followRotationOffset);
    
            _transitionTween = DOTween.Sequence()
                .Append(transform.DOMove(targetPos, _transitionDuration).SetEase(Ease.InOutSine))
                .Join(transform.DORotateQuaternion(targetRot, _transitionDuration).SetEase(Ease.InOutSine))
                .OnComplete(() =>
                {
                    _isFollowing = true;
                    onComplete?.Invoke();
                });
        }
        
        private void LateUpdate()
        {
            if (_isFollowing && _target != null)
            {
                transform.position  = _target.position + _followOffset;
            }
        }
    }
}