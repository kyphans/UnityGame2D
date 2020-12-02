using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void Quit(){
        Debug.Log("Application Quit!");
        Application.Quit();
    }

    public void Retry(){
        Debug.Log("Retry");
        SceneManager.LoadScene("Level1");
    }
}
