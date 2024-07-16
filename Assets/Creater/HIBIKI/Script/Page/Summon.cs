using UnityEngine;

public class Summon : PageBase
{
    [Header("ステータス情報")]
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

    public override bool PageActivation(Vector3 activePoint)
    {
        Debug.Log("召喚ログ");

        Debug.Log(activePoint);

        //仮の召喚位置
        GameObject go = Instantiate(character, activePoint, Quaternion.identity);
        go.GetComponent<CharacterBase>().Spawn(_health, _defense, _attack, _distance, _attackSpeed, _moveSpeed);
        return true;
    }
}
