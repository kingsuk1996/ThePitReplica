using UnityEngine;
using DG.Tweening;

namespace RedApple.ThePit
{
    public class ResetPosition : MonoBehaviour
    {
        private Vector3 currentPosition;

        private void OnEnable()
        {
            currentPosition = this.gameObject.transform.localPosition;
        }

        private void OnDisable()
        {
            transform.DOKill();
            this.gameObject.transform.localPosition = currentPosition;
        }
    }
}
