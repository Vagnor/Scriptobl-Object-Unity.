using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// ���� �������� ��� ����������� ������.
    /// </summary>
    //   [CreateAssetMenu(fileName = "ListVidStructure.asset", menuName = "���������/����� �����/������� ���� ����� ��������")]
    public class ListVidStructuresSO : ScriptableObject
    {
        [field: SerializeField]  public string[] Structures {  get; private set; }
        [SerializeField] private List<VidStructureSO> _listStructeres;

        public void AddListStructures(VidStructureSO vid)
        {
            if (!_listStructeres.Contains(vid))
            {
                _listStructeres.Add(vid);

                Structures = new string[_listStructeres.Count];

                for (int i = 0; i < _listStructeres.Count; i++)
                    Structures[i] = _listStructeres[i].NameStructure;

                Debug.Log("'" + vid.NameStructure + "' ������� �������� � ����! ����� �����: " + vid.NumStructure);                
            }
            else
            {
                Debug.Log("'" + vid.NameStructure + "' ������� � ����� � ����, ���������� ����������!");
            }

            if (vid.NumStructure<=0)
                vid.AddSumm(_listStructeres.IndexOf(vid) + 1);
        }

        public VidStructureSO GetStructure(string nameVid)
        {
            foreach (var vidSructure in _listStructeres)
            {
                if (vidSructure.NameStructure == nameVid)
                    return vidSructure;
            }

            return null;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ListVidStructuresSO))]
    class ListVidStructuresSOHelperEditor : Editor
    {
        bool showPosition = true;
        public override void OnInspectorGUI()
        {
            ListVidStructuresSO targetObject = (ListVidStructuresSO)target;

            EditorGUILayout.LabelField("  ", "���� ����� ��������");

            EditorGUILayout.Space();
            showPosition = EditorGUILayout.Foldout(showPosition, "������");
            if (showPosition)
                for (int i = 0; i < targetObject.Structures.Length; i++)
                    EditorGUILayout.DelayedTextField((i + 1) + ": ", targetObject.Structures[i]);

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
