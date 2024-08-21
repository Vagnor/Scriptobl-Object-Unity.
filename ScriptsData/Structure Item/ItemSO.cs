using Structure.Model;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {

        [field: SerializeField] public Sprite ItemImage { get; private set; }

        [Tooltip("Вид объекта")]
        [field: SerializeField] public ItemVidSO Vid { get; private set; }
        [Tooltip("Стакуется ли")]
        [field: SerializeField] public bool IsStackable { get; private set; }
        [field: SerializeField] public int MaxStack { get; private set; } = 1;
        [Tooltip("Используется ди")]
        [field: SerializeField] public bool IsUsed { get; private set; } = true;

        [Tooltip("ID")][SerializeField] private int _structID;
        public int ID => _structID;
        public void ReID()
        {
            _structID = SetID.AddID(Vid.VidStructure, this);

            _nameObject = Vid.VidName;
        }

        [Tooltip("Изучен ли объект")][SerializeField] private bool _studied;
        public bool FindStudied => _studied;
        public void Explore() => _studied = true;
        public void Forget() => _studied = false;

        private string _nameObject;

        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public RuntimeAnimatorController AnimatorItem { get; private set; }
        [field: SerializeField] [field: TextArea]  public string Description { get; private set; }

        [field: SerializeField] public List<ItemParameter> UseParametersList { get; set; }


        [CustomEditor(typeof(ItemSO))]
        class ItemSOHelperEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                ItemSO targetObject = (ItemSO)target;

                if (GUILayout.Button("Добавит"))
                {
                    targetObject.ReID();

                    Debug.Log("Компиляция " + targetObject.name + " прошла успешно!");
                    GetInfoString();
                    Repaint();
                }
                EditorGUILayout.LabelField("Название", targetObject._nameObject);

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }

        [Serializable]
        public struct ItemParameter: IEquatable<ItemParameter>
        {
            public ItemParameterSO ParameterItem;
            public float Value;

            public bool Equals(ItemParameter other)
            {
                return other.ParameterItem == ParameterItem;
            }
        }
    }
}
