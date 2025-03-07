using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    [SerializeField] private float walkSpeed;
    float initialWalkSpeed;
    [SerializeField] List<Transform> paths = new();
    private int pathIndex;
    private GameObject player;
    Animator animator;

    private void Start()
    {
        initialWalkSpeed = walkSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovesTowardsPaths();
    }

    private void MovesTowardsPaths()
    {
        if (DialogueControl.instance.IsShowing)
        {
            // Porque não funciona??
            // Vector2 playerDirection = player.transform.position - transform.position;
            // transform.eulerAngles = new Vector2(0, playerDirection.x > 0 ? 0 : 180);
            walkSpeed = 0;
            animator.SetBool(IsWalking, false);
        }
        else
        {
            walkSpeed = initialWalkSpeed;
            animator.SetBool(IsWalking, true);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, paths[pathIndex].position, walkSpeed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, paths[pathIndex].position) < 0.1f)
        {
            if (pathIndex == paths.Count - 1)
            {
                pathIndex = 0;
            }
            else
            {
                //pathIndex++;
                pathIndex = Random.Range(0, paths.Count - 1);
            }
        }
        
        // Se está indo para a direita o resultado será positivo, se for esquerda, negativo
        Vector2 direction = paths[pathIndex].position - transform.position;
        transform.eulerAngles = new Vector2(0, direction.x > 0 ? 0 : 180);
    }
}
