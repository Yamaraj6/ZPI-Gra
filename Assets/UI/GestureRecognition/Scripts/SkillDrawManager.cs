using PDollarGestureRecognizer;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrawManager : MonoBehaviour
{    
    [SerializeField]
    private ParticleSystem particle;

    private Rect drawArea;
    private ParticleSystem.EmissionModule emissionModule;
    private RuntimePlatform platform;
    private List<Point> points = new List<Point>();    
    private Vector3 virtualKeyPosition;       

    private const int STROKE_ID = 0;

    void Start()
    {
        platform = Application.platform;
        RectTransform _rt = this.GetComponent<RectTransform>();                
        drawArea = ConvertToScreenSize(_rt);
        emissionModule = particle.emission;
        particle.gameObject.SetActive(false);        
    }

    void Update()
    {
        virtualKeyPosition = GetTouchPosition();
        particle.transform.position = virtualKeyPosition;

        if (drawArea.Contains(virtualKeyPosition))
        {            
            particle.gameObject.SetActive(true);
            emissionModule.enabled = true;
                  
            if (Input.GetMouseButton(0))
            {
                AddDrawnPoints();
            }            
        }
        else
        {
            emissionModule.enabled = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishDraw();            
        }
    }    

    private void AddDrawnPoints()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, STROKE_ID));        
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
        if(points.Count > 0 && HasDifferentPoints())
        {
            Messenger<List<Point>>.Broadcast(GameEvent.SKILL_DRAW, points);
        }
        points.Clear();
        particle.Clear();
        particle.gameObject.SetActive(false);
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

    private bool HasDifferentPoints()
    {
        bool _hasDifferentPoints = false;
        for (int i = 1; i < this.points.Count && !_hasDifferentPoints; i++)
        {
            if (!Mathf.Approximately(points[i - 1].X, points[i].X) || !Mathf.Approximately(points[i - 1].Y, points[i].Y))
            {
                _hasDifferentPoints = true;
            }
        }
        return _hasDifferentPoints;
    }
}