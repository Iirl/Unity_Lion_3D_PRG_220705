using UnityEngine;
/// <summary>
/// 0620 �D�R�A�ݩʻP��k�@�~
/// </summary>
/// 
namespace llii
{
    public class APINS_Homework_0620 : MonoBehaviour
    {
        #region �ݩ�
        [SerializeField] GameObject gob_sphere, gob_cube, gob_caps;
        [SerializeField] Camera cam;
        SphereCollider spc;
        Rigidbody rb;
        [SerializeField, Header("Sphere Move Speed"), Range(0.01f, 1f)]
        private float MoveSpeed = 0.5f;
        bool push = true;
        #endregion


        #region ��k
        /// <summary>
        /// ���ʨ쥪��Ay,z ����
        /// </summary>
        public void MoveToLeft()
        {
            gob_cube.transform.position = new Vector3(Random.Range(-6, gob_cube.transform.position.x), gob_cube.transform.position.y, gob_cube.transform.position.z);
        }
        /// <summary>
        /// ���ʨ�k��Ay,z ����
        /// </summary>
        public void MoveToRight()
        {
            gob_cube.transform.position = new Vector3(Random.Range(gob_cube.transform.position.x, 7), gob_cube.transform.position.y, gob_cube.transform.position.z);
        }

        #endregion

        #region �ƥ�J�f
        /// <summary>
        /// ���o���󪺤���
        /// </summary>
        private void Awake()
        {
            rb = gob_caps.GetComponent<Rigidbody>();
            spc = gob_sphere.GetComponent<SphereCollider>();
        }
        private void Start()
        {
            // Q1.��v���`�� 
            //depth	Camera's depth in the camera rendering order.                
            print("��v���`�׬��G" + cam.depth);
            // Q3.��v�����I���C����w���H���C��
            // backgroundColor	The color with which the screen will be cleared.
            // Random.value	Returns a random float within [0.0..1.0] (range is inclusive) (Read Only).
            cam.backgroundColor = Color.HSVToRGB(Random.value, Random.value, Random.value);

            if (gob_sphere != null)
            {
                // Q2.�y��I�����b�|
                // radius	The radius of the sphere measured in the object's local space.                
                print("�y��I�������b�|�G" + spc.radius);
            }

            if (gob_caps != null)
            {
                // Q4.���n��ؤo�אּ 3, 2, 1
                gob_caps.transform.localScale = new Vector3(3, 2, 1);
            }
        }

        private void Update()
        {
            if (gob_cube != null)
            {
                float cb2sp = Vector2.Distance(gob_cube.transform.position, gob_sphere.transform.position);
                float x_cb2sp = gob_cube.transform.position.x - gob_sphere.transform.position.x;
                float y_cb2sp = gob_cube.transform.position.y - gob_sphere.transform.position.y;

                // Q5.���ߤ���ݵ۲y�����
                // LookAt	Rotates the transform so the forward vector points at /target/'s current position.
                ////  �ߤ���ݲy��
                gob_cube.transform.LookAt(gob_sphere.transform, Vector3.forward);
                ////  �y�����
                ///

                if (gob_sphere.transform.position != transform.position) gob_sphere.transform.position += new Vector3(x_cb2sp / 10, y_cb2sp / 10, 0);
                gob_sphere.transform.Rotate(new Vector3(0, 0, MoveSpeed));
                gob_sphere.transform.Translate(Random.value + 0.1f, 0, 0);
            }
            if (gob_caps != null)
            {
                if (gob_caps.transform.position.y > 80) push = false;
                if (gob_caps.transform.position.y < 0) push = true;
                // Q6.�����n�驹�W���ͱ��O
                // AddForce	Adds a force to the Rigidbody.
                if (push) rb.AddForce(Vector3.up * 1000 * Time.deltaTime);
            }
        }
        #endregion
    }
}