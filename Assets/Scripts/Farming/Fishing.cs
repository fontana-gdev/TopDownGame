using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fishing : MonoBehaviour
{

    [SerializeField] private int successPercentage;
    [SerializeField] private GameObject fishPrefab;
    
    private bool playerInRange;
    private PlayerItens playerItens;
    private PlayerAnim playerAnim;
    private Player player;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            playerAnim.OnCastingStarted();
        }
    }

    public void OnFishingFinished()
    {
        int randomValue = Random.Range(1, 100);
        if (randomValue <= successPercentage)
        {
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-3, -1), 0f, 0f), Quaternion.identity);
        }
        else
        {
            // Failed
            Debug.Log("Fishing Failed!");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
