using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using System;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "Body.asset", menuName = "Игра/Персонаж/Анимированная часть тела")]
    public class AnimationPartСharacterSO : ScriptableObject
    {

        [field: SerializeField] public PartsCharacterVidSO Vid { get; private set; }
        [field: SerializeField] public RaceVidSO VidRaces { get; private set; }
        [field: SerializeField] public Sprite SpriteAnimation { get; private set; }
        [field: SerializeField] public RuntimeAnimatorController Animation { get; private set; }

        [field: SerializeField] public ListRaceSO ListRaceData { get; private set; }
        [field: SerializeField] public ListPartsCharacterVidSO ListPartsCharacterVidData { get; private set; }

        [SerializeField] private int _structID, _indexListVid = -1, _indexListRaces = -1;
        [SerializeField] private bool _studied;


        public int ID => _structID;
        public void ReID() => _structID = SetID.AddID(Vid.VidStructure, this);
        public bool FindStudied => _studied;
        public void Explore() => _studied = true;
        public void Forget() => _studied = false;


#if UNITY_EDITOR
        [CustomEditor(typeof(AnimationPartСharacterSO))]
        class ColorSOHelperEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                AnimationPartСharacterSO targetObject = (AnimationPartСharacterSO)target;

                if (targetObject.ID < 1000000)
                    EditorGUILayout.LabelField("  ", "Новая часть персонажа");
                else
                    EditorGUILayout.LabelField("Часть персонажа", "id = " + targetObject.ID);

                //DrawDefaultInspector();

                EditorGUILayout.PropertyField(serializedObject.FindProperty("_studied"), new GUIContent("Известен ли объект"));
                EditorGUILayout.Space();

                targetObject._indexListVid = EditorGUILayout.Popup(targetObject._indexListVid, targetObject.ListPartsCharacterVidData.Structures);
                if (targetObject._indexListVid >= 0)
                    targetObject.Vid = targetObject.ListPartsCharacterVidData.GetVidPartsCharacter(targetObject.ListPartsCharacterVidData.Structures[targetObject._indexListVid]);

                targetObject._indexListRaces = EditorGUILayout.Popup(targetObject._indexListRaces, targetObject.ListRaceData.Structures);
                if (targetObject._indexListRaces >= 0)
                    targetObject.VidRaces = targetObject.ListRaceData.GetRace(targetObject.ListPartsCharacterVidData.Structures[targetObject._indexListVid]);

                EditorGUILayout.Space(10);

                targetObject.SpriteAnimation = (Sprite)EditorGUILayout.ObjectField("Спрайт :", targetObject.SpriteAnimation, typeof(Sprite), true);
                targetObject.Animation = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Анимация :", targetObject.Animation, typeof(RuntimeAnimatorController), true);

                EditorGUILayout.Space(10);
                if (targetObject._indexListVid >= 0 && targetObject._indexListRaces >= 0)
                {
                    if (targetObject.ID < 1000000)
                    {
                        if (GUILayout.Button("Добавить Часть персонажа"))
                        {
                            targetObject.ReID();

                            Debug.Log("Компиляция " + targetObject.name + " прошла успешно!");
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Проверка Часть персонажа"))
                        {
                            targetObject.ReID();

                            Debug.Log("Проверка " + targetObject.name + " прошла успешно!");
                        }
                    }
                }

                GetInfoString();
                Repaint();

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
