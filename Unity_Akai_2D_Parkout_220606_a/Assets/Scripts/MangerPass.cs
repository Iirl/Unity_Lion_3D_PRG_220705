using UnityEngine;
using TMPro;

namespace AGI
{
    public class MangerPass : MonoBehaviour
    {
        [SerializeField, Header("目標名稱")]
        private string targetName = "Cyan_basic";
        [SerializeField, Header("跑步系統")]
        private SystemRun systemRun;
        [SerializeField, Header("跳躍系統")]
        private SystemMove systemMove;
        [SerializeField, Header("結束畫布系統")]
        private MangerFinal mangerFinal;
        [SerializeField, Header("面板文字")]
        TextMeshProUGUI tpro_times;


        #region  觸發事件
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 如果碰到的是玩家的話...
            if (this.tag == "Finish")
            {
                if (collision.name.Contains(targetName))
                {
                    systemMove.enabled = false;
                    systemRun.enabled = false;
                    mangerFinal.enabled = true;
                    mangerFinal.StopMusic();

                    mangerFinal.finalString = "You win the games!" + mangerFinal.SceneTimeCal();
                    this.enabled = false;
                }
            }
            if (this.tag == "Block" && collision.name.Contains(targetName))
            {
                mangerFinal.FX_Shock();
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }

        private void OnTriggerStay2D(Collider2D collision)
        {

        }
        #endregion

        #region 碰撞事件
        private void OnCollisionEnter2D(Collision2D collision)
        {

        }

        private void OnCollisionExit2D(Collision2D collision)
        {

        }

        private void OnCollisionStay2D(Collision2D collision)
        {

        }

        private void Update()
        {
            try
            {

                tpro_times.SetText($"經過時間(Times)：{Time.timeSinceLevelLoad.ToString("0.00")}");
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
    }
}