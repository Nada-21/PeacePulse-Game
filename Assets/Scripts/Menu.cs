using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button button1; // define button. 
    public int sceneindex;

    void Start()
    {
        button1.onClick.AddListener(ButtonAction); // when we click on button ,execute 'ButtonAction function'.
    }

    void ButtonAction()
    {
        SceneManager.LoadScene(sceneindex);  
        
    }
}
