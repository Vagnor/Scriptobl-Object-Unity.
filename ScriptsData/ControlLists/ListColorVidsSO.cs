using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// ���� ������ ��� ����������� ������.
    /// </summary>
    // [CreateAssetMenu(fileName = "ListVidColor.asset", menuName = "���������/����� �����/������� ���� ����� ������")]
    public class ListColorVidsSO : ScriptableObject
    {
        [field: SerializeField] public string[] Structures { get; private set; }
        [SerializeField] private List<ColorVidSO> _listObjects;

        public void AddListVidColors(ColorVidSO vid)
        {
            if (!_listObjects.Contains(vid))
            {
                _listObjects.Add(vid);

                Structures = new string[_listObjects.Count];

                for (int i = 0; i < _listObjects.Count; i++)
                    Structures[i] = _listObjects[i].VidName;

                Debug.Log("'" + vid.VidName + "' ������� �������� � ����!");
            }
            else
            {
                Debug.Log("'" + vid.VidName + "' ������� � ����� � ����, ���������� ����������!");
            }
        }

        public ColorVidSO GetVidColor(string nameVid)
        {
            foreach (var vidSructure in _listObjects)
            {
                if (vidSructure.VidName == nameVid)
                    return vidSructure;
            }

            return null;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(ListColorVidsSO))]
    class ListColorVidsSOHelperEditor : Editor
    {
        bool showPosition = true;
        public override void OnInspectorGUI()
        {
            ListColorVidsSO targetObject = (ListColorVidsSO)target;

            EditorGUILayout.LabelField("  ", "���� ����� ������");

            EditorGUILayout.Space();
            showPosition = EditorGUILayout.Foldout(showPosition, "������");
            if (showPosition)
                for (int i = 0; i < targetObject.Structures.Length; i++)
                    EditorGUILayout.DelayedTextField((i + 1) + ": ", targetObject.Structures[i]);

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
            base.SaveChanges();
        }
    }
#endif
}