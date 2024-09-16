using UnityEngine;


namespace RedApple.ThePit
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private RoadMovement roadMovement;

        internal PoolObjectType obstacleType;
        private GameObject obstacle;
        private PoolObjectType roadType;

        internal void Init(PoolObjectType _roadType)
        {
            roadType = _roadType;
            ObstacleSpawnInRoad();
        }

        private void ObstacleSpawnInRoad()
        {
            obstacleType = DifferentObstacleType(roadType);
            obstacle = ObjectPool.OnFetchingFromPool(obstacleType);
            obstacle.SetActive(true);
            obstacle.transform.SetParent(gameObject.transform);
            obstacle.transform.localPosition = ObstacleSpawnPosition(obstacleType);

            roadMovement.currentObstacle = obstacle;
            roadMovement.obstacleType = obstacleType;
        }

        private PoolObjectType DifferentObstacleType(PoolObjectType _currentRoadType)
        {
            if (_currentRoadType == PoolObjectType.Road1)
            {
                return (PoolObjectType)Random.Range(3, 14);
            }
            else if (_currentRoadType == PoolObjectType.Road2)
            {
                return (PoolObjectType)Random.Range(3, 6);
            }
            else
            {
                return (PoolObjectType)Random.Range(3, 6);
            }
        }

        private Vector3 ObstacleSpawnPosition(PoolObjectType _obsType)
        {
            switch (_obsType)
            {
                case PoolObjectType.Spike1:
                    return new Vector3(-1.8f, transform.position.y, Random.Range(20, 24));

                case PoolObjectType.Spike2:
                    return new Vector3(-1.8f, transform.position.y, 11);

                case PoolObjectType.Spike3:
                    return new Vector3(-1.8f, transform.position.y, Random.Range(24, 28));

                case PoolObjectType.BigStone:
                    return new Vector3(transform.position.x, 1f, 25);

                case PoolObjectType.Door:
                    return new Vector3(transform.position.x, 10, 17);

                case PoolObjectType.Door3:
                    return new Vector3(transform.position.x, 10, Random.Range(15, 28));

                case PoolObjectType.Blade:
                    return new Vector3(.7f, 1f, 22.5f);

                case PoolObjectType.Blade3:
                    return new Vector3(-.75f, transform.position.y, 22f);

                case PoolObjectType.SlidingBox:
                    return new Vector3(-1, .178f, Random.Range(16, 28));

                case PoolObjectType.FloorSpike:
                    return new Vector3(1.18f, .65f, Random.Range(16, 28));

                case PoolObjectType.Pipe:
                    return new Vector3(2f, transform.position.y, Random.Range(15, 18));

                default:
                    return default;
            }
        }
    }
}
