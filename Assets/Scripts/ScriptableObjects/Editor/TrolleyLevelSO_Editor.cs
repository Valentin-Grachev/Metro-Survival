using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrolleyLevelsSO))]
public class TrolleyLevelsSOEditor : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TrolleyLevelsSO view = (TrolleyLevelsSO)target;
        EditorGUILayout.LabelField("Upgrade functions (X = level):");



        // Отображение функции цены
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Price = ", GUILayout.MaxWidth(50));
        view.price0 = EditorGUILayout.FloatField(view.price0, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("+", GUILayout.MaxWidth(10));
        view.price1 = EditorGUILayout.FloatField(view.price1, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("X +", GUILayout.MaxWidth(20));
        view.price2 = EditorGUILayout.FloatField(view.price2, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("X^2", GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();


        // Отображение функции здоровья
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health = ", GUILayout.MaxWidth(50));
        view.health0 = EditorGUILayout.FloatField(view.health0, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("+", GUILayout.MaxWidth(10));
        view.health1 = EditorGUILayout.FloatField(view.health1, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("X +", GUILayout.MaxWidth(20));
        view.health2 = EditorGUILayout.FloatField(view.health2, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("X^2", GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();




    }


}
