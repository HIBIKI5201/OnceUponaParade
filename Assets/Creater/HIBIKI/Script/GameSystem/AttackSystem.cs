using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public enum AttackType
    {
        Single,
        Area,
        All
    }

    public static void SingleAttack()
    {
        Debug.Log("単体攻撃ログ");
    }

    public static void AreaAttack()
    {
        Debug.Log("範囲攻撃ログ");
    }

    public static void AllAttack()
    {
        Debug.Log("全体攻撃ログ");
    }
}
