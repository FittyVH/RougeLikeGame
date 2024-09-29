using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomAttack : MonoBehaviour
{
    public MonoBehaviour[] attackScripts;   
    public float attackInterval;            
    public bool attackOver;                 

    private MonoBehaviour currentAttackScript;

    void Start()
    {
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            EnableScript();

            yield return new WaitUntil(() => currentAttackScript.isActiveAndEnabled == false);

            // attackOver = false;

            yield return new WaitForSeconds(attackInterval);
        }
    }

    void EnableScript()
    {
        int randomIndex = Random.Range(0, attackScripts.Length);
        currentAttackScript = attackScripts[randomIndex];
        currentAttackScript.enabled = true;
    }
}