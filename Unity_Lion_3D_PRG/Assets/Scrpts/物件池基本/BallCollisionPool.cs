using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class BallCollisionPool : MonoBehaviour
    {
        public delegate void DelgBall(GameObject ball);
        public DelgBall ballOnHit;

        private void OnCollisionEnter(Collision collision)
        {            
            if (!collision.gameObject.name.Equals(name)) ballOnHit(gameObject);
        }
    }
}