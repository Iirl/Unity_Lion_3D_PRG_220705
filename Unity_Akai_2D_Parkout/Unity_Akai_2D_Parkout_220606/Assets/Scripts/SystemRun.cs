using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �]�B�t��
/// </summary>

namespace AGI { 
    public class SystemRun : MonoBehaviour
    {
        #region ��ơG�t�ΫO�s���
        [SerializeField, Header("�]�B�Ѽƽվ�"), Tooltip("�򥻳t�׽վ�A�ɶq���n�W�L10�C"), Range(0f, 20)]
        private float RunSpeed = 3.5f;
        [SerializeField, Header("����I���ϰ�")]
        private Vector2 collider_range;
        private Animator ani;
        private Rigidbody2D rig;
        private CapsuleCollider2D co2d;
        [SerializeField, Header("���D�t��")]
        private SystemMove systemMove;
        #endregion

        #region �\��G�D�n�{�����檺�ϰ�

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

        #region �ƥ�G�{�����J�f�I
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