using DG.Tweening;
using UnityEngine;

namespace RedApple.ThePit
{
    public class ObstacleBehaviour : MonoBehaviour, IObstacleInteraction
    {
        [SerializeField] private PoolObjectType obstacleType;


        public void InteractWithObstacle()
        {
            switch (obstacleType)
            {
                case PoolObjectType.BigStone:
                    transform.DOMoveZ(-18f, 4f);
                    transform.DORotate(new Vector3(-360, 0, 0), .01f , RotateMode.LocalAxisAdd).SetLoops(-1);
                    break;

                case PoolObjectType.Door:
                    transform.DOMoveY(11.5f, 1.5f);
                    break;

                case PoolObjectType.Door3:
                    transform.DOMoveY(11.5f, 1f);
                    break;

                case PoolObjectType.Blade:
                    transform.DOMoveZ(-20, 5f);
                    transform.DORotate(new Vector3(0, 360, 0), .01f, RotateMode.LocalAxisAdd).SetLoops(-1);
                    break;

                case PoolObjectType.Blade3:
                    transform.DOLocalMoveZ(-15, 5f);
                    transform.DORotate(new Vector3(0, -360, 0), .01f, RotateMode.LocalAxisAdd).SetLoops(-1);
                    break;

                case PoolObjectType.SlidingBox:
                    transform.GetChild(0).DOLocalMoveX(1.55f, 2);
                    transform.GetChild(1).DOLocalMoveX(.5f, 2);
                    break;
            }
        }

    }
}
