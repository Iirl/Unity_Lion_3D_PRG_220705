using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class SpawnBall : MonoBehaviour
    {
        [SerializeField]
        GameObject ball;

        private void Awake()
        {
            InvokeRepeating("Spawn", 0, 0.1f);
        }

        private void Spawn()
        {
            Vector3 v3;
            v3.x = Random.Range(-24, 24);
            v3.y = Random.Range(0, 20);
            v3.z = Random.Range(-20, 20);

            Instantiate(ball, v3, Quaternion.identity);

        }
    }
}