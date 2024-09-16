using UnityEngine;

namespace RedApple.ThePit
{

    public class RollingStone : MonoBehaviour
    {
        private float speed = 5;

        void Update()
        {
            transform.Rotate(-1 * speed, 0, 0);
        }
    }
}
