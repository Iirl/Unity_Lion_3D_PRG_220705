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
        [SerializeField, Header("Jump Speed"), Range(1f, 15f)]
        private float jumpSpd = 2.4f;
        private Animator ani;
        private CharacterController ccller;
        private Transform tfCamera;
        private Vector3 direction;
        private string parBMove = "BasicMove", parRun = "Running", parJmp= "toJump";
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
            Jump();
            if (Input.GetAxis("Fire1") != 0) ani.SetTrigger("toHurt");
        }
        #endregion

        #region Method
        private void Move(float x, float z)
        {
            direction.x = x ;
            direction.z = z ;
            float xSpeed = moveSpd;
            // 角色移動
            direction = transform.TransformDirection(direction);  //  將區域角度轉成世界角度
            // 跑步控制
            if (Input.GetKey(KeyCode.LeftShift)) xSpeed *= 2;
            else { ani.SetFloat(parRun, 0); }
            ccller.Move(direction * xSpeed * Time.deltaTime);
            // 跟著攝影機轉
            if (!Input.GetKey(KeyCode.F)) transform.rotation = Quaternion.Lerp(transform.rotation, tfCamera.rotation, spineSpd);
            // 人物角度固定
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            // 動畫控制
            ani.SetFloat(parBMove, (x != 0) ? Mathf.Abs(x) : (z != 0) ? Mathf.Abs(z) : 0);
            if (Input.GetKey(KeyCode.LeftShift)) ani.SetFloat(parRun, (x != 0) ? Mathf.Abs(x) : (z != 0) ? Mathf.Abs(z) : 0);
        }
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && ccller.isGrounded)
            {
                direction.y = jumpSpd;
                ani.SetFloat(parJmp, 1);
            } else if  (!ccller.isGrounded) { ani.SetFloat(parJmp, 0.5f); }
            else ani.SetFloat(parJmp, 0f);

            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        private void FadeJump()
        {

        }
        #endregion
    }
}