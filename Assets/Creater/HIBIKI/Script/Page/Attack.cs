using UnityEngine;

public class Attack : PageBase
{
    [SerializeField]
    float _damage;

    private void Start()
    {
        _pageKind = PageKind.Attack;
    }

    public override void PageActivation()
    {
        Debug.Log("UŒ‚ƒƒO");
    }
}
