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
        if (AttackSystem.Attack(_attackType, _damage))
        {
            Debug.Log("攻撃成功");
        }
        else
        {
            Debug.Log("攻撃対象が見つからなかった");
        }
    }
}
