using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRecognizer : MonoBehaviour {

    public SpellCaster spellCaster;

    private List<Gesture> trainingSet = new List<Gesture>();
    private const string TRAINING_SET_PATH = "GestureSet/10-stylus-MEDIUM/";

    void Awake()
    {
        Messenger<List<Point>>.AddListener(GameEvent.SKILL_DRAW, OnSkillDraw);    
    }

    void OnDestroy()
    {
        Messenger<List<Point>>.RemoveListener(GameEvent.SKILL_DRAW, OnSkillDraw);    
    }
    
    void Start ()
    {
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>(TRAINING_SET_PATH);
        foreach (TextAsset gestureXml in gesturesXml)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        }
        Debug.Log(string.Format("Loaded Gestures count: {0}", trainingSet.Count));
    }		

    private void OnSkillDraw(List<Point> points)
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        spellCaster.CastSpell(gestureResult.GestureClass);
        Debug.Log(gestureResult.GestureClass + " " + System.Math.Round(gestureResult.Score, 2));
    }
}
