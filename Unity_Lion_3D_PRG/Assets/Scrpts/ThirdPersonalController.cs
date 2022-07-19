using System.Collections.Generic;
using UnityEngine;


namespace agi
{
    /// <summary>
    /// �ĤT�H�ٱ��
    /// </summary>
    public class ThirdPersonalController : MonoBehaviour
    {
        #region Parameters �W��
        /* �򥻲��ʡGfloat BasicMove
         * �]�B�Gfloat Running
         * ���D�Gfloat toJump
         * ���ˡGbool toHurt
         * ���`�Gbool isDead
         * [��L]
         * ���šGbool isAir
         * 
         */
        #endregion
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
        private string parBMove = "BasicMove", parRun = "Running", parJmp = "toJump", parHurt= "toHurt";
        private bool isMove = false;
        // ���Ĭ���
        private AudioSource ads;
        [SerializeField, Header("�H������")]
        private List<AudioClip> clipList = new List<AudioClip>();
        #endregion

        #region Event
        private void Awake()
        {
            ani = GetComponent<Animator>();
            ccller = GetComponent<CharacterController>();
            tfCamera = GameObject.Find("Main Camera").transform;
            ads = GetComponent<AudioSource>();
        }
        private void Update()
        {
            isMove = Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Jump();
            if (Input.GetAxis("Fire1") != 0) ani.SetTrigger("toHurt");
            // ��������
            if (isMove & !ads.isPlaying) InvokeRepeating("PlayWalk", 0.2f, 2.5f);
            else CancelInvoke("PlayWalk");
        }
        #endregion

        #region Method
        /// <summary>
        /// ���ʱ���
        /// </summary>
        /// <param name="x">��J�����b</param>
        /// <param name="z">��J�a�V�b</param>
        private bool Move(float x, float z)
        {
            if (ani.GetBool(parHurt)) return false;            
            direction.x = x;
            direction.z = z;
            bool move = (x+z !=0)? true : false;
            float xSpeed = moveSpd;
            // ���Ⲿ��
            direction = transform.TransformDirection(direction);  //  �N�ϰ쨤���ন�@�ɨ���
            // �]�B����
            if (Input.GetKey(KeyCode.LeftShift)) xSpeed *= 2;
            else { ani.SetFloat(parRun, 0); }
            ccller.Move(direction * xSpeed * Time.deltaTime);
            // �����v����
            if (!Input.GetKey(KeyCode.F)) transform.rotation = Quaternion.Lerp(transform.rotation, tfCamera.rotation, spineSpd);
            // �H�����שT�w
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            // �ʵe����
            ani.SetFloat(parBMove, (x != 0) ? Mathf.Abs(x) : (z != 0) ? Mathf.Abs(z) : 0);
            if (Input.GetKey(KeyCode.LeftShift)) ani.SetFloat(parRun, (x != 0) ? Mathf.Abs(x) : (z != 0) ? Mathf.Abs(z) : 0);


            return move;
        }
        /// <summary>
        /// ���D����
        /// </summary>
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && ccller.isGrounded)
            {
                direction.y = jumpSpd;
                ani.SetFloat(parJmp, 1);
                SoundControl(1,1);
            }
            else if (!ccller.isGrounded) { ani.SetFloat(parJmp, 0.5f); }
            else ani.SetFloat(parJmp, 0f);

            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        /// <summary>
        /// ���ļ��񱱨�
        /// </summary>
        /// <param name="i">�M�w�n�����ح��ġA��ĳ��m���Ľs�����G
        /// 0: ����
        /// 1: ���D
        /// 2: ����
        /// 3: ����
        /// </param>
        private void SoundControl(int i, float vol)
        {
            ads.volume = vol;
            ads.PlayOneShot(clipList[i]);

        }
        /// <summary>
        /// �եΨ������ġA�o��������� Invoke �ϥΡC
        /// </summary>
        private void PlayWalk()
        {
            SoundControl(0,0.4f);
        }
        #endregion
    }
}