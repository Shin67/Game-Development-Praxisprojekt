using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for a single dialogue
//Needs to be serializable to show up in Unity
//This comment is useless lol
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
