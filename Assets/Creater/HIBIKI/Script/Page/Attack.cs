using UnityEngine;

public class Attack : PageBase
{
    [SerializeField]
    AttackSystem.AttackType _attackType;

    [SerializeField]
    float _damage;

    private void Start()
    {
        _pageKind = PageKind.Attack;
    }

    public override void PageActivation()
    {

        switch (_attackType)
        {
            case AttackSystem.AttackType.Single:
                AttackSystem.SingleAttack();
                break;

            case AttackSystem.AttackType.Area:
                AttackSystem.AreaAttack();
                break;

            case AttackSystem.AttackType.All:
                AttackSystem.AllAttack();
                break;
        }
    }
}
