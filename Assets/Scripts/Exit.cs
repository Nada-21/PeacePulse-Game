using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       SceneManager.LoadScene("Home");

    }
}
