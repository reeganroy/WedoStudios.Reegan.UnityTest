using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WedoStudios.Reegan.UnityTest
{
    public class ClientObjectPool : MonoBehaviourSingletonPersistent<ClientObjectPool>
    {
        public GameObject enemyPrefab;
        public Transform enemyHolder;

        [SerializeField] private Image playerDamageScreenImage;
        [SerializeField] private Image enemyDamageScreenImage;
        private Color FlashColor = new Color(255f, 255f, 255f, 1f);

        private Coroutine PlayerColourCouroutine;
        private Coroutine enemyColourCouroutine;

        private EnemyObjectPool _pool;

        public override void Awake()
        {
            base.Awake(); 
        }

        void Start()
        {
            _pool = gameObject.AddComponent<EnemyObjectPool>();

            StartCoroutine(SpawnEnemyAfterSomeTimeInFirstTime());
        }

        private IEnumerator SpawnEnemyAfterSomeTimeInFirstTime()
        {
            yield return new WaitForSeconds(2.5f);

            _pool.Spawn();
        }

        public void SpawnEnemy()
        {
            _pool.Spawn();
        }

        public void PlayerGetsDamaged()
        {
            playerDamageScreenImage.color = FlashColor;
            StopPlayerColourLerperRoutine();

            PlayerColourCouroutine = StartCoroutine(ColourLerper(playerDamageScreenImage));
        }

        public void EnemyGetsDamaged()
        {
            enemyDamageScreenImage.color = FlashColor;
            StopEnemyColourLerperRoutine();

            enemyColourCouroutine = StartCoroutine(ColourLerper(enemyDamageScreenImage));
        }

        private IEnumerator ColourLerper(Image image)
        {
            float ElapsedTime = 0.0f;
            float TotalTime = 0.35f;
            while (ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                image.color = Color.Lerp(image.color, Color.clear, ElapsedTime / TotalTime);
                yield return null;
            }
        }

        private void StopPlayerColourLerperRoutine()
        {
            if (PlayerColourCouroutine != null)
            {
                StopCoroutine(PlayerColourCouroutine);
                PlayerColourCouroutine = null;
            }
        }

        private void StopEnemyColourLerperRoutine()
        {
            if (enemyColourCouroutine != null)
            {
                StopCoroutine(enemyColourCouroutine);
                enemyColourCouroutine = null;
            }
        }
    }

    public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
    where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;

                // Use below line if it is necessary
                //DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}