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
            Debug.Log("�U������");
        }
        else
        {
            Debug.Log("�U���Ώۂ�������Ȃ�����");
        }
    }
}
