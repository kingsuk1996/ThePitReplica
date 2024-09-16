using UnityEngine;
using DG.Tweening;

namespace RedApple.ThePit
{
    public class CoinBehaviour : MonoBehaviour
    {
        private Vector3 currentPosition;

        private void OnEnable()
        {
            currentPosition = this.gameObject.transform.localPosition;
        }

        void Update()
        {
            transform.Rotate(0, 0, -1 * 10);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                gameObject.transform.DOLocalMove(new Vector3(7, 5, -6), .2f);
            }
        }

        private void OnDisable()
        {
            transform.DOKill();
            this.gameObject.transform.localPosition = currentPosition;
        }
    }
}
