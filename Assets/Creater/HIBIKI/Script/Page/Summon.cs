using UnityEngine;

public class Summon : PageBase
{
    [SerializeField]
    GameObject character;

    [SerializeField]
    float _health;
    [SerializeField]
    float _defense;
    [SerializeField]
    float _attack;

    [SerializeField]
    float _distance;

    [SerializeField]
    float _attackSpeed;
    [SerializeField]
    float _moveSpeed;

    private void Start()
    {
        _pageKind = PageKind.Summon;
    }

    public override void PageActivation()
    {
        Debug.Log("è¢ä´ÉçÉO");

        Instantiate(character, Vector2.zero, Quaternion.identity);

    }
}
