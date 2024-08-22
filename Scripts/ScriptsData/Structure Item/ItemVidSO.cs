using UnityEngine;



namespace Structure.Model
{
    [CreateAssetMenu(fileName = "ItemVid", menuName = "Игра/Предметы/Новый Вид предмета")]
    public class ItemVidSO : ScriptableObject
    {
        [Tooltip("Имя вида")]
        [field: SerializeField] public string VidName { get; private set; }

        [Tooltip("Отношение предмета к структурам программы")]
        [field: SerializeField] public VidStructureSO VidStructure { get; private set; }
    }
}
