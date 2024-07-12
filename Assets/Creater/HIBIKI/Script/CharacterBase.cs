using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    float _health;
    float _defense;
    float _attack;
    float _distance;
    float _attackSpeed;
    float _moveSpeed;

    float _currentHealth;

    public void Spawn(float health, float defense, float attack, float distance, float attackSpeed, float moveSpeed)
    {
        _health = health;
        _defense = defense;
        _attack = attack;
        _distance = distance;
        _attackSpeed = attackSpeed;
        _moveSpeed = moveSpeed;

        _currentHealth = _health;

        Debug.Log($"��������܂����B\n�X�e�[�^�X��\n�w���X{_health}\n�h���{_defense}\n�U����{_attack}\n�˒�{_distance}\n�U�����x{_attackSpeed}\n�ړ����x{_moveSpeed}");
    }

    public void HitDamage(float damage)
    {
        _currentHealth -= Mathf.Min(damage - _defense, 0);

        Debug.Log(_currentHealth);
    }
}
