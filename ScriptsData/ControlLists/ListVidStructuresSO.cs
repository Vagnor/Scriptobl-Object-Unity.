using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// Лист структур для выподающего списка.
    /// </summary>
    //   [CreateAssetMenu(fileName = "ListVidStructure.asset", menuName = "Системное/Листы видов/Создать лист видов структур")]
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

                Debug.Log("'" + vid.NameStructure + "' успешно добавлен в лист! Задан номер: " + vid.NumStructure);                
            }
            else
            {
                Debug.Log("'" + vid.NameStructure + "' имеется в листе в лист, добавление невозможно!");
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

            EditorGUILayout.LabelField("  ", "Лист видов структур");

            EditorGUILayout.Space();
            showPosition = EditorGUILayout.Foldout(showPosition, "Список");
            if (showPosition)
                for (int i = 0; i < targetObject.Structures.Length; i++)
                    EditorGUILayout.DelayedTextField((i + 1) + ": ", targetObject.Structures[i]);

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
