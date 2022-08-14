using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跑步系統
/// </summary>

namespace AGI { 
    public class SystemRun : MonoBehaviour
    {
        #region 資料：系統保存資料
        [SerializeField, Header("跑步參數調整"), Tooltip("基本速度調整，盡量不要超過10。"), Range(0f, 20)]
        private float RunSpeed = 3.5f;
        [SerializeField, Header("角色碰撞區域")]
        private Vector2 collider_range;
        private Animator ani;
        private Rigidbody2D rig;
        private CapsuleCollider2D co2d;
        [SerializeField, Header("跳躍系統")]
        private SystemMove systemMove;
        #endregion

        #region 功能：主要程式執行的區域

        void HorizonMove(float horizonKey)
        {
            float xSpeed;
            float ySpeed = rig.velocity.y;
            bool running = Input.GetKey(KeyCode.LeftShift);
            if (horizonKey > 0)
            {
                xSpeed = RunSpeed;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizonKey < 0)
            {
                xSpeed = -RunSpeed;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                xSpeed = 0.0f;
            }

            rig.velocity = new  Vector2(xSpeed,ySpeed);
            

        }
        void Shot_Down(float MoveDown)
        {
            Vector2 v2 = new Vector2(transform.localScale.x, 0.5f);
            if (MoveDown < 0) { transform.localScale = v2; co2d.size = new Vector2(collider_range.x, collider_range.y * 0.2f); }
            else { transform.localScale = new Vector2(transform.localScale.x,1); co2d.size = collider_range; }
        }
        private void Animate_Run()
        {
            if ( rig.velocity != Vector2.zero)
            {
                ani.SetBool("isRun", true);

            } else
            {

                ani.SetBool("isRun", false);
            }
        }
        #endregion

        #region 事件：程式的入口點
        private void Start()
        {
            // print("Hello MyDDD"); 


        }
        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            co2d = GetComponent<CapsuleCollider2D>();
            co2d.size = collider_range;
        }

        private void Update()
        {
            HorizonMove(Input.GetAxis("Horizontal"));
            Shot_Down(Input.GetAxis("Vertical"));
            rig.velocity += new Vector2(RunSpeed * transform.localScale.x, 0);
            if (Input.GetKey(KeyCode.LeftShift) && systemMove.ckGround) rig.AddForce(Vector2.right * RunSpeed * 20 * transform.localScale.x);
            Animate_Run();
        }

        private void OnDisable()
        {
            rig.velocity = Vector2.zero;
            Animate_Run();
        }
        #endregion
    }
}