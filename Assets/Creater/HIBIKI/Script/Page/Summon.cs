using UnityEngine;

public class Summon : PageBase
{
    [SerializeField]
    GameObject character;

    [SerializeField]
    float _health;
    [SerializeField]
    float _attack;

    private void Start()
    {
        _pageKind = PageKind.Summon;
    }

    public override void PageActivation()
    {
        Debug.Log("¢Š«ƒƒO");
    }
}
