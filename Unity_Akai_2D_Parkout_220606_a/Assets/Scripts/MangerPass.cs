using UnityEngine;
using TMPro;

namespace AGI
{
    public class MangerPass : MonoBehaviour
    {
        [SerializeField, Header("�ؼЦW��")]
        private string targetName = "Cyan_basic";
        [SerializeField, Header("�]�B�t��")]
        private SystemRun systemRun;
        [SerializeField, Header("���D�t��")]
        private SystemMove systemMove;
        [SerializeField, Header("�����e���t��")]
        private MangerFinal mangerFinal;
        [SerializeField, Header("���O��r")]
        TextMeshProUGUI tpro_times;


        #region  Ĳ�o�ƥ�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �p�G�I�쪺�O���a����...
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

        #region �I���ƥ�
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

                tpro_times.SetText($"�g�L�ɶ�(Times)�G{Time.timeSinceLevelLoad.ToString("0.00")}");
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
    }
}