using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropList : MonoBehaviour
{
    public Dropdown dropdown;

   
    void Start()
    {
        // Add listener for when the dropdown's value changes
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    // This method is called when the dropdown changes
    void DropdownValueChanged(Dropdown dropdown)
    {
        // Check the selected option
        string selectedOption = dropdown.options[dropdown.value].text;
        Debug.Log("Selected option: " + selectedOption); 

        if (selectedOption == "Light"){
            SceneManager.LoadScene("ExposureLight");
        }

        if (selectedOption == "Night"){
            SceneManager.LoadScene("ExposureNight");
        }
    }

}
