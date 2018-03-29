using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDrawManager : MonoBehaviour
{
    public Transform gestureOnScreenPrefab;
    public GameObject gestureArea;

    private RuntimePlatform platform;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;
    private Rect drawArea;
    private int vertexCount = 0;

    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private bool recognized;

    void Start ()
    {
        platform = Application.platform;        
        RectTransform rt = gestureArea.GetComponent<RectTransform>();       

        drawArea = RectTransformToWorld(rt);                
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        }
        Debug.Log(string.Format("Loaded Gestures count: {0}", trainingSet.Count));
    }
		
	void Update ()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
                Debug.Log(string.Format("VirtualKeyPosition - Position: ({0}, {1})", virtualKeyPosition.x, virtualKeyPosition.y));
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                Debug.Log(string.Format("VirtualKeyPosition - Position: ({0}, {1})", virtualKeyPosition.x, virtualKeyPosition.y));
            }
        }            

        if(drawArea.Contains(virtualKeyPosition))
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(recognized)
                {
                    recognized = false;
                    strokeId = -1;

                    points.Clear();

                    foreach(LineRenderer lineRenderer in gestureLinesRenderer)
                    {
                        lineRenderer.SetVertexCount(0);
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }

                ++strokeId;

                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                Debug.Log(string.Format("TmpGesture - Position: ({0}, {1})", tmpGesture.position.x, tmpGesture.position.y));
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }

            if(Input.GetMouseButton(0))
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
        }        

        if(Input.GetMouseButtonUp(0))
        {
            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            Debug.Log(gestureResult.GestureClass + " " + System.Math.Round(gestureResult.Score, 2));
        }
	}    

    private Rect RectTransformToWorld(RectTransform rt)
    {        
        //Trzeba zmienić sposób pozycjonowania Recta!
        return new Rect(Screen.width / 2, Screen.height / 4, Screen.width / 2, Screen.height / 2);
    }    
}
