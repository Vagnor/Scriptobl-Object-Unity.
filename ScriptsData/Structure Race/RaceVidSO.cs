using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using Character.Ui;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "RaceXXX", menuName = "����/��������/������� ����� ��� ��� �����")]

    public class RaceVidSO : ScriptableObject
    {
        [field: SerializeField] public string RaceName;
        [field: SerializeField] public string Description;
        [field: SerializeField] public ListRaceSO ListRaceData { get; private set; }
        [field: SerializeField] public ListPartsCharacterVidSO ListPartsCharacterVidData { get; private set; }
        [field: SerializeField] public List<PartsCharacter> ListPartsCharacter { get; private set; }

        private void Awake()
        {
            if (RaceName.Length > -1)
                ListRaceData.AddListRaces(this);
        }

        public int ReListRaceVid()
        {
            int coll = 0;

            if (ListPartsCharacter.Count < ListPartsCharacterVidData.Structures.Length)
                foreach (var vid in ListPartsCharacterVidData.Structures)
                {
                    int i = 0;
                    foreach (var part in ListPartsCharacter)
                        if (part.Name == vid)
                        {
                            i = 1; break;
                        }

                    if (i != 1)
                    {
                        PartsCharacter newPart = new() { Name = vid, ActiveObject = true, ParentColor = ListPartsCharacterVidData.GetVidPartsCharacter(vid) };
                        ListPartsCharacter.Add(newPart);
                        coll++;
                    }

                }

            return coll;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(RaceVidSO))]
        class RaceVidSOHelperEditor : Editor
        {
            bool showPosition = true;

            public override void OnInspectorGUI()
            {
                RaceVidSO targetObject = (RaceVidSO)target;

                EditorGUILayout.LabelField("   ", "�����");

                //DrawDefaultInspector();

                EditorGUILayout.Space(2);

                targetObject.RaceName = EditorGUILayout.DelayedTextField("��������", targetObject.RaceName);

                EditorGUILayout.Space(2);
                targetObject.Description = EditorGUILayout.DelayedTextField("��������", targetObject.Description);

                EditorGUILayout.Space(10);
                if (GUILayout.Button("������� ����� � ���� ��� ��������� �������"))
                {
                    targetObject.ListRaceData.AddListRaces(targetObject); ;
                    int coll = targetObject.ReListRaceVid();
                    Debug.Log("�������� " + targetObject.name + " ������ �������!" + "  ��������� ����� ���������: " + coll);
                }

                EditorGUILayout.Space(10);

                showPosition = EditorGUILayout.Foldout(showPosition, "������� �� �������� ��� ������ ����� �" + System.Environment.NewLine + "������� ���� �� �������� ����� �������������� ����.");

                EditorGUILayout.Space(10);

                if (showPosition)
                    for (int i = 0; i < targetObject.ListPartsCharacter.Count; i++)
                    {
                        targetObject.ListPartsCharacter[i].ActiveObject = EditorGUILayout.ToggleLeft(targetObject.ListPartsCharacter[i].Name, targetObject.ListPartsCharacter[i].ActiveObject);

                        EditorGUILayout.Space(-5);
                        targetObject.ListPartsCharacter[i].ActiveColor = EditorGUILayout.ToggleLeft("��������/��������� ������ ����� � �����", targetObject.ListPartsCharacter[i].ActiveColor);
                        if (targetObject.ListPartsCharacter[i].ActiveColor)
                        {
                            EditorGUILayout.Space(-5);
                            targetObject.ListPartsCharacter[i].IndexListVid = EditorGUILayout.Popup(targetObject.ListPartsCharacter[i].IndexListVid, targetObject.ListPartsCharacterVidData.Structures);

                            if (targetObject.ListPartsCharacter[i].IndexListVid >= 0)
                                targetObject.ListPartsCharacter[i].ParentColor = targetObject.ListPartsCharacterVidData.GetVidPartsCharacter(targetObject.ListPartsCharacterVidData.Structures[targetObject.ListPartsCharacter[i].IndexListVid]);
                        }
                        else
                        {
                            targetObject.ListPartsCharacter[i].IndexListVid = -1;
                            targetObject.ListPartsCharacter[i].ParentColor = null;
                        }

                        EditorGUILayout.Space(7);
                    }
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }

    [Serializable]
    public class PartsCharacter
    {
        public string Name;
        public bool ActiveObject;
        public bool ActiveColor;
        public int IndexListVid = -1;
        public PartsCharacterVidSO ParentColor;
    }

}
