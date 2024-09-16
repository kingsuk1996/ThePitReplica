using UnityEngine;

namespace RedApple.ThePit
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private Transform lastRoadPos;
        [SerializeField] private PlayerController playerController;

        private float zOffset = 30;


        private void RoadSpawn()
        {
            int random = Random.Range(1, 101);

            PoolObjectType _roadType = RoadType(GameManager.Instance.CurrentGameState, random);
            GameObject _tempRoad = ObjectPool.OnFetchingFromPool(_roadType);
            _tempRoad.SetActive(true);

            _tempRoad.transform.position = new Vector3(lastRoadPos.position.x, lastRoadPos.position.y, lastRoadPos.position.z + zOffset);
            lastRoadPos = _tempRoad.transform;

            RoadMovement _roadMovement = _tempRoad.GetComponent<RoadMovement>();
            _roadMovement.currentRoad = _tempRoad;
            _roadMovement.roadType = _roadType;

           /* _tempRoad.TryGetComponent(out GetChild _getChild);
            if (_getChild != null)
            {
                playerController.Spike = _getChild.Spike;
                playerController.Door = _getChild.Door;
                playerController.floorSpike = _getChild.floorSpike;
            }*/

            if (GameManager.Instance.CurrentGameState == GameState.Start)
            {
                ObstacleSpawner _obstacleSpawner = _tempRoad.GetComponent<ObstacleSpawner>();
                CoinSpawner _coinSpawner = _tempRoad.GetComponent<CoinSpawner>();

                _obstacleSpawner.Init(_roadType);
                _coinSpawner.Init(_obstacleSpawner.obstacleType, _roadType);
            }
        }
       
        private PoolObjectType RoadType(GameState _gameState, int _random)
        {
            switch (_gameState)
            {
                case GameState.NotStart:
                    return PoolObjectType.Road1;

                case GameState.Instruction:
                    return PoolObjectType.Road1;

                   /* if (GameManager.Instance.Once)
                    {
                        GameManager.Instance.Once = false;
                        return PoolObjectType.InstructionRoad;
                    }
                    else
                    {
                        return PoolObjectType.Road1;
                    }*/

                case GameState.Start:
                    if (_random >= 1 && _random <= 80)
                    {
                        return PoolObjectType.Road1;
                    }
                    else if (_random >= 81 && _random <= 90)
                    {
                        return PoolObjectType.Road2;
                    }
                    else
                    {
                        return PoolObjectType.Road3;
                    }

                default:
                    return default;
            }
        }

        private void OnEnable()
        {
            PlayerController.OnRoadSpawn += RoadSpawn;
        }

        private void OnDisable()
        {
            PlayerController.OnRoadSpawn -= RoadSpawn;
        }
    }
}
