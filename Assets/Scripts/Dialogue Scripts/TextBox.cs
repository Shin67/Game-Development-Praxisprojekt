using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TextBox{


    public TextBoxType typeOfBox;

    //In case of SentenceBox
    public string name;
    public string text;

    //In case of EventBox
    public UnityEvent toExecute;

    //In case of ChoiceBox
    public Reply[] replies;
}

public enum TextBoxType {
    SentenceBox,
    QuestionBox,
    EventBox
}