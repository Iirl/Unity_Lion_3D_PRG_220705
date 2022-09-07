using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// 腳本化物件系統：血量資料。
    /// </summary>
    [CreateAssetMenu(menuName = "AScript/Health Data", fileName ="DataHealth")]
    public class DataHealth : ScriptableObject
    {
        [SerializeField, Header("血量"), Range(0,999)]
        public float hp;
        public float maxHp => hp;
        [SerializeField, Header("是否掉落")]
        public bool isDropItem;
        [SerializeField, Header("掉落物"), NonReorderable,HideInInspector]
        public GameObject[] dropItem;
        [SerializeField, Tooltip("掉落機率"), Range(0,1), HideInInspector]
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