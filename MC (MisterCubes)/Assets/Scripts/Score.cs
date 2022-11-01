using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text HighScore;
    public Text ScoreTimer;
    public float ScoreNumber = 0f;
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GetComponent<CubeMoving>().IsItTimeToMove())
        {
            ScoreNumber += 0.05f;
            int i = (int)ScoreNumber;
            ScoreTimer.text = i.ToString();
        }
    }
}
