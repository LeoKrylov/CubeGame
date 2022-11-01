using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathDrawing : MonoBehaviour
{
    private LineRenderer lineRender;

    public GameObject lineObject;
    public GameObject currentLine;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> linePositions;

    public bool startTimer = false;
    public bool checker = false;

    public Button RestartButton;

    public bool newGameFlag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checker == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DrawTheLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 path = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (linePositions.Count > 0)
                {
                    if (Vector2.Distance(path, linePositions[linePositions.Count - 1]) > 100f)
                    {
                        ExpandTheLine(path);
                    }
                }
            }
            if (!newGameFlag)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    checker = true;
                }
                
            }
        }
    }

    void DrawTheLine() 
    {
        
        currentLine = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
        lineRender = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        linePositions.Clear();
        linePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        linePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRender.SetPosition(0, linePositions[0]);
        lineRender.SetPosition(1, linePositions[1]);
        edgeCollider.points = linePositions.ToArray();
    }

    void ExpandTheLine(Vector2 newLine)
    {
        linePositions.Add(newLine);
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount - 1, newLine);
        edgeCollider.points = linePositions.ToArray();
    }

    public bool CheckStartTimer()
    {

        if (!Input.GetMouseButton(0) && linePositions.Count > 1)
        {
            return true;
        }
        else 
            return false;
    }

    public void NewGame()
    {
        checker = false;
        newGameFlag = true;
    }
}
