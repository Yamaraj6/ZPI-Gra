using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CameraMove))]
public class CameraEditor : Editor {

    private int leftValue = -30;
    private int rightValue = 30;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraMove camera = target as CameraMove;

        if (camera.bossLevel)
        {
            GUILayout.Label("Boss area parameters:");
            camera.boss = (GameObject) EditorGUILayout.ObjectField("Boss", camera.boss, typeof(UnityEngine.Object), true);
            camera.cameraOffset_boss_fromPlayer = EditorGUILayout.Vector3Field("Camera offset (Player)", camera.cameraOffset_boss_fromPlayer);
            camera.cameraOffset_boss_fromBoss = EditorGUILayout.Vector3Field("Camera offset (Boss)", camera.cameraOffset_boss_fromBoss);
        }
    }

}
