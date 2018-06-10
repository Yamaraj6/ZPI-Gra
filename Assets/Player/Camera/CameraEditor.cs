#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CameraMove))]
public class CameraEditor : Editor {

    SerializedProperty bossLevel;
    SerializedProperty boss;
    SerializedProperty cameraOffset_boss_fromPlayer;
    SerializedProperty cameraOffset_boss_fromBoss;

    private void OnEnable()
    {
        bossLevel = serializedObject.FindProperty("bossLevel");
        boss = serializedObject.FindProperty("boss");
        cameraOffset_boss_fromPlayer = serializedObject.FindProperty("cameraOffset_boss_fromPlayer");
        cameraOffset_boss_fromBoss = serializedObject.FindProperty("cameraOffset_boss_fromBoss");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        if (bossLevel.boolValue)
        {
            EditorGUILayout.PropertyField(boss);
            EditorGUILayout.PropertyField(cameraOffset_boss_fromPlayer);
            EditorGUILayout.PropertyField(cameraOffset_boss_fromBoss);
        }
        serializedObject.ApplyModifiedProperties();

        //CameraMove camera = target as CameraMove;

        //if (camera.bossLevel)
        //{
        //    GUILayout.Label("Boss area parameters:");
        //    camera.boss = (GameObject)EditorGUILayout.ObjectField("Boss", camera.boss, typeof(UnityEngine.Object), true);
        //    camera.cameraOffset_boss_fromPlayer = EditorGUILayout.Vector3Field("Camera offset (Player)", camera.cameraOffset_boss_fromPlayer);
        //    camera.cameraOffset_boss_fromBoss = EditorGUILayout.Vector3Field("Camera offset (Boss)", camera.cameraOffset_boss_fromBoss);
        //}
    }

}
#endif