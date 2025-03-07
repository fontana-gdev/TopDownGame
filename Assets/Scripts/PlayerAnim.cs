using System;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    [Header("Combat settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    
    private Player player;
    private Animator animator;
    private Fishing fishing;

    private bool onHitRecovery;
    private float hitRecoveryTime = 1.5f;
    private float recoveryCountdown;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        fishing = FindObjectOfType<Fishing>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
        HitRecoveryControl();
    }

    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.IsRolling)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("rolling"))
                {
                    animator.SetTrigger("isRoll");    
                }
            }
            else
            {
                animator.ResetTrigger("isRoll");
                animator.SetInteger("transition", (int) PlayerAnims.Walking);
            }
        }
        else
        {
            animator.SetInteger("transition", (int) PlayerAnims.Idle);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.IsCutting)
        {
            animator.SetInteger("transition", (int) PlayerAnims.Cutting);
        }
        else if (player.IsDigging)
        {
            animator.SetInteger("transition", (int) PlayerAnims.Digging);
        }
        else if (player.IsWatering)
        {
            animator.SetInteger("transition", (int) PlayerAnims.Watering);
        }
        else if (player.IsStrikingWithSword)
        {
            animator.SetInteger("transition", (int) PlayerAnims.Striking);
        }
    }

    void OnRun()
    {
        if (player.IsRunning && player.direction.sqrMagnitude > 0)
        {
            animator.SetInteger("transition", (int) PlayerAnims.Running);
        }
    }
    #endregion

    #region ToolsUsage
    public void OnCastingStarted()
    {
        player.actionsBlocked = true;
        animator.SetTrigger("isCasting");
    }
    
    public void OnCastingEnded()
    {
        fishing.OnFishingFinished();
        player.actionsBlocked = false;
    }
    
    public void OnHammeringStarted()
    {
        animator.SetBool("isHammering", true);
        player.actionsBlocked = true;
    }

    public void OnHammeringEnded()
    {
        animator.SetBool("isHammering", false);
        player.actionsBlocked = false;
    }
    #endregion

    #region Combat
    public void OnHit()
    {
        if (!onHitRecovery)
        {
            animator.SetTrigger("gotHit");    
            onHitRecovery = true;
        }
    }
    
    private void HitRecoveryControl()
    {
        if (onHitRecovery)
        {
            recoveryCountdown += Time.deltaTime;
            if (recoveryCountdown >= hitRecoveryTime)
            {
                onHitRecovery = false;
                recoveryCountdown = 0;
            }
        }
    }

    // Called from attack/cutting animation 
    public void OnAttack()
    {
       Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
       if (hitEnemy)
       {
           var damage = player.IsCutting ? 1 : 2;
           hitEnemy.GetComponent<Skeleton>().TakeDamage(damage);
       }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    #endregion
    
    
}