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
            Debug.Log("UŒ‚¬Œ÷");
        }
        else
        {
            Debug.Log("UŒ‚‘ÎÛ‚ªŒ©‚Â‚©‚ç‚È‚©‚Á‚½");
        }
    }
}
