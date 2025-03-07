using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    [System.Serializable]
    public enum Language
    {
        pt,
        en,
        es
    }
    
    public static DialogueControl instance;
    
    [Header("Components")]
    public GameObject dialogueBoxObj;
    public Image profileSprite;
    public Text currentSpeechText;
    public Text actorNameText;
    
    [Header("Settings")] 
    public float typingSpeed;
    public Language language;
    
    // Variáveis de controle
    private bool isShowing;
    private bool playerIsBlocked;
    private int index; // Index das sentenças
    private List<Sentences> dialogues;

    public bool IsShowing => isShowing;

    private Player player;

    // Chamado antes de todos os Start() na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }
    
    IEnumerator TypeSentence()
    {
        var sentence = GetNextSentence();
        foreach (char letter in sentence)
        {
            // Exibir cada letra na caixa de diálogo pausando conforme 
            currentSpeechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    public void StartDialogue(List<Sentences> speechDialogues, bool blockPlayerActions)
    {
        if (!isShowing)
        {
            dialogueBoxObj.SetActive(true);
            dialogues = speechDialogues;
            StartCoroutine(TypeSentence());
            isShowing = true;
            
            playerIsBlocked = blockPlayerActions;
            if (playerIsBlocked)
            {
                player.actionsBlocked = true;    
            }
            
        }
    }
    
    public void StopDialogue()
    {
        currentSpeechText.text = "";
        index = 0;
        dialogueBoxObj.SetActive(false);
        dialogues = null;
        isShowing = false;
        
        if (playerIsBlocked)
        {
            player.actionsBlocked = false;
        }
    }
    
    public void NextDialogue()
    {
        string sentence = GetNextSentence();

        if (currentSpeechText.text == sentence)
        {
            if (index < dialogues.Count - 1)
            {
                index++;
                var dialogue = dialogues[index];
                currentSpeechText.text = "";
                profileSprite.sprite = dialogue.profile;
                actorNameText.text = dialogue.actorName;
                StartCoroutine(TypeSentence());
            }
            else // Acabaram os textos
            {
                StopDialogue();
            }
        }
        else
        {
        }
    }

    private string GetNextSentence()
    {
        var sentencesByLang = dialogues[index].sentence;
        switch (instance.language)
        {
            case Language.pt:
                return sentencesByLang.portuguese;
            case Language.en:
                return sentencesByLang.english;
            case Language.es:
                return sentencesByLang.spanish;
            default:
                return sentencesByLang.portuguese;
        }
    }
}
