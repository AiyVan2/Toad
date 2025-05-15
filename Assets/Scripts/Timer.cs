using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    private float timeElapsed = 0f;
    private bool isRunning = false;


    void Update()
    {
        StartTimer();
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            timer.text = FormatTime(timeElapsed);
        } 
    }

    public void StartTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRunning)
        {
            isRunning = true;
            timeElapsed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isRunning = false;
        }
    }


    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}