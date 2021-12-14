using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class EnemyPart : MonoBehaviour, IDamageable
{
    [SerializeField] float damageMult = 1f;
    [SerializeField] bool changeVisual;
    [SerializeField] int partHP = 50;
    [SerializeField] SkinnedMeshRenderer meshRenderer;

    int currentHP;
    Enemy coreEnemy;
    Collider coll;

    private void Awake()
    {
        coreEnemy = GetComponentInParent<Enemy>();
        coll = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        coll.enabled = true;
        if (changeVisual)
        {
            meshRenderer.enabled = true;
            currentHP = partHP;
        }
    }


    public void DoDestroy()
    {
        
    }

    public void TakeDamege(int damage)
    {
        int currentDamage = (int)(damage * damageMult);
        coreEnemy.TakeDamege(currentDamage);
        if (changeVisual)
        {
            currentHP -= currentDamage;
            if (currentHP <= 0)
            {
                coll.enabled = false;
                meshRenderer.enabled = false;
            }
        }
    }
}
