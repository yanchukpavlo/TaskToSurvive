using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour, IDamageable
{

    [SerializeField] int hp = 10;
    [SerializeField] float speed = 10;

    int currentHP;

    private void Awake()
    {
        currentHP = hp;
    }

    public void DoDestroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamege(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            DoDestroy();
        }
    }
}
