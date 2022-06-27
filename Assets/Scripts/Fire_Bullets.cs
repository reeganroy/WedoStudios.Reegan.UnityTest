using UnityEngine;

namespace WedoStudios.Reegan.UnityTest
{
    public class Fire_Bullets : MonoBehaviour
    {
        public float BulletFiringInterval = 0.15f;
        public GameObject Bullet;
        float NextBulletTime;

        public int MaxRounds;
        public int StartingRounds;
        private int RemainingRounds;


        void Awake()
        {
            NextBulletTime = 0f;
            RemainingRounds = StartingRounds;
        }

        void Update()
        {
            if(RemainingRounds <= 0)
            {
                Reload();
                return;
            }

            Player_Controller PC = transform.root.GetComponent<Player_Controller>();

            if (Input.GetAxisRaw("Fire1") > 0 && NextBulletTime < Time.time && RemainingRounds > 0)
            {
                NextBulletTime = Time.time + BulletFiringInterval;
                Vector3 Rot;
                if (PC.GetFacing() == -1)
                {
                    Rot = new Vector3(0, -90, 0);
                }
                else
                {
                    Rot = new Vector3(0, 90, 0);
                }

                Instantiate(Bullet, transform.position, Quaternion.Euler(Rot));

                RemainingRounds -= 1;
            }
        }

        public void Reload()
        {
            RemainingRounds = MaxRounds;
        }
    }
}