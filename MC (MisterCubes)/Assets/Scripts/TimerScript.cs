using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text Timer;
    public bool TimerON = false;
    private float time = 3;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        //TimerON = gameController.GetComponent<PathDrawing>().CheckStartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (TimerON == false)
        {
            TimerON = gameController.GetComponent<PathDrawing>().CheckStartTimer();
            
        }
        else if (TimerON == true)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                UpdateTimer(time);
            }
            else
            {
                time = 0;
                TimerON = false;
                
            }
        }
       
        if (gameController.GetComponent<PathDrawing>().newGameFlag && !Input.GetMouseButton(0) && gameController.GetComponent<PathDrawing>().linePositions.Count > 1) 
        {
            time = 3;
            TimerON = true;
            gameController.GetComponent<PathDrawing>().newGameFlag = false;

        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);
        Timer.text = seconds.ToString();
    }

    public float GetTime()
    {
        return time;
    }

    public void SetTime(float seconds)
    {
        time = seconds;
    }
}
