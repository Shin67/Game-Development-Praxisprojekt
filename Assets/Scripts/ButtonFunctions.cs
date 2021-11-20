using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Button geglickt - Spiel beenden");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //only for the editor
        #else
        Application.Quit(); //closes all other applications
        #endif
    }
    public void Startlevel()
    {
        SceneManager.LoadScene("Name der ersten Map");
    }
}