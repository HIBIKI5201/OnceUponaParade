using UnityEngine;
using UnityEngine.Rendering;

public class Attack : PageBase
{
    [SerializeField]
    float _damage;

    public AttackType _attackType;
    public enum AttackType
    {
        One,
        Field,
        All
    }

    private void Start()
    {
        _pageKind = PageKind.Attack;
    }

    public override void PageActivation()
    {
        Debug.Log("UŒ‚ƒƒO");
    }
}
