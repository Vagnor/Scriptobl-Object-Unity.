using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Structure.Model
{
    /// <summary>
    /// ���� ��� ��� ����������� ������.
    /// </summary>
    // [CreateAssetMenu(fileName = "ListRace.asset", menuName = "���������/����� �����/������� ���� ���")]
    public class ListRaceSO : ScriptableObject
    {
        [field: SerializeField] public string[] Structures { get; private set; }
        [SerializeField] private List<RaceVidSO> _listObjects;

        public void AddListRaces(RaceVidSO vid)
        {
            if (!_listObjects.Contains(vid))
            {
                _listObjects.Add(vid);

                Structures = new string[_listObjects.Count];

                for (int i = 0; i < _listObjects.Count; i++)
                    Structures[i] = _listObjects[i].RaceName;


                Debug.Log("'" + vid.RaceName + "' ������� �������� � ����!");
            }
            else
            {
                Debug.Log("'" + vid.RaceName + "' ������� � ����� � ����, ���������� ����������!");
            }
        }

        public RaceVidSO GetRace(string nameVid)
        {
            foreach (var vidSructure in _listObjects)
            {
                if (vidSructure.RaceName == nameVid)
                    return vidSructure;
            }

            return null;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(ListRaceSO))]
        class ListRaceSOHelperEditor : Editor
        {
            bool showPosition = true;
            public override void OnInspectorGUI()
            {
                ListRaceSO targetObject = (ListRaceSO)target;

                EditorGUILayout.LabelField("  ", "���� ����� ������ ���������");

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
}

