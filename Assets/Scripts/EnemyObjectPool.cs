using UnityEngine;
using UnityEngine.Pool;

namespace WedoStudios.Reegan.UnityTest
{
    public class EnemyObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;
        private ClientObjectPool clientObjectPool;

        private void Awake()
        {
            clientObjectPool = transform.GetComponent<ClientObjectPool>();
        }

        public IObjectPool<Enemy> Pool
        {
            get
            {
                if (_pool == null)
                    _pool =
                        new ObjectPool<Enemy>(
                            CreatedPooledItem,
                            OnTakeFromPool,
                            OnReturnedToPool,
                            OnDestroyPoolObject,
                            true,
                            stackDefaultCapacity,
                            maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<Enemy> _pool;

        private Enemy CreatedPooledItem()
        {
            //var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var go = Instantiate(clientObjectPool.enemyPrefab, new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 90f, 0f)), clientObjectPool.enemyHolder);

            Enemy enemy = go.GetComponent<Enemy>();

            go.name = "Enemy";
            enemy.Pool = Pool;

            return enemy;
        }

        private void OnReturnedToPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        public void Spawn()
        {
            //var amount = Random.Range(1, 10);
            var amount = 1;

            for (int i = 0; i < amount; ++i)
            {
                var enemy = Pool.Get();

                enemy.transform.localPosition = new Vector3(Random.Range(0, 10), enemy.transform.GetComponent<CapsuleCollider>().height * 0.5f, 0);
            }
        }
    }
}