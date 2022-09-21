using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace agi
{
    public class EnemyObjectPool : ObjectPoolSystem
    {
        private int count;
        public int CountNow => ObjPool.CountActive;
        protected override GameObject CreateObj()
        {
            GameObject tmp=null;
            if (CountNow >= getLimit) RelaseObj(outsideObj[Random.Range(0, outsideObj.Count - 1)]);
            else
            {
                tmp = Instantiate(PoolObj);
                outsideObj.Add(tmp);
            }
            tmp.name = tmp.name + " " + count;
            count++;
            
            return tmp;

        }
    }
}
