using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    bool canAttack = true;
    public float attackCooldown = 1f;
    public float attackDuration = 0.5f; 

    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    void Update()
    {
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        StartCoroutine(AttackReset());
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(attackDuration);

        boxCollider.enabled = false;
        meshRenderer.enabled = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}