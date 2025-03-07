using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int totalHealth;
    [SerializeField] float awarenessDistance;
    
    [Header("Components")]
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Image healthBar;
    [SerializeField] LayerMask playerLayer;
    
    private Player player;
    private SkeletonAnimationControl animationControl;
    private bool playerInAttackRange;
    private int currentHealth;
    private bool isDead;
    private bool detectedPlayer;

    public bool IsDead => isDead;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        animationControl = GetComponentInChildren<SkeletonAnimationControl>();
    }

    void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentHealth = totalHealth;
    }

    void Update()
    {
        if (isDead) return;
        
        playerInAttackRange = Vector2.Distance(transform.position, player.transform.position) <= navMeshAgent.stoppingDistance;
        if (playerInAttackRange)
        {
            animationControl.PlayAnimation(SkeletonAnims.Attacking);
        }
        else
        {
            // float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            // if (distanceFromPlayer <= awarenessDistance)
            if (detectedPlayer)
            {
                navMeshAgent.speed = 2;
                navMeshAgent.SetDestination(player.transform.position);
                
                TurnToPlayer();
                
                animationControl.PlayAnimation(SkeletonAnims.Walking);
            }
            else
            {
                animationControl.PlayAnimation(SkeletonAnims.Idle);
                navMeshAgent.speed = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        var playerDetection = Physics2D.OverlapCircle(transform.position, awarenessDistance, playerLayer);
        detectedPlayer = playerDetection;
    }

    private void TurnToPlayer()
    {
        var xPos = player.transform.position.x - transform.position.x;
        transform.eulerAngles = new Vector2(0, xPos > 0 ? 0 : 180);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / (float) totalHealth;

        if (currentHealth > 0)
        {
            animationControl.TakeDamage();
        }
        else
        {
            animationControl.Die();
            isDead = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, awarenessDistance);
    }
}
