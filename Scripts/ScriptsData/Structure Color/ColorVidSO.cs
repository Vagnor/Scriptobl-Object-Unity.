using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "MaterialVid.asset", menuName = "Игра/Цвет/Новый Вид")]
    public class ColorVidSO : ScriptableObject
    {
        [field: SerializeField] public string VidName { get; private set; }

        [field: SerializeField] public VidStructureSO VidStructureData { get; private set; }
        [field: SerializeField] public ListColorVidsSO ListVidColorsData { get; private set; }
        [field: SerializeField] public ListPartsCharacterVidSO ListPartsCharacterVidData { get; private set; }
        [field: SerializeField] public ListRaceSO ListRaceData { get; private set; }

        //заполнить только одно поле
        [Header("Предмет")]
        [SerializeField] private bool _activeItem = false;
        [SerializeField] private ItemVidSO _itemVid;

        [Header("Часть персонажа")]
        [SerializeField] private bool _activeCharacter = false;
        [SerializeField] private PartsCharacterVidSO _partsCharacterVid;
        [SerializeField] public List<RaceActive> _listRace;

        [SerializeField] private int _indexListVid = -1;


        private void Awake()
        {
            if (VidName.Length > -1)
                ListVidColorsData.AddListVidColors(this);
        }

        public VidStructureSO VidStructureObject()
        {
            VidStructureSO vidStructure = null;

            vidStructure = _itemVid != null ? _itemVid.VidStructure :
                _partsCharacterVid != null ? _partsCharacterVid.VidStructure :
                vidStructure;

            return vidStructure;
        }

        public ItemVidSO GetItemVid(ItemVidSO vid) => _itemVid;
        public PartsCharacterVidSO GetPartsCharacterVid(PartsCharacterVidSO vid) => _partsCharacterVid;
        public List<RaceVidSO> GetListRace()
        {
            List<RaceVidSO> NewList = new List<RaceVidSO>();

            foreach (var race in _listRace)
                if (race.ActiveRaces)
                    NewList.Add(race.Race);

            return NewList;
        }

        private void ReLisrRaceColor()
        {
            if (_listRace == null)
            {
                _listRace = new List<RaceActive>();

                for (int i = 0; i < ListRaceData.Structures.Length; i++)
                {
                    RaceActive NewRace = new() { ActiveRaces = false, Race = ListRaceData.GetRace(ListRaceData.Structures[i]) };
                    _listRace.Add(NewRace);
                }
            }
            else if (ListRaceData.Structures.Length != _listRace.Count)
            {
                UnityEngine.Debug.Log(1);
                for (int i = 0; i < ListRaceData.Structures.Length; i++)
                {
                    int b = 0;

                    foreach (var race in _listRace)
                        if (ListRaceData.Structures[i] == race.Race.RaceName)
                            b++;

                    if (b == 0)
                    {
                        RaceActive NewRace = new() { ActiveRaces = false, Race = ListRaceData.GetRace(ListRaceData.Structures[i]) };
                        _listRace.Add(NewRace);
                    }
                }
            }

            if (ListRaceData.Structures.Length <= _listRace.Count)
            {
                foreach (var race in _listRace)
                {
                    int b = 0;

                    for (int i = 0; i < ListRaceData.Structures.Length; i++)
                        if (ListRaceData.Structures[i] == race.Race.RaceName)
                            b++;

                    if (b == 0)
                    {
                        _listRace.Remove(race);
                    }
                }
            }
        }


        [Serializable]    
        public class RaceActive
        {
            public bool ActiveRaces;
            public RaceVidSO Race; 
        }



#if UNITY_EDITOR
        [CustomEditor(typeof(ColorVidSO))]
        class ColorSOHelperEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                ColorVidSO targetObject = (ColorVidSO)target;

                EditorGUILayout.LabelField("Подсказка", "Отношение цвета к объекту");

                //DrawDefaultInspector();
                EditorGUILayout.Space();

                targetObject.VidName = EditorGUILayout.TextField("Имя для вида", targetObject.VidName);

                EditorGUILayout.Space(7);

                if (!targetObject._activeItem && !targetObject._activeCharacter)
                {
                    targetObject._activeItem = EditorGUILayout.ToggleLeft("Предмет", targetObject._activeItem);
                    targetObject._activeCharacter = EditorGUILayout.ToggleLeft("Персонаж", targetObject._activeCharacter);
                }
                else if (targetObject._activeItem)
                {
                    EditorGUILayout.TextField("Предмет");
                    EditorGUILayout.Space(7);

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_itemVid"));


                    if (GUILayout.Button("Добавит цвет предмета в лист " + Environment.NewLine + "или проверить наличие"))
                        targetObject.ListVidColorsData.AddListVidColors(targetObject); ;
                }
                else if (targetObject._activeCharacter)
                {
                    EditorGUILayout.TextField("Персонаж");
                    EditorGUILayout.Space(7);

                    targetObject._indexListVid = EditorGUILayout.Popup(targetObject._indexListVid, targetObject.ListPartsCharacterVidData.Structures);

                    if (targetObject._indexListVid >= 0)
                    {
                        targetObject.ReLisrRaceColor();
                        targetObject._partsCharacterVid = targetObject.ListPartsCharacterVidData.GetVidPartsCharacter(targetObject.ListPartsCharacterVidData.Structures[targetObject._indexListVid]);

                        for ( int i = 0; i < targetObject._listRace.Count; i++)
                            targetObject._listRace[i].ActiveRaces = EditorGUILayout.ToggleLeft(targetObject._listRace[i].Race.RaceName, targetObject._listRace[i].ActiveRaces);
                    }



                    if (GUILayout.Button("Добавит цвет части персонажа в лист" + Environment.NewLine + " или проверить наличие"))
                        targetObject.ListVidColorsData.AddListVidColors(targetObject); ;
                }

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}