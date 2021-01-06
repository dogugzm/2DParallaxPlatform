using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour

{
    [SerializeField]
        int attackDamage;
    [SerializeField]
        float attackRange;
    [SerializeField]
        LayerMask targetLayer;
    [SerializeField]
         GameObject AttackPoint;

    public bool isAttacking = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Collider2D[] hitResults = Physics2D.OverlapCircleAll(AttackPoint.transform.position, attackRange, targetLayer);

        if (hitResults == null)
        {
            return; //EĞER KİMSEYE VURMUYOSAM ÇIK
        }

        foreach(Collider2D hit in hitResults)
        {
            if (hit.GetComponent<Health>() != null)
            {
                hit.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.transform.position, attackRange);
    }
}
