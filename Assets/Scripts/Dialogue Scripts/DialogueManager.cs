using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public Queue<TextBox> textBoxes;
    public KeyCode interactionKey;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    //Wieviel Frames pro Textbox gewartet werden müssen bevor man skippen kann
    public int dialogueDelay;
    public bool activeDialogue;
    public TextBoxType currentBoxType;
    private TextBox currentBox;
    private int maxAcceptableAnswer;

    // Start is called before the first frame update
    void Start()
    {
        //Eine Queue nutzen ist praktisch für Konversationen, da FIFO.
        textBoxes = new Queue<TextBox>();
    }

    //Etwas crappy, dass ich das hier rufe. Besser wäre wenn das interactable Script sofort den Interact-Button mitgibt, sodass wir keine 2 seperaten InteractKeys haben maybe? Not sure.
    void Update(){

        if (currentBoxType == TextBoxType.SentenceBox || currentBoxType == TextBoxType.EventBox){
            if (dialogueDelay <= 0 && Input.GetKeyDown(interactionKey)){
                DisplayNextBox();
            }
        }

        //Dialogue Options
        if (currentBoxType == TextBoxType.QuestionBox){
            int input = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                input = 1;
            } else if (Input.GetKeyDown(KeyCode.Alpha2)){
                input = 2;
            } else if (Input.GetKeyDown(KeyCode.Alpha3)){
                input = 3;
            } else if (Input.GetKeyDown(KeyCode.Alpha4)){
                input = 4;
            } else if (Input.GetKeyDown(KeyCode.Alpha5)){
                input = 5;
            }

            if (input != 0 && input <= maxAcceptableAnswer){
                Reply answer = currentBox.replies[input-1];
                nameText.text = answer.reactingNPCName;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(answer.reaction));
                currentBoxType = TextBoxType.SentenceBox;
            }

        }
        
        
        if (dialogueDelay > 0){
            dialogueDelay -= 1;
        }
    }

    //Start Dialogue Methode, wird aus anderem Skript gerufen
    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("IsVisible", true);
        activeDialogue = true;
        textBoxes.Clear();

        foreach (TextBox t in dialogue.textBoxen){
            Debug.Log("Enqueing TextBox of type: " + t.typeOfBox);
            textBoxes.Enqueue(t);
        }
        DisplayNextBox();
    }
    
    public void DisplayNextBox(){
        if (textBoxes.Count == 0){
            EndDialogue();
            return;
        }
        //60 frames of delay
        dialogueDelay = 60;
        TextBox box = textBoxes.Dequeue();
        currentBoxType = box.typeOfBox;
        currentBox = box;
        //Je nach verschiedener Box muss was anderes gemacht werden
        switch (currentBoxType){
            //Bei einer SentenceBox kann die nächste Box einfach getyped werden
            case TextBoxType.SentenceBox:
                nameText.text = box.name;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(box.text));
                break;
            //Bei einer QuestionBox muss man zuerst die Option angeben, und festlegen was die höchste erlaubte Antwort ist.
            //In der Update Methode wird im Fall dass currentBoxType == QuestionBox abgefragt, ob die Tasten 1,2,3,4...len gedrückt werden
            //Wenn eine zulässige Auswahl getroffen wurde, dann wird die zugehörige Antwort angezeigt und der currentBoxType auf SentenceBox gesetzt, damit man die nächste Box anzeigen kann
            case TextBoxType.QuestionBox:
                string options = "";
                int len = box.replies.Length;
                for (int i = 0; i < len; i++){
                    options += (i+1) + " : " + box.replies[i].answer + "\n";
                }
                maxAcceptableAnswer = len;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(options));
                break;
            //Bei einer EventBox muss das Event nur invoked werden. Die bestehende TextBox wird nicht geändert.
            case TextBoxType.EventBox:
                box.toExecute.Invoke();
                break;
        }
        
    }

    //Render each letter by itself and not all at once
    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            //Wait for waitTime many seconds
            float waitTime = 0.03f;
            yield return new WaitForSeconds(waitTime);
        }
    }

    void EndDialogue(){
        activeDialogue = false;
        animator.SetBool("IsVisible", false);
        Debug.Log("End of Dialogue");
    }

}
