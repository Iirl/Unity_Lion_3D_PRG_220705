using UnityEngine;


namespace AGI { 
    public class MangerDead : MonoBehaviour
    {

        [SerializeField, Header("�ؼЦW��")]
        private string targetName = "Cyan_basic";
        [SerializeField, Header("�����e���t��")]
        private MangerFinal mangerFinal;
        [SerializeField, Header("CM ��v��")]
        private GameObject cm_object;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log(collision.gameObject.name+"IN");
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            ////�۩w�q��k
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