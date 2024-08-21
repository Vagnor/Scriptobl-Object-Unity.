using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "Body.asset", menuName = "Игра/Персонаж/Новый Вид части персонажа")]
    public class PartsCharacterVidSO : ScriptableObject
    {
        [field: SerializeField] public string VidName { get; private set; }

        [field: SerializeField] public VidStructureSO VidStructure { get; private set; }
        [field: SerializeField] public ListPartsCharacterVidSO ListPartsCharacterVidData { get; private set; }


        private void Awake()
        {
            if (VidName.Length > -1)
                ListPartsCharacterVidData.AddListVidPartsCharacter(this);
        }



#if UNITY_EDITOR
        [CustomEditor(typeof(PartsCharacterVidSO))]
        class PartsCharacterVidSOHelperEditor : Editor
        {
            public string nameNewVid;
            public override void OnInspectorGUI()
            {
                PartsCharacterVidSO targetObject = (PartsCharacterVidSO)target;

                EditorGUILayout.LabelField(" ", "Вид части персонажа");
                //DrawDefaultInspector();
                EditorGUILayout.Space(3);

                if (targetObject.VidName.Length <= 0)
                {
                    nameNewVid = EditorGUILayout.DelayedTextField("Введи название вида", nameNewVid);

                    EditorGUILayout.Space(10);
                    if (nameNewVid.Length > 0)
                        if (GUILayout.Button("Добавит новый вид в лист"))
                        {
                            targetObject.VidName = nameNewVid;
                            targetObject.ListPartsCharacterVidData.AddListVidPartsCharacter(targetObject);
                        }
                }
                else
                {
                    EditorGUILayout.DelayedTextField("Название", targetObject.VidName);

                    EditorGUILayout.Space(10); if (GUILayout.Button("Проверить наличие в листе"))
                        targetObject.ListPartsCharacterVidData.AddListVidPartsCharacter(targetObject);
                }

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}