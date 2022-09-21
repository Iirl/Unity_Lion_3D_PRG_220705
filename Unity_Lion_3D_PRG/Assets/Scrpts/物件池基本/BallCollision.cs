using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class BallCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.name.Equals(name)) Destroy(gameObject);
        }
    }
}