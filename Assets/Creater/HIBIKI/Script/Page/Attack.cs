using System.Collections.Generic;
using UnityEngine;
using static AttackSystem;

public class Attack : PageBase
{
    [SerializeField]
    AttackType _attackType;

    [SerializeField]
    float _damage;

    private void Start()
    {
        _pageKind = PageKind.Attack;
    }

    public override bool PageActivation()
    {
        List<CharacterBase> hitCharacter = AttackSystem.Attack(_attackType, TargetTag.Enemy);
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
