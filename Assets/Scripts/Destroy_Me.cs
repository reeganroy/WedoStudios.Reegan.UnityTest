using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WedoStudios.Reegan.UnityTest
{
    public class Destroy_Me : MonoBehaviour
    {
        public float AliveTime = 0.1f;

        void Awake()
        {
            Destroy(gameObject, AliveTime);
        }
    }
}
