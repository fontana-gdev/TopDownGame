using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC_Dialogue : MonoBehaviour
{

    [SerializeField] private float dialogueRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private DialogueSettings dialogueSettings;
    [SerializeField] private bool blockPlayer;
    
    private bool playerInRange;
    
    private List<string> sentences = new List<string>();

    private void Start()
    {
        InitializeNpcSpeechTexts();
    }

    // Chamado uma vez a cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            // DialogueControl.instance.StartChatting(sentences.ToArray());
            DialogueControl.instance.StartDialogue(dialogueSettings.dialogues, blockPlayer);
        }
    }

    void InitializeNpcSpeechTexts()
    {
        foreach (var dialogue in dialogueSettings.dialogues)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.Language.pt:
                    sentences.Add(dialogue.sentence.portuguese);
                    break;
                case DialogueControl.Language.en:
                    sentences.Add(dialogue.sentence.english);
                    break;
                case DialogueControl.Language.es:
                    sentences.Add(dialogue.sentence.spanish);
                    break;
                default:
                    sentences.Add(dialogue.sentence.portuguese);
                    break;
            }
           
        }
    }

    // Utilizado pela física. Independente da frame rate.
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D playerProximity = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        if (playerProximity)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
            DialogueControl.instance.StopDialogue();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
