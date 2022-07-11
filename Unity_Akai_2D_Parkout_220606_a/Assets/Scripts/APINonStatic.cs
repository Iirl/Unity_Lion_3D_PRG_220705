using UnityEngine;

/// <summary>
/// �D�R�A API
/// </summary>
namespace llii
{

    public class APINonStatic : MonoBehaviour
    {
        #region ���ݩ�
        //�]�w�C������
        [SerializeField]
        private GameObject Cyan ;
        [SerializeField]
        private Transform start_point;


        #endregion

        #region �ƥ�B�z
        private void Start()
        {
            // GetProperty
            //// GameObject
            print("<color=#c92020>���o���󪬺A�G"+ Cyan.activeInHierarchy + "</color>");
            print("<color=#002020>���o����ϼh�G"+ Cyan.layer + "</color>");
            //// Transform
            print("�_�I���y�Ь��G" + start_point.position);

            // SetProperty
            //// GameObject
            Cyan.layer = 4;
            Cyan.tag = "Player";
            //// Transform
            start_point.position = new Vector2(2.25f,5.0f);

            // SetMethod
            //// GameObject
            Cyan.SetActive(true);
            //// Transform

        }

        private void Update()
        {
            start_point.Translate (0.05f,0 ,0 );
            start_point.Rotate(0,0,0.7f);
        }
        #endregion
    }

}
