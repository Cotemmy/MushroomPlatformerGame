using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text playerDisplay;
    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
         SceneManager.LoadScene("EntryScene");
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    
}
