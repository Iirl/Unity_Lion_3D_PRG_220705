using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace agi
{
    /// <summary>
    /// 第三人稱控制器
    /// </summary>
    public class ThirdPersonalController : MonoBehaviour
    {
        #region Parameters 規劃
        /* 基本移動：float BasicMove
         * 跑步：float Running
         * 跳躍：float toJump
         * 受傷：bool toHurt
         * 死亡：bool isDead
         * [其他]
         * 滯空：bool isAir
         * 
         */
        #endregion
        #region Data
        [SerializeField, Header("Move Speed"), Range(0.0f, 20f)]
        private float moveSpd = 3.5f;
        [SerializeField, Header("Spine Speed"), Range(0, 50)]
        private float spineSpd = 5f;
        [SerializeField, Header("Jump Speed"), Range(1f, 15f)]
        private float jumpSpd = 2.4f;
        private Animator ani;
        private CharacterController ccller;
        private Transform tfCamera;
        private CinemachineFreeLook cfLook;
        private Vector3 direction;
        private Motion actorMV;
        private bool isMove = false, isJump = false;
        // 音效相關
        private AudioSource ads;
        [SerializeField, Header("人物音效")]
        private List<AudioClip> clipList = new List<AudioClip>();
        #endregion

        #region Event
        private void Awake()
        {
            ani = GetComponent<Animator>();
            ccller = GetComponent<CharacterController>();
            tfCamera = GameObject.Find("Main Camera").transform;
            ads = GetComponent<AudioSource>();
            // 取得焦點
            cfLook = GameObject.Find("第三人稱攝影機").GetComponent<CinemachineFreeLook>();
        }
        private void Start()
        {
            CameraFocus();

        }
        private void Update()
        {
            isMove = Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Jump();
            // 走路音效
            if (isMove) Invoke("PlayWalk", 0.5f);
        }
        private void OnEnable()
        {
            CameraFocus();
        }

        private void OnCollisionEnter(Collision collision)
        {
            try
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(direction + Vector3.up * 2);
            }
            catch (Exception) { }
        }

        #endregion

        public void StopMove()
        {
            ani.SetFloat(Motion.BasicMove.ToString(), 0);
            ani.SetFloat(Motion.Running.ToString(), 0);
        }


        #region Method
        /// <summary>
        /// 移動控制
        /// </summary>
        /// <param name="x">輸入水平軸</param>
        /// <param name="z">輸入縱向軸</param>
        private bool Move(float x, float z)
        {
            string parBMove = Motion.BasicMove.ToString();
            string parHurt = Motion.toHurt.ToString();
            string parRun = Motion.Running.ToString();
            if (ani.GetBool(parHurt)) return false;
            direction.x = x;
            direction.z = z;
            bool move = (x + z != 0) ? true : false;
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

            // 動畫控制 x為左右； z 為前後
            ani.SetFloat(parBMove, z / 2);
            ani.SetFloat(parRun, x / 2);
            //print(ani.GetFloat(parBMove));
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ani.SetFloat(parBMove, ani.GetFloat(parBMove) * 2);
                ani.SetFloat(parRun, ani.GetFloat(parRun) * 2);
            }


            return move;
        }
        /// <summary>
        /// 跳躍控制
        /// </summary>
        private void Jump(Motion motion = Motion.toJump)
        {
            if (Input.GetKeyDown(KeyCode.Space)) isJump = true;
            if (isJump && ccller.isGrounded)
            {
                direction.y = jumpSpd;
                ani.SetFloat(motion.ToString(), 1);
                SoundControl(1, 1);
                isJump = false;
            }
            else if (!ccller.isGrounded)
            {
                ani.SetFloat(motion.ToString(), 0.5f);
                direction.x /= 2;
                direction.z /= 2;
            }
            else
            {
                ani.SetFloat(motion.ToString(), 0f);
                isJump = false;
            }
            direction.y += Physics.gravity.y * Time.deltaTime;
        }

        private void CameraFocus()
        {
            cfLook.LookAt = this.transform;
            cfLook.Follow = this.transform;

        }
        /// <summary>
        /// 音效播放控制
        /// </summary>
        /// <param name="i">決定要撥放何種音效，建議放置音效編號為：
        /// 0: 走路
        /// 1: 跳躍
        /// 2: 受傷
        /// 3: 攻擊
        /// </param>
        private void SoundControl(int i, float vol = 0.7f, float pitch = 1)
        {
            ads.volume = vol;
            ads.pitch = pitch;
            try
            {
                ads.PlayOneShot(clipList[i]);
            }
            catch (Exception) { print($"The voice {i} not setting"); }

        }
        /// <summary>
        /// 調用走路音效，這項函數應給 Invoke 使用。
        /// </summary>
        private void PlayWalk()
        {
            float pth = 1;
            if (Input.GetKey(KeyCode.LeftShift)) pth = 1.5f;
            SoundControl(0, 0.4f, pth);
            CancelInvoke("PlayWalk");
        }
        public void PlayTrack(int i) => SoundControl(i); //播放音效
        /// <summary>
        /// 動畫執行控制，所有動畫設定在這裡設。
        /// </summary>
        public void AnimeControl(int idx, float par = 0)
        {
            foreach (var m in Enum.GetValues(typeof(Motion))) if ((int)m == idx) actorMV = (Motion)m;
            string motionName = actorMV.ToString();
            try
            {
                if (idx == 1) ani.SetTrigger(motionName);
                //else if (idx == 4) isJump = true;
                else if (idx == 5) ani.SetBool(motionName, par == 0);
                else if (idx == 6) ani.SetTrigger(motionName);
                else if (idx == 8) ani.SetFloat(motionName, par);

            }
            catch (Exception)
            {
                print($"not set animate parameter {actorMV}");
            }

        }
        #endregion

    }
}
public enum Motion
{
    isDead,
    toHurt,
    BasicMove,
    Running,
    toJump,
    isShoot,
    toSWAtk,
    toSWHeavy,
    actAttack,
    actBlock
}