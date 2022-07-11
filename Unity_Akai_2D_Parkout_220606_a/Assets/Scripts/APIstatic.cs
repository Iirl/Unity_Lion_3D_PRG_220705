using UnityEngine;

/// <summary>
/// API 靜態方法
/// </summary>
namespace AGI { 
    public class APIstatic : MonoBehaviour
    {
        private void Start()
        {
            #region Class Learning
            // 取得靜態屬性 Random
            print("Random: "+ Random.value);
            GameObject cyan = GameObject.Find("Cyan_basic");
            SpriteRenderer spr = cyan.GetComponent<SpriteRenderer>();
            //print("玩家的顏色: "+spr.color);
            float r = Random.value * 100f;
            float g = Random.value * 100f;
            float b = Random.value * 100f;
            spr.color = Random.ColorHSV(0.6f, 0.8f, 0.2f, 1, 0.5f, 1, 0.8f, 1);

            //數學值：PI
            print("PI is: "+ Mathf.PI);

            // 設定靜態屬性 Cursor, Physics2D, Time
            Cursor.visible = false;
            Physics2D.gravity = new Vector2(0, -0.05f);
            Time.timeScale = 0.8f;

            // 取得方法值
            float range = Random.Range(20f, 100f);
            print("隨機範圍: " + range);
            #endregion

            #region HomeWork
            // 所有攝影機數量
            //-->allCamerasCount	The number of cameras in the current Scene.
            print("攝影機數量："+Camera.allCamerasCount);

            // 應用程式的平台
            //-->platform	Returns the platform the game is running on (Read Only).
            print("平台："+Application.platform);

            // 3D 物理睡眠臨界值設定為 10 
            //-->sleepThreshold	The mass-normalized energy threshold, below which objects start going to sleep. 
            // threshold: 門檻。
            Physics.sleepThreshold = 10;
            print("睡眠臨界值設定為: "+ Physics.sleepThreshold);

            // 時間大小設定為 0.5 (慢動作)
            //-->timeScale	The scale at which time passes.
            Time.timeScale = 0.5f;
            print("時間流動速度為: "+ Time.timeScale);

            //// Methods 
            //對 9.999 去小數點 (不限制去除方式) 
            //-->Floor	Returns the largest integer smaller than or equal to f.
            //-->Ceil	Returns the smallest integer greater to or equal to f.
            //-->Round	Returns f rounded to the nearest integer.
            float nine = 9.999f;
            print("Floor 9.999f: " + Mathf.Floor(nine));  // Result=9
            print("Ceil 9.999f: " + Mathf.Ceil(nine));  // Result=10
            print("Round 9.999f: " + Mathf.Round(nine));  // Result=10

            //取得兩點的距離
            ///new Vector3(1, 1, 1)
            ///new Vector3(22, 22, 22)
            ///-->Distance	Returns the distance between a and b.
            Vector3 vec1 = new Vector3(1, 1, 1);
            Vector3 vec2 = new Vector3(22, 22, 22);
            print("兩點距離為："+ Vector3.Distance(vec1,vec2));



            ///Open link:  https://unity.com/
            Application.OpenURL("https://unity.com/");
            #endregion
        }
        private void Update()
        {
            #region Class Learning
            //int range_Int = Random.Range(1, 3);
            //print("隨機範圍: " + range_Int);
            #endregion

            #region HomeWork
            // 是否輸入任意鍵 (欄位)
            //-->anyKey	Is any key or mouse button currently held down? (Read Only)
            if (Input.anyKey)
            {
                print("A key or mouse click has been detected");
            }

            // 遊戲經過時間
            //-->realtimeSinceStartup	The real time in seconds since the game started (Read Only).
            //print("經過時間："+Time.realtimeSinceStartup);
            print("經過時間："+Time.timeSinceLevelLoad); //標準答案

            ///Method
            //是否按下按鍵 (指定為空白鍵)
            if (Input.GetKeyDown("space"))
            {
                print("sapce 按下");
            }
            #endregion


        }
    }
}
