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
        Debug.Log("�P�̍U�����O");
    }

    public static void AreaAttack()
    {
        Debug.Log("�͈͍U�����O");
    }

    public static void AllAttack()
    {
        Debug.Log("�S�̍U�����O");
    }
}
