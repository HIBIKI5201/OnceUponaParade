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

    public static bool Attack(AttackType attackType, float damage)
    {
        List<GameObject> hitGameObjects = new();

        switch (attackType)
        {
            case AttackType.Single:
                hitGameObjects.Add(SingleAttack());
                break;

            case AttackType.Area:
                hitGameObjects = AreaAttack();
                break;

            case AttackType.All:
                hitGameObjects = AllAttack();
                break;
        }


        if (hitGameObjects != null)
        {
            foreach (GameObject obj in hitGameObjects)
            {
                Debug.Log($"{obj.name}に{damage}ダメージ与えた");
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    static GameObject SingleAttack()
    {
        Debug.Log("単体攻撃ログ");
        GameObject newGameObject = new("testObj");

        GameObject hitGameObject = newGameObject;
        return hitGameObject;
    }

    static List<GameObject> AreaAttack()
    {
        Debug.Log("範囲攻撃ログ");
        List<GameObject> hitGameObjects = new()
        {
            new("testObj1"),
            new("testObj2"),
            new("testObj3")
        };

        return hitGameObjects;
    }

    static List<GameObject> AllAttack()
    {
        Debug.Log("全体攻撃ログ");
        //正式にはnew()にすること
        List<GameObject> hitGameObjects = null;

        return hitGameObjects;
    }
}
