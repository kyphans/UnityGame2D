using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void QuidGame(){
        Application.Quit();    
    }
}
