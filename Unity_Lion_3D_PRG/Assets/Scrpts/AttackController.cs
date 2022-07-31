using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class AttackController : MonoBehaviour
    {
        ThirdPersonalController tirdPC;
        private Animator ani;
        #region 公用欄位
        #endregion

        #region 方法

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
            if (tirdPC == null)print("NULL");
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetAxis("Fire1") == 1 && ani.GetBool(Equiped.isGun.ToString()))
            {

                tirdPC.AnimeControl(5);
                tirdPC.Invoke("PlayShoot", 0.15f);
            }
        }
        #endregion


        enum Equiped { isSWS, isGun }
    }
}