using Character.Ui;
using UnityEditor;
using UnityEngine;

namespace Structure.Model
{
    [CreateAssetMenu(fileName = "ListVidStructure.asset", menuName = "���������/������� ��� ���������")]
    public class VidStructureSO : ScriptableObject
    {
        [field: SerializeField] public string NameStructure{get;private set;}

        [field: SerializeField] public int NumStructure { get; private set; } // 1-99

        [field: SerializeField] public ControlListsGames ControlListsStructure { get; private set; }
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
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(VidStructureSO))]
    class VidStructureSOHelperEditor : Editor
    {
        //public int index = 0;

        public override void OnInspectorGUI()
        {
            VidStructureSO targetObject = (VidStructureSO)target;

            EditorGUILayout.LabelField("��������", "��� �������� �������");

            DrawDefaultInspector();

            EditorGUILayout.Space();
            if (GUILayout.Button("������� ��������� � ���� ��� ��������� �������"))
            {
                targetObject.ListVidStructuresData.AddListStructures(targetObject);;
            }

            //index = EditorGUILayout.Popup(index, targetObject.ListVidStructuresData.Structures);

            base.SaveChanges();
        }
    }
#endif
}
