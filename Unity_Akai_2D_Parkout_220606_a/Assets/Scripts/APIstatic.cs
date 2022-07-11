using UnityEngine;

/// <summary>
/// API �R�A��k
/// </summary>
namespace AGI { 
    public class APIstatic : MonoBehaviour
    {
        private void Start()
        {
            #region Class Learning
            // ���o�R�A�ݩ� Random
            print("Random: "+ Random.value);
            GameObject cyan = GameObject.Find("Cyan_basic");
            SpriteRenderer spr = cyan.GetComponent<SpriteRenderer>();
            //print("���a���C��: "+spr.color);
            float r = Random.value * 100f;
            float g = Random.value * 100f;
            float b = Random.value * 100f;
            spr.color = Random.ColorHSV(0.6f, 0.8f, 0.2f, 1, 0.5f, 1, 0.8f, 1);

            //�ƾǭȡGPI
            print("PI is: "+ Mathf.PI);

            // �]�w�R�A�ݩ� Cursor, Physics2D, Time
            Cursor.visible = false;
            Physics2D.gravity = new Vector2(0, -0.05f);
            Time.timeScale = 0.8f;

            // ���o��k��
            float range = Random.Range(20f, 100f);
            print("�H���d��: " + range);
            #endregion

            #region HomeWork
            // �Ҧ���v���ƶq
            //-->allCamerasCount	The number of cameras in the current Scene.
            print("��v���ƶq�G"+Camera.allCamerasCount);

            // ���ε{�������x
            //-->platform	Returns the platform the game is running on (Read Only).
            print("���x�G"+Application.platform);

            // 3D ���z�ίv�{�ɭȳ]�w�� 10 
            //-->sleepThreshold	The mass-normalized energy threshold, below which objects start going to sleep. 
            // threshold: ���e�C
            Physics.sleepThreshold = 10;
            print("�ίv�{�ɭȳ]�w��: "+ Physics.sleepThreshold);

            // �ɶ��j�p�]�w�� 0.5 (�C�ʧ@)
            //-->timeScale	The scale at which time passes.
            Time.timeScale = 0.5f;
            print("�ɶ��y�ʳt�׬�: "+ Time.timeScale);

            //// Methods 
            //�� 9.999 �h�p���I (������h���覡) 
            //-->Floor	Returns the largest integer smaller than or equal to f.
            //-->Ceil	Returns the smallest integer greater to or equal to f.
            //-->Round	Returns f rounded to the nearest integer.
            float nine = 9.999f;
            print("Floor 9.999f: " + Mathf.Floor(nine));  // Result=9
            print("Ceil 9.999f: " + Mathf.Ceil(nine));  // Result=10
            print("Round 9.999f: " + Mathf.Round(nine));  // Result=10

            //���o���I���Z��
            ///new Vector3(1, 1, 1)
            ///new Vector3(22, 22, 22)
            ///-->Distance	Returns the distance between a and b.
            Vector3 vec1 = new Vector3(1, 1, 1);
            Vector3 vec2 = new Vector3(22, 22, 22);
            print("���I�Z�����G"+ Vector3.Distance(vec1,vec2));



            ///Open link:  https://unity.com/
            Application.OpenURL("https://unity.com/");
            #endregion
        }
        private void Update()
        {
            #region Class Learning
            //int range_Int = Random.Range(1, 3);
            //print("�H���d��: " + range_Int);
            #endregion

            #region HomeWork
            // �O�_��J���N�� (���)
            //-->anyKey	Is any key or mouse button currently held down? (Read Only)
            if (Input.anyKey)
            {
                print("A key or mouse click has been detected");
            }

            // �C���g�L�ɶ�
            //-->realtimeSinceStartup	The real time in seconds since the game started (Read Only).
            //print("�g�L�ɶ��G"+Time.realtimeSinceStartup);
            print("�g�L�ɶ��G"+Time.timeSinceLevelLoad); //�зǵ���

            ///Method
            //�O�_���U���� (���w���ť���)
            if (Input.GetKeyDown("space"))
            {
                print("sapce ���U");
            }
            #endregion


        }
    }
}
