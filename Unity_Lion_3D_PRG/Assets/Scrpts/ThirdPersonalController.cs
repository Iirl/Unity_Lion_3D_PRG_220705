using UnityEngine;


namespace agi
{
    /// <summary>
    /// 第三人稱控制器
    /// </summary>
    public class ThirdPersonalController : MonoBehaviour
    {
        #region Data
        [SerializeField, Header("Move Speed"), Range(0, 50)]
        private float moveSpd = 3.5f;
        [SerializeField, Header("Spine Speed"), Range(0, 50)]
        private float spineSpd = 5f;
        [SerializeField, Header("Jump Speed"), Range(0, 50)]
        private float jumpSpd = 7f;
        private Animator ani;
        private CharacterController ccller;
        private Transform tfCamera;
        private Vector3 direction;

        #endregion

        #region Event
        private void Awake()
        {
            ani = GetComponent<Animator>();
            ccller = GetComponent<CharacterController>();
            tfCamera = GameObject.Find("Main Camera").transform;
        }
        private void Update()
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetAxis("Fire1") != 0) ani.SetTrigger("getHurt");
        }
        #endregion

        #region Method
        private void Move(float x, float z)
        {
            direction.x = x ;
            direction.z = z ;
            ani.SetFloat("isRunning", (x != 0) ? Mathf.Abs( x) : (z != 0) ? Mathf.Abs(z) : 0);
            ccller.Move(direction * moveSpd * Time.deltaTime);
            // 跟著攝影機轉
            transform.rotation = Quaternion.Lerp(transform.rotation, tfCamera.rotation, spineSpd);
        }
        private void Jump()
        {

        }
        #endregion
    }
}