using UnityEngine;

namespace WedoStudios.Reegan.UnityTest
{
    public class Player_Controller : MonoBehaviour
    {
        public float RunSpeed = 0.0f;
        float GroundCheckRadius = 0.2f;
        public Transform GroundCheck;
        public float JumpHeight;
        bool IsGrounded = false;
        bool FacingRight = true;
        public LayerMask GroundLayer;
        Rigidbody RB;

        void Start()
        {
            RB = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);

            float Move = Input.GetAxis("Horizontal");

            if (IsGrounded && Input.GetButtonDown("Jump"))
            {
                IsGrounded = false;
                RB.velocity = new Vector3(0, JumpHeight, 0);
            }

            RB.velocity = new Vector3(Move * RunSpeed, RB.velocity.y, 0);

            if (Move > 0 && !FacingRight) Flip();
            else if (Move < 0 && FacingRight) Flip();
        }

        void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 Scales = transform.localScale;
            Scales.z *= -1;
            transform.localScale = Scales;
        }

        public int GetFacing()
        {
            if (FacingRight)
                return 1;
            else
                return -1;
        }
    }
}
