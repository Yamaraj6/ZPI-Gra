using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDrawManager : MonoBehaviour
{
    [SerializeField]
    private Transform gestureOnScreenPrefab;

    private RuntimePlatform platform;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;
    private Rect drawArea;
    private int vertexCount = 0;

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private bool recognized;

    void Start()
    {
        platform = Application.platform;
        RectTransform rt = this.GetComponent<RectTransform>();

        drawArea = ConvertToScreenSize(rt);
        Debug.Log(string.Format("DrawArea Position: ({0}, {1})", drawArea.position.x, drawArea.position.y));
        Debug.Log(string.Format("DrawArea Size: ({0}, {1})", drawArea.size.x, drawArea.size.y));
    }

    void Update()
    {
        virtualKeyPosition = GetTouchPosition();

        if (drawArea.Contains(virtualKeyPosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (recognized)
                {
                    ClearDrawPanel();
                }
                ++strokeId;

                InstantiateNewLine();
                vertexCount = 0;
            }

            if (Input.GetMouseButton(0))
            {
                AddDrawnPoints();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            FinishDraw();            
        }        
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 virtualKeyPosition = Vector3.zero;

        if (platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);                
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);                
            }
        }

        return virtualKeyPosition;
    }

    private void ClearDrawPanel()
    {
        recognized = false;
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {
            lineRenderer.positionCount = 0;
            Destroy(lineRenderer.gameObject);
        }

        gestureLinesRenderer.Clear();
    }

    private void AddDrawnPoints()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
        currentGestureLineRenderer.positionCount = ++vertexCount;
        currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, virtualKeyPosition.z)));
        Debug.Log(string.Format("Line Position: ({0}, {1}, {2})", currentGestureLineRenderer.GetPosition(vertexCount - 1).x, currentGestureLineRenderer.GetPosition(vertexCount - 1).y, currentGestureLineRenderer.GetPosition(vertexCount - 1).z));
    }

    private void InstantiateNewLine()
    {
        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;        
        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
        gestureLinesRenderer.Add(currentGestureLineRenderer);       
    }

    private void FinishDraw()
    {
        recognized = true;
        Messenger<List<Point>>.Broadcast(GameEvent.SKILL_DRAW, points);
    }

    private Rect ConvertToScreenSize(RectTransform rt)
    {        
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector2 scaledSize = new Vector2(rt.rect.size.x * rt.lossyScale.x, rt.rect.size.y * rt.lossyScale.y);
        return new Rect(corners[0], scaledSize);
    }
}