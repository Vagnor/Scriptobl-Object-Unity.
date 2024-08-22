using UnityEditor;
using UnityEngine;


namespace Structure.Model
{
    [CreateAssetMenu(fileName = "Material.asset", menuName = "����/����/����� ����")]
    public class ColorSO : ScriptableObject
    {
        [Tooltip("�� ������ ������� ����")][field: SerializeField] public ColorVidSO Vid { get; private set; }

        [SerializeField] private ListColorVidsSO _listVidColor;
        [SerializeField] private Color _color;
        [SerializeField] private int _indexList = -1;
        [SerializeField] private int _structID;
        [SerializeField] private bool _studied;
        private string _nameObject;

        public Color Color => _color;
        public int ID => _structID;
        public void ReID()
        {
            _structID = SetID.AddID(Vid.VidStructureData, this);

            _nameObject = Vid.VidName;
        }

        public bool FindStudied => _studied;
        public void Explore() => _studied = true;
        public void Forget() => _studied = false;



        [CustomEditor(typeof(ColorSO))]
        class ColorSOHelperEditor : Editor
        {

            public override void OnInspectorGUI()
            {
                ColorSO targetObject = (ColorSO)target;

                //DrawDefaultInspector();
                if (targetObject.ID < 1000000)
                    EditorGUILayout.LabelField("  ", "����� ����");
                else
                    EditorGUILayout.LabelField("����", "id = " + targetObject.ID);

                targetObject._color = EditorGUILayout.ColorField(targetObject._color);

                EditorGUILayout.Space(2);

                targetObject._indexList = EditorGUILayout.Popup(targetObject._indexList, targetObject._listVidColor.Structures);

                EditorGUILayout.Space(2);

                targetObject._studied = EditorGUILayout.ToggleLeft("��������", targetObject._studied);

                EditorGUILayout.Space(20);


                if (targetObject._indexList > -1)
                {

                    targetObject.Vid = targetObject._listVidColor.GetVidColor(targetObject._listVidColor.Structures[targetObject._indexList]);
                   
                    if (targetObject.ID < 1000000)
                    {
                        if (GUILayout.Button("�������"))
                        {
                            targetObject.ReID();
                            Debug.Log("���������� " + targetObject.name + " ������ �������!  �������� ID: " + targetObject.ID);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("���������"))
                        {
                            targetObject.ReID();
                            Debug.Log("���������� " + targetObject.name + " ������ �������!  �������� ID: " + targetObject.ID);
                        }
                    }
                }
                    

                EditorUtility.SetDirty(targetObject);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
