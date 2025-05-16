using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void normal()
    {
        SceneManager.LoadScene("Normal");
    }
    public void speedrun()
    {
        SceneManager.LoadScene("Speedrun");
    }
}
