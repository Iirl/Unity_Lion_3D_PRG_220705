using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    static public class StateMechine
    {
        static private GeneMotion state;
        static public GeneMotion GetMotion()
        {
            return state;
        }
        static public GeneMotion ToWait()
        {
            return state = GeneMotion.None;
        }
        static public GeneMotion ToHurt()
        {
            return state = GeneMotion.toHurt;
        }
        static public GeneMotion ToDead()
        {
            return state = GeneMotion.isDead;
        }
        static public GeneMotion ToRun()
        {
            return state = GeneMotion.Running;
        }
        static public GeneMotion ToJump()
        {
            return state = GeneMotion.toJump;
        }
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
    None,
    Running,
    toJump,
    toHurt,
    isDead,
}