using System;
using System.Collections.Generic;
using UnityEngine;
using static Inventory.Model.ItemSO;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Предметы/Используемое(Еда)")]
    public class EdibleItemSO : ItemSO, IDestructibleItem, IItemAction
    {
        [Tooltip("Лист модивикаторов")]
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
        public string ActionName => "Consume";

        [field: SerializeField] public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, int index, List<ItemParameter> itemStatr = null)
        {
            foreach (ModifierData data in modifierData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
    }

    public interface IDestructibleItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, int index, List<ItemParameter> itemStatr);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}
