using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Игра/Предмет/Создать предмет для постройки Объектов")]
    public class BuildingsItemSO : ItemSO, IPrescribe
    {
        [field: SerializeField] public GameObject ObjectBuilding { get; private set; }
        [field: SerializeField] public ItemVidSO BuildingVid { get; private set; }
        [field: SerializeField] public AudioClip BuildingSFX { get; private set; }

        [field: SerializeField] public RuleTile RuleTileObject { get; private set; }
        [field: SerializeField] public List<GameObject> ObjectPrescribe { get; private set; } = null;
    }
    public interface IPrescribe
    {
        [SerializeField] public AudioClip BuildingSFX { get; }
    }
}
