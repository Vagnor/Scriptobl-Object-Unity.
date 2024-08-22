using UnityEngine;



namespace Structure.Model
{
    [CreateAssetMenu(fileName = "ItemVid", menuName = "����/��������/����� ��� ��������")]
    public class ItemVidSO : ScriptableObject
    {
        [Tooltip("��� ����")]
        [field: SerializeField] public string VidName { get; private set; }

        [Tooltip("��������� �������� � ���������� ���������")]
        [field: SerializeField] public VidStructureSO VidStructure { get; private set; }
    }
}
