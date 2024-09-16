using UnityEngine;

namespace RedApple.ThePit
{
    public class OnTriggerWithObstacle : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                if (other.TryGetComponent(out IObstacleInteraction _interactable))
                {
                    _interactable.InteractWithObstacle();
                }
            }
        }
    }
}
