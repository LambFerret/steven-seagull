using UnityEngine;
using DG.Tweening;

namespace effect
{
    public class ShakerBehavior : MonoBehaviour
    {
        public float duration;
        public Ease ease;
        public int amount;
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            var i = transform.DORotate(new Vector3(0, 0, -amount), 0);
            var a = transform.DORotate(new Vector3(0, 0, amount), duration).SetEase(ease);
            var b = transform.DORotate(new Vector3(0, 0, -amount), duration).SetEase(ease);
            sequence.Append(i);
            sequence.Append(a);
            sequence.Append(b);
            sequence.SetLoops(-1);
        }
    }
}