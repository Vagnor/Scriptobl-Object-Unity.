using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// ���� �� ����� ��������� ��� ����������.
    /// </summary>
    // [CreateAssetMenu(fileName = "ControlListsGames.asset", menuName = "���������/������� ���� ���������")]
    public class ControlListsGamesSO : ScriptableObject
    {
        [SerializeField] private SetStructureIDSO _listVidStructure;

        [field: SerializeField] public List<AnimationPart�haracterSO> ListSpeciesData {  get; private set; } // ���� ���� � �� ��������

        [field: SerializeField] public List<ColorSO> ListColorData { get; private set; } // ���� ������ � ����


        [field: SerializeField] public List<ItemSO> ListItemSO { get; private set; } // ���� ���������

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ��������
        //[SerializeField] private VidStructureSO vidStructure---;

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ��������
        //[SerializeField] private VidStructureSO vidStructure---;

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ������
        //[SerializeField] private VidStructureSO vidStructure---;


        private static Semaphore _semaphore2;

        private void Awake()
        {
            _semaphore2 = new Semaphore(1, 1);
        }

        public int CheckObject(AnimationPart�haracterSO objectData)
        {
            if (ListSpeciesData.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
            else
            {
                ListSpeciesData.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
        }

        public int CheckObject(ColorSO objectData)
        {
            if (ListColorData.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructureData);
            }
            else
            {
                ListColorData.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructureData);
            }
        }

        public int CheckObject(ItemSO objectData)
        {
            if (ListItemSO.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
            else
            {
                ListItemSO.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }

        }

        private void AddSemaphore() => _semaphore2 = new Semaphore(1, 1);

        private int CheckID(int id, VidStructureSO vidStructure)
        {
            if (_semaphore2 == null)
                AddSemaphore();
            _semaphore2.WaitOne();

            int index = _listVidStructure.CheckStructureID(vidStructure, id);
            
            _semaphore2.Release();
            return index;
        }


        private int AddIndexLIst(int ID, VidStructureSO vidStructure)
        {
            int index;

            if (ID < 1000000000)
            {

                index = _listVidStructure.SetStructureID(vidStructure);
            }
            else
            {
                index = CheckID(ID, vidStructure);
            }
            return index;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(ControlListsGamesSO))]
        class ControlListsGamesSOHelperEditor : Editor
        {
            bool showPosition = true;
            bool showPosition1 = true;
            bool showPosition2 = true;
            public override void OnInspectorGUI()
            {
                ControlListsGamesSO targetObject = (ControlListsGamesSO)target;
                //DrawDefaultInspector();
                EditorGUILayout.LabelField("  ", "���� ��������");

                EditorGUILayout.Space();

                showPosition = EditorGUILayout.Foldout(showPosition, "������ ��������");
                if (showPosition)
                    for (int i = 0; i < targetObject.ListSpeciesData.Count; i++)
                        EditorGUILayout.ObjectField((i + 1) + " :", targetObject.ListSpeciesData[i], typeof(AnimationPart�haracterSO), true);

                EditorGUILayout.Space();

                showPosition1 = EditorGUILayout.Foldout(showPosition1, "������ ������");
                if (showPosition1)
                    for (int i = 0; i < targetObject.ListColorData.Count; i++)
                        EditorGUILayout.ObjectField((i + 1) + " :", targetObject.ListColorData[i], typeof(ColorSO), true);

                EditorGUILayout.Space();

                showPosition2 = EditorGUILayout.Foldout(showPosition2, "������ ���������");
                if (showPosition2)
                    for (int i = 0; i < targetObject.ListItemSO.Count; i++)
                        EditorGUILayout.ObjectField((i + 1) + " :", targetObject.ListItemSO[i], typeof(ItemSO), true);

                EditorGUILayout.Space();

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }

    public static class SetID
    {

        public static int AddID<T>(VidStructureSO structureVid, T structure)
        {
            int IDStruct = 0;

            if (typeof(T) == typeof(AnimationPart�haracterSO))
            {
                AnimationPart�haracterSO newStructure = structure as AnimationPart�haracterSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }
            else if (typeof(T) == typeof(ColorSO))
            {
                ColorSO newStructure = structure as ColorSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }
            else if (typeof(T) == typeof(ItemSO))
            {
                ItemSO newStructure = structure as ItemSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }               

            return IDStruct;
        }
    }
}
