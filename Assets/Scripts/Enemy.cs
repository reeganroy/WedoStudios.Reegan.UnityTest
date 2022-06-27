using UnityEngine;
using UnityEngine.Pool;

namespace WedoStudios.Reegan.UnityTest
{
    public class Enemy : HealthSystem
    {
        public IObjectPool<Enemy> Pool { get; set; }

        void Awake()
        {
            CurrentHealth = maxHealth;  
        }

        void OnEnable()
        {
            AttackPlayer();
            CharacterEventBus.Subscribe(CharacterEventType.ENEMY_DEAD, EnemyDeadCallback);
            CharacterEventBus.Subscribe(CharacterEventType.ENEMY_DAMAGED, EnemyDamagedCallback);
        }

        private void OnDisable()
        {
            ResetDrone();
            CharacterEventBus.Unsubscribe(CharacterEventType.ENEMY_DEAD, EnemyDeadCallback);
            CharacterEventBus.Unsubscribe(CharacterEventType.ENEMY_DAMAGED, EnemyDamagedCallback);
        }

        private void ReturnToPool()
        {
            Pool.Release(this);
        }

        private void ResetDrone()
        {
            CurrentHealth = maxHealth;
        }

        public void AttackPlayer()
        {
            Debug.Log("New Enemy Spawned");
        }

        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);

            CharacterEventBus.Publish(CharacterEventType.ENEMY_DAMAGED);
        }

        protected override void Die()
        {
            CharacterEventBus.Publish(CharacterEventType.ENEMY_DEAD);

            ReturnToPool();
        }

        private void EnemyDeadCallback()
        {
            Debug.Log("Enemy Dead Callback");

            ClientObjectPool.Instance.SpawnEnemy();
        }

        private void EnemyDamagedCallback()
        {
            Debug.Log("Enemy Damaged Callback");

            ClientObjectPool.Instance.EnemyGetsDamaged();
        }
    }
}