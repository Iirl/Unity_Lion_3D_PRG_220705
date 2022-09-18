using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace agi
{
    public class AttackController : AttackSystem
    {
        ThirdPersonalController tirdPC;

        #region 公用欄位
        [SerializeField, Header("子彈")]
        GameObject[] bullet;
        [SerializeField, Header("指定位置")]
        Transform ShootTrans;
        [SerializeField, Header("裝備狀態")]
        Equiped Equipment;
        [SerializeField, Header("右手位置")]
        Transform RightHandTransform;
        #endregion


        #region 方法
        private IEnumerator ShootInstantiate(int blc = 0)
        {
            // blc: Bullet count;

            Instantiate(bullet[blc], ShootTrans.position + Vector3.one * Random.value / 2, ShootTrans.rotation);
            yield return new WaitForSeconds(0.2f);
        }

        private void EquipControll()
        {
            int ename = -1;
            string[] equip = { Equiped.isSWS.ToString(), (Equiped.isGun.ToString()) };
            foreach (var e in equip) ani.SetBool(e, false);
            switch (Equipment)
            {
                case Equiped.isSWS:
                    ename = 0;
                    break;
                case Equiped.isGun:
                    ename = 1;
                    break;
                default:
                    break;
            }
            ani.SetBool(equip[ename], true);
        }
        private void EquipChange(int idx)
        {

            Equipment = (Equiped)idx;
            EquipControll();
        }

        #region 複寫方法
        protected override void SoundAttack(bool hit)
        {
            if (Equipment == Equiped.isSWS)
            {
                if (hit) tirdPC.PlayTrack(4);
                else tirdPC.PlayTrack(5);
            }
        }
        protected override void StopAttack()
        {
            tirdPC.enabled = true;
        }
        #endregion
        #endregion

        #region 事件
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            tirdPC = GetComponent<ThirdPersonalController>() ?? null;
        }


        void Start()
        {
            if (tirdPC == null) print("NULL");
            EquipControll();
        }



        void Update()
        {
            if (Input.GetKeyUp("1")) EquipChange(0);
            if (Input.GetKeyUp("2")) EquipChange(1);
            if (!isAnimating) AttackState();

        }

        private void AttackState()
        {
            switch (Equipment)
            {
                case Equiped.isSWS:
                    if (Input.GetAxisRaw("Fire1") == 1)
                    {
                        //print("刀劍攻擊狀態");
                        tirdPC.AnimeControl(6);
                        tirdPC.enabled = false;
                        StartAttack(0);
                    }
                    break;
                case Equiped.isGun:
                    if (Input.GetMouseButton(1))
                    {
                        tirdPC.AnimeControl(5);
                        if (Input.GetMouseButtonUp(0) && !isAnimating)
                        {
                            StartAttack(1);
                            StartCoroutine(ShootInstantiate());
                            tirdPC.PlayTrack(3);
                        }
                    }
                    else tirdPC.AnimeControl(5, -1);
                    break;
                default:
                    break;
            }
        }
        #endregion


        enum Equiped { isSWS, isGun }
    }
}