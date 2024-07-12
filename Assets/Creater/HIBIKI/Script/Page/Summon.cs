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

    public override bool PageActivation(Vector3 Pos)
    {
        Debug.Log("�������O");

        Debug.Log(Pos);
        Physics.Raycast(transform.position, Pos, out RaycastHit activePos, Mathf.Infinity, LayerMask.GetMask("Ground"));

        Debug.Log(activePos.point);

        //���̏����ʒu
        GameObject go = Instantiate(character, activePos.point, Quaternion.identity);
        go.GetComponent<CharacterBase>().Spawn(_health, _defense, _attack, _distance, _attackSpeed, _moveSpeed);
        return true;
    }
}
