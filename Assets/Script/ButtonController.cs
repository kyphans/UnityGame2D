using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{
    
    public void NextLevelButton(int index)
        {
            Application.LoadLevel(index);
        }
    
    public void NextLevelButton(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void ChangeToNextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuidGame(){
        Application.Quit();    
    }
}
