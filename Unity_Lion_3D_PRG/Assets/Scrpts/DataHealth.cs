using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// �}���ƪ���t�ΡG��q��ơC
    /// </summary>
    [CreateAssetMenu(menuName = "AScript/Health Data", fileName ="DataHealth")]
    public class DataHealth : ScriptableObject
    {
        [SerializeField, Header("��q"), Range(0,999)]
        public float hp;
        public float maxHp => hp;
        [SerializeField, Header("�O�_����")]
        public bool isDropItem;
        [SerializeField, Header("������"), NonReorderable,HideInInspector]
        public GameObject[] dropItem;
        [SerializeField, Tooltip("�������v"), Range(0,1), HideInInspector]
        public float dropProb;
    }

    [CustomEditor(typeof(DataHealth))]
    public class HealthEditor : Editor
    {
        SerializedProperty spIsDrop;
        SerializedProperty spItems;
        SerializedProperty spProb;

        private void OnEnable()
        {
            spIsDrop = serializedObject.FindProperty(nameof(DataHealth.isDropItem));
            spItems = serializedObject.FindProperty(nameof(DataHealth.dropItem));
            spProb = serializedObject.FindProperty(nameof(DataHealth.dropProb));
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();
            if (spIsDrop.boolValue)
            {
                EditorGUILayout.PropertyField(spItems.GetArrayElementAtIndex(0));
                EditorGUILayout.PropertyField(spProb);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }

}