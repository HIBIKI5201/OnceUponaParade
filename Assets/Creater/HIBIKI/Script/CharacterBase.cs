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

        Debug.Log($"召喚されました。\nステータスは\nヘルス{_health}\n防御力{_defense}\n攻撃力{_attack}\n射程{_distance}\n攻撃速度{_attackSpeed}\n移動速度{_moveSpeed}");
    }

    public void HitDamage(float damage)
    {
        _currentHealth -= Mathf.Min(damage - _defense, 0);

        Debug.Log(_currentHealth);
    }
}
