using UnityEngine;

namespace WedoStudios.Reegan.UnityTest
{
    public class Player : HealthSystem
    {
        void Awake()
        {
            CurrentHealth = maxHealth;
        }

        private void OnEnable()
        {
            CharacterEventBus.Subscribe(CharacterEventType.PLAYER_DEAD, PlayerDeadCallback);
            CharacterEventBus.Subscribe(CharacterEventType.PLAYER_DAMAGED, PlayerDamagedCallback);
        }

        private void OnDestroy()
        {
            CharacterEventBus.Unsubscribe(CharacterEventType.PLAYER_DEAD, PlayerDeadCallback);
            CharacterEventBus.Unsubscribe(CharacterEventType.PLAYER_DAMAGED, PlayerDamagedCallback);
        }

        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);

            CharacterEventBus.Publish(CharacterEventType.PLAYER_DAMAGED);
        }

        protected override void Die()
        {
            CharacterEventBus.Publish(CharacterEventType.PLAYER_DEAD);

            Destroy(this.gameObject);
            Debug.Log("Player Dead");
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                ApplyDamage(2);
            }
        }

        void OnCollisionStay(Collision col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                ApplyDamage(1);
            }
        }

        private void PlayerDeadCallback()
        {
            Debug.Log("Player Dead Callback");

            UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        private void PlayerDamagedCallback()
        {
            Debug.Log("Player Damaged Callback");

            ClientObjectPool.Instance.PlayerGetsDamaged();
        }
    }
}
