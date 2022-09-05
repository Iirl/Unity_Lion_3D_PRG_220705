using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace agi
{
    static public class StateMechine
    {
        static public string state(this GeneMotion st) => st.ToString();
        static public int stateInt(this GeneMotion st) => (int)st;
        static public GeneMotion stateInt(this GeneMotion st, int i) => st = (GeneMotion)i;
        static public GeneMotion ToWait(this GeneMotion st) => GeneMotion.isIdel;
        static public GeneMotion ToTrack(this GeneMotion st) => GeneMotion.toTrack;
        static public GeneMotion ToAtk(this GeneMotion st) => GeneMotion.isAtking;
        static public GeneMotion ToHurt(this GeneMotion st) => GeneMotion.toHurt;
        static public GeneMotion ToDead(this GeneMotion st) => GeneMotion.isDead;
        static public GeneMotion ToRun(this GeneMotion st) => GeneMotion.Running;
        static public GeneMotion ToJump(this GeneMotion st) => GeneMotion.toJump;
    }
}


#region Parameters 規劃
/* 基本移動：float BasicMove
 * 跑步：float Running
 * 跳躍：float toJump
 * 受傷：bool toHurt
 * 死亡：bool isDead
 * [其他]
 * 滯空：bool isAir
 * 
 */
#endregion
public enum GeneMotion
{
    isIdel,
    Running,
    toJump,
    toTrack,
    isAtking,
    toHurt,
    isDead,
}
