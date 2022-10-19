using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace agi
{
    public class CheckPoint : MonoBehaviour
    {
        public GameObject CheckPointPrefab;


        public void DisableNPC() => DisalbeChar();

        private void DisalbeChar()
        {
            if (CheckPointPrefab.activeInHierarchy) gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
