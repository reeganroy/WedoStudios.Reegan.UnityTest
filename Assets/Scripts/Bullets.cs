using UnityEngine;

namespace WedoStudios.Reegan.UnityTest
{
    public class Bullets : MonoBehaviour
    {
        public float Range = 10f;
        public float Damage = 5f;
        Ray ShootRay;
        RaycastHit ShootHit;
        LayerMask ShootableLayerMask;
        LineRenderer BulletLine;

        void Awake()
        {
            ShootableLayerMask = LayerMask.GetMask("Shootable");
            BulletLine = GetComponent<LineRenderer>();

            ShootRay.origin = transform.position;
            ShootRay.direction = transform.forward;
            BulletLine.SetPosition(0, transform.position);

            if (Physics.Raycast(ShootRay, out ShootHit, Range, ShootableLayerMask))
            {
                if (ShootHit.collider.tag == "Enemy")
                {
                    Enemy enemy = ShootHit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ApplyDamage(10);
                    }
                }

                BulletLine.SetPosition(1, ShootHit.point);
            }
            else
            {
                BulletLine.SetPosition(1, (ShootRay.origin + (ShootRay.direction * Range)));
            }
        }
    }
}
