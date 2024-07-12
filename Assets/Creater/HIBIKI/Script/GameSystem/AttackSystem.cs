using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public enum AttackType
    {
        Single,
        Area,
        All
    }

    public enum TargetTag
    {
        Friend,
        Enemy,
    }

    static readonly Dictionary<TargetTag, string> _targetTag = new()
    {
        {TargetTag.Friend, "Friend"},
        {TargetTag.Enemy, "Enemy"}
    };



    public static List<CharacterBase> Attack(AttackType attackType, TargetTag targetTag)
    {
        List<GameObject> hitGameObjects = new();
        List< CharacterBase > hitCharacter = new();

        switch (attackType)
        {
            case AttackType.Single:
                SingleAttack(ref hitGameObjects);
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
            hitGameObjects = hitGameObjects.Where(go => go.CompareTag(_targetTag[targetTag])).ToList();

            if (hitGameObjects.Count > 0)
            {
                foreach (GameObject obj in hitGameObjects)
                {
                    if (obj.TryGetComponent<CharacterBase>(out CharacterBase characterBase))
                    {
                        hitCharacter.Add(characterBase);
                        Debug.Log($"{obj.name}‚É“–‚½‚Á‚½");
                    }
                }

                return hitCharacter;
            }
        }

        return null;
       
    }

    static void SingleAttack(ref List<GameObject> hitGameObject)
    {
        if (Physics.Raycast(Vector3.zero, Vector3.forward, out RaycastHit hitInfo))
        {
            hitGameObject.Add(hitInfo.collider.gameObject);
        }
        Debug.Log("’P‘ÌUŒ‚ƒƒO");
    }

    static List<GameObject> AreaAttack()
    {
        Debug.Log("”ÍˆÍUŒ‚ƒƒO");
        List<GameObject> hitGameObjects = Physics.RaycastAll(Vector3.zero, Vector3.forward)
            .Select(hit => hit.collider.gameObject)
            .ToList();

        return hitGameObjects;
    }

    static List<GameObject> AllAttack()
    {
        Debug.Log("‘S‘ÌUŒ‚ƒƒO");
        List<GameObject> hitGameObjects = Physics.RaycastAll(Vector3.zero, Vector3.forward)
            .Select(hit => hit.collider.gameObject)
            .ToList();

        return hitGameObjects;
    }
}
