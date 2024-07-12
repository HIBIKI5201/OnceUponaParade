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

    public override bool PageActivation()
    {
        Debug.Log("è¢ä´ÉçÉO");

        //âºÇÃè¢ä´à íu
        GameObject go = Instantiate(character, transform.position, Quaternion.identity);
        go.GetComponent<CharacterBase>().Spawn(_health, _defense, _attack, _distance, _attackSpeed, _moveSpeed);
        return true;
    }
}
