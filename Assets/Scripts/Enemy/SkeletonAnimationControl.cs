using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class SkeletonAnimationControl : MonoBehaviour
{
    
    [SerializeField] Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Skeleton skeleton; 
    
    private Player player;
    private PlayerAnim playerAnim;
    
    // Start is called before the first frame update
    void Awake()
    {
         player = FindObjectOfType<Player>();
         playerAnim = player.GetComponent<PlayerAnim>();
    }

    public void PlayAnimation(SkeletonAnims anim)
    {
        animator.SetInteger("transition", (int) anim);
    }

    // Called from attack animation
    public void Attack()
    {
        if (skeleton.IsDead) return;
        
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (hit)
        {
            playerAnim.OnHit();
        }
    }
    
    // Called from attack animation
    public void TurnToPlayer()
    {
        var xPos = player.transform.position.x - skeleton.transform.position.x;
        skeleton.transform.eulerAngles = new Vector2(0, xPos > 0 ? 0 : 180);
    }

    public void TakeDamage()
    {
        animator.SetTrigger("gotHit");
    }
    
    public void Die()
    {
        animator.SetTrigger("died");
        Destroy(skeleton.gameObject, 2f);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
