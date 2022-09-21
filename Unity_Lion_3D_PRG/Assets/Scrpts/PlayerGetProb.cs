using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace agi
{
    public class PlayerGetProb : MonoBehaviour
    {
        ObjectPoolSystem objectPool;

        private void Awake()
        {
            objectPool = FindObjectOfType<ObjectPoolSystem>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.tag.Contains("ItemPool")) objectPool.RelaseObj(hit.gameObject);
        }
    }
}
