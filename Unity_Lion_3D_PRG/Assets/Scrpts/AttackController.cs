using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class AttackController : MonoBehaviour
    {
        ThirdPersonalController tirdPC;
        private Animator ani;
        private bool isAtk;
        #region 公用欄位
        [SerializeField, Header("子彈")]
        GameObject[] bullet;
        [SerializeField, Header("指定位置")]
        Transform ShootTrans;
        #endregion

        #region 方法
        private IEnumerator ShootInstantiate(int blc=0)
        {
            // blc: Bullet count;
            
            Instantiate(bullet[blc],ShootTrans.position + Vector3.one * Random.value /2, ShootTrans.rotation);
            yield return new WaitForSeconds(0.2f);
            isAtk = false;
        }
        #endregion

        #region 事件
        // Start is called before the first frame update
        private void Awake()
        {
            tirdPC = GetComponent<ThirdPersonalController>() ?? null;
            ani = GetComponent<Animator>();

        }
        void Start()
        {
            if (tirdPC == null) print("NULL");
        }

        // Update is called once per frame
        void Update()
        {
            if (ani.GetBool(Equiped.isGun.ToString()))
            {
                if (Input.GetAxisRaw("Fire1") == 1 && Input.GetAxisRaw("Fire2") == 1 )
                {
                    if(!isAtk) StartCoroutine( ShootInstantiate());
                    tirdPC.AnimeControl(5);
                    tirdPC.Invoke("PlayShoot", 0.15f);
                    isAtk = true;
                }
            }
        }
        #endregion


        enum Equiped { isSWS, isGun }
    }
}