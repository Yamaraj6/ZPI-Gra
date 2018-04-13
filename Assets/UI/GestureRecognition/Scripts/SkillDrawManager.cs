using PDollarGestureRecognizer;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrawManager : MonoBehaviour
{    
    [SerializeField]
    private ParticleSystem particle;

    private Rect drawArea;    
    private RuntimePlatform platform;
    private List<Point> points = new List<Point>();
    private bool recognized;
    private int strokeId = -1;
    private int vertexCount = 0;
    private Vector3 virtualKeyPosition;

    void Start()
    {
        platform = Application.platform;
        RectTransform _rt = this.GetComponent<RectTransform>();                
        drawArea = ConvertToScreenSize(_rt);
        particle.gameObject.SetActive(false);
        Debug.Log(string.Format("DrawArea Position: ({0}, {1})", drawArea.position.x, drawArea.position.y));
        Debug.Log(string.Format("DrawArea Size: ({0}, {1})", drawArea.size.x, drawArea.size.y));
    }

    void Update()
    {
        virtualKeyPosition = GetTouchPosition();
        
        if (drawArea.Contains(virtualKeyPosition))
        {            
            particle.transform.position = virtualKeyPosition;            
            particle.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                if (recognized)
                {
                    ClearDrawPanel();
                }
                ++strokeId;                
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

    private void AddDrawnPoints()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));        
    }

    private void ClearDrawPanel()
    {
        recognized = false;
        strokeId = -1;
        points.Clear();

        particle.Clear();
        particle.gameObject.SetActive(false);
    }

    private Rect ConvertToScreenSize(RectTransform rt)
    {        
        Vector3[] _corners = new Vector3[4];
        rt.GetWorldCorners(_corners);
        Vector2 _scaledSize = new Vector2(rt.rect.size.x * rt.lossyScale.x, rt.rect.size.y * rt.lossyScale.y);
        return new Rect(_corners[0], _scaledSize);
    }

    private void FinishDraw()
    {
        recognized = true;
        Messenger<List<Point>>.Broadcast(GameEvent.SKILL_DRAW, points);
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 _virtualKeyPosition = Vector3.zero;

        if (platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                _virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                _virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        return _virtualKeyPosition;
    }
}