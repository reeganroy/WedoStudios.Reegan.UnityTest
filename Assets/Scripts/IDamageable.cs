namespace WedoStudios.Reegan.UnityTest
{
    public interface IDamageable
    {
        int CurrentHealth { get; set; }
        void ApplyDamage(int damage);
    }
}
