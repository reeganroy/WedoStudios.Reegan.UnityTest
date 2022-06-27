using UnityEngine;
using UnityEngine.UI;

namespace WedoStudios.Reegan.UnityTest
{
    public abstract class HealthSystem : MonoBehaviour, IDamageable
    {
        public int maxHealth = 100;

        public int CurrentHealth
        {
            get;
            set;
        }

        public Slider healthSlider;

        public virtual void ApplyDamage(int damage)
        {
            if (CurrentHealth > 0)
                CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
            healthSlider.value = CurrentHealth;
        }

        protected abstract void Die();
    }
}