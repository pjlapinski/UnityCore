﻿// Source: https://github.com/adammyhre/Unity-Utils/blob/master/UnityUtils/Scripts/Hotkeys/Editor/SaveSceneAndProject.cs

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public static class SaveSceneAndProject
{
    [MenuItem("File/Save Scene And Project %#&s")]
    public static void FunctionSaveSceneAndProject()
    {
        EditorApplication.ExecuteMenuItem("File/Save");
        EditorApplication.ExecuteMenuItem("File/Save Project");
        Debug.Log("Saved scene and project");
    }
}
#endif