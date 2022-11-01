using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public  Transform Cube;
    public float speed = 300f;
    private Vector2 direction = new Vector2(1,0);
    private bool check = false;
    public GameObject gameController;
    private bool stop = false;
    public Text HighScore;
    public Text ScoreTimer;

    void Start()
    {
        check = IsItTimeToMove();
    }

    // Update is called once per frame
    void Update()
    {
        check = IsItTimeToMove();
        if (gameController.GetComponent<PathDrawing>().CheckStartTimer())
        {
            FirstMovement();
            stopMethod();
        }
            
        if (check && !stop )
        {
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
                direction = new Vector2(1, 0);
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
                direction = new Vector2(-1, 0);
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0)
                direction = new Vector2(0, 1);
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0)
                direction = new Vector2(0, -1);

            transform.Translate(Time.deltaTime * speed * direction.x, Time.deltaTime * speed * direction.y, 0);
        }

        
    }

    public bool IsItTimeToMove()
    {
        bool itIsTime = false;
        
        if (GetComponent<TimerScript>().GetTime() == 0)
        {
            itIsTime = true;
        }
        return itIsTime;
    }

    private void FirstMovement()
    {
        if (GetComponent<TimerScript>().GetTime() == 3)
        {
            Cube.position = gameController.GetComponent<PathDrawing>().linePositions[0];
            stop = false;
            
        }
    }

    public void stopMethod()
    {
        int lastLineNumber = gameController.GetComponent<PathDrawing>().linePositions.Count - 1;
        Vector2 lastLine = gameController.GetComponent<PathDrawing>().linePositions[lastLineNumber];
        if (Cube.position.x < lastLine.x + 20 && Cube.position.x > lastLine.x - 20 && Cube.position.y < lastLine.y + 20 && Cube.position.y > lastLine.y - 20) 
        {
            stop = true;
            if (float.Parse(HighScore.text) < float.Parse(ScoreTimer.text))
                HighScore.text = ScoreTimer.text;
        }
    }

    public void PauseMethod()
    {
        stop = true;
    }

    public void PlayMethod()
    {
        stop = false;
    }

    public void Restart()
    {
        stop = false;
        direction = new Vector2(1, 0);
        GetComponent<Score>().ScoreNumber = 0;
        GetComponent<TimerScript>().TimerON = true;
        GetComponent<TimerScript>().SetTime(3);
        FirstMovement();
    }

    public void NewGame()
    {
        stop = true;
        direction = new Vector2(1, 0);
        LineRenderer line = FindObjectOfType<LineRenderer>();
        Destroy(line.gameObject);
        gameController.GetComponent<PathDrawing>().NewGame();
        gameController.GetComponent<PathDrawing>().linePositions.Clear();
        GetComponent<TimerScript>().SetTime(3);
        HighScore.text = "0";
        GetComponent<Score>().ScoreNumber = 0;
        ScoreTimer.text = "0";
    }
    
}
