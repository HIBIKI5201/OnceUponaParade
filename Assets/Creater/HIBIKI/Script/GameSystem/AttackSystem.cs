using System.Collections.Generic;
using System.Linq;
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



    public static List<CharacterBase> Attack(AttackType attackType, TargetTag targetTag, Vector3 activePoint, float range)
    {
        List<CharacterBase> hitCharacter = Physics.OverlapSphere(activePoint, range)
            .Where(collider => collider.CompareTag(_targetTag[targetTag]) && collider.TryGetComponent<CharacterBase>(out _))
            .Select(collider => collider.GetComponent<CharacterBase>())
            .ToList();


        if (hitCharacter.Count > 0)
        {
            switch (attackType)
            {
                case AttackType.Single:
                    return hitCharacter = hitCharacter.Take((1)).ToList();

                case AttackType.Area:
                case AttackType.All:
                    return hitCharacter;

            }
        }
        return null;

    }
}
