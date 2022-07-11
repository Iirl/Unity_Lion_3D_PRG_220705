using UnityEngine;


namespace AGI
{
    public class SystemMove : MonoBehaviour
    {
        private Animator ani = null;
        private Rigidbody2D r2d = null;
        [SerializeField, Header("跳躍高度"), Range(100, 600)]
        private float Jmp_speed;
        public bool click_jmp;
        public bool ckGround;
        public bool ckWall;
        [SerializeField, Header("檢查地板大小")]
        private Vector3 ck_ground_size = Vector3.one;
        [SerializeField, Header("檢查地板位移")]
        private Vector3 ck_ground_offset;
        [SerializeField, Header("檢查地板顏色")]
        private Color ck_ground_color = new Color(0.0f, 0.8f, 0.5f);
        [SerializeField, Header("檢查地板圖層")]
        private LayerMask ck_layer_ground;
        [SerializeField, Header("動畫參數")]
        private string ani_jump = "isJump";
        private string ani_wall = "isOntheWall";

        [SerializeField, Header("跳躍音效")]
        private AudioClip audioClip_jump;
        private AudioSource ads;

        #region 功能

        /// <summary>
        /// 跳躍系統
        /// </summary>
        void Jumpkey()
        {
            if (Input.GetKeyDown("space")) click_jmp = true;
            else if (Input.GetKeyUp("space")) click_jmp = false;
        }
        private void Jumpup()
        {
            //print($"JUMP:{click_jmp} GROUND:{ckGround}");
            float x_force = r2d.velocity.x;
            float y_force = 0;
            if (click_jmp && ckWall && !ckGround)
            {
                x_force = -x_force * 500;
                y_force = Jmp_speed ;
            }else if (click_jmp && ckGround)
            {
                x_force = 0;
                y_force = Jmp_speed;
                ads.PlayOneShot(audioClip_jump, Random.Range(0.8f, 1.2f));
            }
            if (click_jmp)
            {
                r2d.AddForce(new Vector2(x_force, y_force));
                click_jmp = false;
            }
        }

        /// <summary>
        /// 更新動畫事件
        /// </summary>
        private void Animate()
        {
            ani.SetBool(ani_jump, !ckGround);
            ani.SetBool(ani_wall, ckWall);
        }

        private void Check_Ground_Collider()
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position + ck_ground_offset, ck_ground_size, 0, ck_layer_ground);            
            ckGround = hit;//print("碰到區域：" +hit.name);

            Vector3 wall_offset = new Vector3(ck_ground_offset.y + 0.2f, ck_ground_offset.x, ck_ground_offset.z);
            Vector3 wall_size = new Vector3(ck_ground_size.y, ck_ground_size.x, ck_ground_size.z);
            Collider2D Right_wall = Physics2D.OverlapBox(transform.position + (-wall_offset), wall_size, 0, ck_layer_ground);
            Collider2D Left_wall = Physics2D.OverlapBox(transform.position + wall_offset, wall_size, 0, ck_layer_ground);
            if (Left_wall != null)
            {
                ckWall = Left_wall;
            } else if (Right_wall != null)
            {
                ckWall = Right_wall;

            } else { ckWall = false; }

        }
        
        #endregion

        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();
            r2d = GetComponent<Rigidbody2D>();
            ads = GetComponent<AudioSource>();

        }
        private void OnDrawGizmos()
        {
            Vector3 wall_offset = new Vector3(ck_ground_offset.y+0.2f, ck_ground_offset.x, ck_ground_offset.z);
            Vector3 wall_size = new Vector3(ck_ground_size.y, ck_ground_size.x, ck_ground_size.z);
            Gizmos.color = ck_ground_color;
            Gizmos.DrawCube(transform.position + ck_ground_offset, ck_ground_size);
            Gizmos.DrawCube(transform.position + wall_offset, wall_size);
            Gizmos.DrawCube(transform.position + (-wall_offset), wall_size);
        }
        private void Start()
        {

        }

        private void Update()
        {
            Jumpkey();
            Jumpup();
            Animate();
            Check_Ground_Collider();
        }

        #endregion
    }

}