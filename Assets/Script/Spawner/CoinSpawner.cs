using UnityEngine;

namespace RedApple.ThePit
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private RoadMovement roadMovement;

        internal void Init(PoolObjectType _obstacleType, PoolObjectType _roadType)
        {
            CoinSpawnInRoad(_obstacleType, _roadType);
        }

        private void CoinSpawnInRoad(PoolObjectType _obstacleType, PoolObjectType _roadType)
        {
            PoolObjectType _coinType = CoinChunkSpawn(_obstacleType, _roadType);
            GameObject _tempCoin = ObjectPool.OnFetchingFromPool(_coinType);

            _tempCoin.SetActive(true);
            _tempCoin.transform.SetParent(gameObject.transform);
            _tempCoin.transform.position = transform.position;

            roadMovement.currentCoin = _tempCoin;
            roadMovement.coinType = _coinType;
        }

        private PoolObjectType CoinChunkSpawn(PoolObjectType _obstacleType, PoolObjectType _roadType)
        {
            if ((_roadType == PoolObjectType.Road1) && (_obstacleType == PoolObjectType.Spike1 || _obstacleType == PoolObjectType.Spike2 || _obstacleType == PoolObjectType.Spike3 || _obstacleType == PoolObjectType.FloorSpike || _obstacleType == PoolObjectType.Pipe))
            {
                return (PoolObjectType)Random.Range(14, 16);
            }
            else
                return PoolObjectType.none;
        }
    }
}
