using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public Queue<string> sentences;
    public KeyCode interactionKey;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    //Wieviel Frames pro Textbox gewartet werden müssen bevor man skippen kann
    public int dialogueDelay;
    public bool activeDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //Eine Queue nutzen ist praktisch für Konversationen, da FIFO.
        sentences = new Queue<string>();
    }

    //Etwas crappy, dass ich das hier rufe. Besser wäre wenn das interactable Script sofort den Interact-Button mitgibt, sodass wir keine 2 seperaten InteractKeys haben maybe? Not sure.
    void Update(){
        if (dialogueDelay <= 0 && Input.GetKeyDown(interactionKey)){
            DisplayNextSentence();
        }
        if (dialogueDelay > 0){
            dialogueDelay -= 1;
        }
    }

    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("IsVisible", true);
        activeDialogue = true;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            Debug.Log("Enqueing Sentence: " + sentence);
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    
    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }
        dialogueDelay = 60;
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue(){
        activeDialogue = false;
        animator.SetBool("IsVisible", false);
        Debug.Log("End of Dialogue");
    }

}
