using UnityEngine;


namespace Structure.Model
{
    [CreateAssetMenu(fileName = "ItemParametr.asset", menuName = "����/�������/������� ����� �������� ��������")]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] public string ParameetrName { get; private set; }
        [field: SerializeField] public int FullValueParameter { get; private set; }
    }
}
