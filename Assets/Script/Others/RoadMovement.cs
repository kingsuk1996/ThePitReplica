using System.Collections.Generic;
using UnityEngine;

namespace RedApple.ThePit
{
    public class RoadMovement : MonoBehaviour
    {
        internal GameObject currentRoad;
        internal GameObject currentCoin;
        internal GameObject currentObstacle;

        internal PoolObjectType roadType;
        internal PoolObjectType coinType;
        internal PoolObjectType obstacleType;


        void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameState.NotStart || GameManager.Instance.CurrentGameState == GameState.Instruction || GameManager.Instance.CurrentGameState == GameState.Start)
            {
                MoveForward();
                ReturnRoadToPool();
            }
        }

        private void MoveForward()
        {
            transform.Translate(-Vector3.forward * 10 * Time.deltaTime);
        }

        private void ReturnRoadToPool()
        {
            if (transform.position.z <= -45)
            {
                if (currentRoad != null)
                {
                    ObjectPool.OnReturningToPool?.Invoke(currentRoad, roadType);
                }
                
                if (currentCoin != null)
                {
                    ObjectPool.OnReturningToPool?.Invoke(currentCoin, coinType);
                }
            }

            if (transform.position.z <= -44)
            {
                if (currentObstacle != null)
                {
                    ObjectPool.OnReturningToPool?.Invoke(currentObstacle, obstacleType);
                }
            }
        }
    }
}
