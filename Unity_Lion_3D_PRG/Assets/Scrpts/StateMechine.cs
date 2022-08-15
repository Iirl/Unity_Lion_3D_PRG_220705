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
            return state = GeneMotion.isIdel;
        }
        static public GeneMotion ToTrack()
        {
            return state = GeneMotion.toTrack;
        }
        static public GeneMotion ToAtk()
        {
            return state = GeneMotion.isAtking;
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

#region Parameters �W��
/* �򥻲��ʡGfloat BasicMove
 * �]�B�Gfloat Running
 * ���D�Gfloat toJump
 * ���ˡGbool toHurt
 * ���`�Gbool isDead
 * [��L]
 * ���šGbool isAir
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