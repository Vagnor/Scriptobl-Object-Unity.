using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Structure.Model
{
    /// <summary>
    /// ���� ����� ������ ��������� ��� ����������� ������.
    /// </summary>
    // [CreateAssetMenu(fileName = "ListVidPartsCharacter.asset", menuName = "���������/����� �����/������� ���� ����� ������ ���������")]
    public class ListPartsCharacterVidSO : ScriptableObject
    {
        [field: SerializeField] public string[] Structures { get; private set; }
        [SerializeField] private List<PartsCharacterVidSO> _listObjects;

        public void AddListVidPartsCharacter(PartsCharacterVidSO vid)
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

        public PartsCharacterVidSO GetVidPartsCharacter(string nameVid)
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
    [CustomEditor(typeof(ListPartsCharacterVidSO))]
    class ListPartsCharacterVidSOHelperEditor : Editor
    {
        bool showPosition = true;
        public override void OnInspectorGUI()
        {
            ListPartsCharacterVidSO targetObject = (ListPartsCharacterVidSO)target;

            EditorGUILayout.LabelField("  ", "���� ����� ������ ���������");
            DrawDefaultInspector();
            EditorGUILayout.Space();

            showPosition = EditorGUILayout.Foldout (showPosition, "������");
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
