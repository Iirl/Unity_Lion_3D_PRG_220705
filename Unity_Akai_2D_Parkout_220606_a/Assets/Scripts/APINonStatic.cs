using UnityEngine;

/// <summary>
/// 非靜態 API
/// </summary>
namespace llii
{

    public class APINonStatic : MonoBehaviour
    {
        #region 基本屬性
        //設定遊戲物件
        [SerializeField]
        private GameObject Cyan ;
        [SerializeField]
        private Transform start_point;


        #endregion

        #region 事件處理
        private void Start()
        {
            // GetProperty
            //// GameObject
            print("<color=#c92020>取得物件狀態："+ Cyan.activeInHierarchy + "</color>");
            print("<color=#002020>取得物件圖層："+ Cyan.layer + "</color>");
            //// Transform
            print("起點的座標為：" + start_point.position);

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
