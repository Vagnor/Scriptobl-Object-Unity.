using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "ListVidStructure.asset", menuName = "���������/������� ��� ���������")]
    public class VidStructureSO : ScriptableObject
    {
        [field: SerializeField] public string NameStructure{get;private set;}

        [field: SerializeField] public int NumStructure { get; private set; } 

        [field: SerializeField] public ControlListsGamesSO ControlListsStructure { get; private set; }
        [field: SerializeField] public ListVidStructuresSO ListVidStructuresData { get; private set; }

        private void Awake()
        {
            if (NameStructure.Length > -1)
                ListVidStructuresData.AddListStructures(this);
        }

        public void AddSumm(int num)
        {
            if (NumStructure <=0)
                NumStructure = num;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(VidStructureSO))]
        class VidStructureSOHelperEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                VidStructureSO targetObject = (VidStructureSO)target;

                EditorGUILayout.LabelField(targetObject.NumStructure + "", "��� �������� �������");
                //DrawDefaultInspector();

                if (targetObject.NameStructure.Length >= 3)
                {
                    EditorGUILayout.DelayedTextField("�������� ���������", targetObject.NameStructure);

                    EditorGUILayout.Space();

                    if (targetObject.NumStructure <= 0)
                    {
                        if (GUILayout.Button("������� ��������� � ����"))
                            targetObject.ListVidStructuresData.AddListStructures(targetObject);
                    }
                    else
                    {

                        if (GUILayout.Button("��������� ������� ��������� � �����"))
                            targetObject.ListVidStructuresData.AddListStructures(targetObject);
                    }
                }
                else
                {
                    targetObject.NameStructure = EditorGUILayout.DelayedTextField("������� �������� ���������", targetObject.NameStructure);
                }


                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
