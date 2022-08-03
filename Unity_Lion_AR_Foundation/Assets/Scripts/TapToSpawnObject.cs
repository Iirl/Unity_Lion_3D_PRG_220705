using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


namespace agi
{
    /// <summary>
    /// 點擊就會出現物件系統
    /// </summary>
    /// 
    [RequireComponent(typeof(ARRaycastManager))]
    public class TapToSpawnObject : MonoBehaviour
    {

        [SerializeField, Header("要生成的物件")]
        GameObject target;
        private ARRaycastManager ARm;
        private Vector2 touchPoint;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void TapAndSpawn()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                touchPoint = Input.mousePosition;
                if (ARm.Raycast(touchPoint, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    GameObject temp = Instantiate(target, pose.position, Quaternion.identity);
                    Vector3 cameraPos = transform.position;
                    cameraPos.y = temp.transform.position.y;
                    temp.transform.LookAt(cameraPos);
                }
            }
        }

        private void Awake()
        {
            ARm = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            TapAndSpawn();
        }
    }
}
