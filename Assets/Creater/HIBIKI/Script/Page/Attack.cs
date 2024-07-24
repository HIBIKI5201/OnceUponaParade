using System.Collections.Generic;
using UnityEngine;
using static AttackSystem;

public class Attack : PageBase
{
    [Header("ステータス情報")]
    [SerializeField]
    AttackType _attackType;

    [SerializeField]
    float _damage = 0;
    [SerializeField]
    float _range = 1;

    private void Start()
    {
        _pageKind = PageKind.Attack;
    }

    public override bool PageActivation(Vector3 activePoint)
    {
        List<CharacterBase> hitCharacter = AttackSystem.Attack(_attackType, TargetTag.Enemy, activePoint, _range);
        if (hitCharacter != null )
        {
            foreach (CharacterBase Character in hitCharacter)
            {
                Character.HitDamage(_damage);
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
