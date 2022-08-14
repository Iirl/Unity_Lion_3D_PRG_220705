using UnityEngine;


namespace AGI { 
    public class MangerDead : MonoBehaviour
    {

        [SerializeField, Header("目標名稱")]
        private string targetName = "Cyan_basic";
        [SerializeField, Header("結束畫布系統")]
        private MangerFinal mangerFinal;
        [SerializeField, Header("CM 攝影機")]
        private GameObject cm_object;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log(collision.gameObject.name+"IN");
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            ////自定義方法
            if (collision.name.Contains(targetName))
            {
                mangerFinal.finalString = "Cyan is Dead!!" + mangerFinal.SceneTimeCal();
                mangerFinal.enabled = true;
                mangerFinal.FX_Shock(0.5f);
                cm_object.SetActive(false);
                Destroy(collision.gameObject);
            }
        }
    }
}