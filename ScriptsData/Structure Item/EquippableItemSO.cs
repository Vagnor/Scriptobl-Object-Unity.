using Character.Active;
using Structure.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Игра/Предмет/Создать одеваемый предмет")]
    public class EquippableItemSO : ItemSO, IItemAction
    {

        [Tooltip("Колайдеры анимации в верх")]
        [field: SerializeField] public PolygonCollider2D TopCollider { get; private set; }
        [Tooltip("Колайдеры анимации в влево")]
        [field: SerializeField] public PolygonCollider2D LeftCollider { get; private set; }
        [Tooltip("Колайдеры анимации в вправо")]
        [field: SerializeField] public PolygonCollider2D RightCollider { get; private set; }
        [Tooltip("Колайдеры анимации в вниз")]
        [field: SerializeField] public PolygonCollider2D DownCollider { get; private set; }


        [Tooltip("Цвета для предметов")]
        [SerializeField] private ColorSO _colorItem1, _colorItem2, _colorItem3;
        public Color ColorItem1 => _colorItem1.Color;
        public Color ColorItem2 => _colorItem2.Color;
        public Color ColorItem3 => _colorItem3.Color;

        [SerializeField] private Sprite _sprite1, _sprite2, _sprite3;
        [Tooltip("Шаблоны частей для предметов")]
        public Sprite Sprite1 => _sprite1;
        public Sprite Sprite2 => _sprite2;
        public Sprite Sprite3 => _sprite3;

        [Tooltip("Тени частей для предметов")]
        [SerializeField] private Sprite _spriteTen1, _spriteTen2, _spriteTen3;
        public Sprite SpriteTen1 => _spriteTen1;
        public Sprite SpriteTen2 => _spriteTen2;
        public Sprite SpriteTen3 => _spriteTen3;

        [Tooltip("Тени частей для предметов")]
        [SerializeField] private Image _image1, _image2, _image3;
        public Image Image1 => _image1;
        public Image Image2 => _image2;
        public Image Image3 => _image3;

        [Tooltip("Тени частей для предметов")]
        [SerializeField] private Image _imageTen1, _imageTen2, _imageTen3;
        public Image ImageTen1 => _imageTen1;
        public Image ImageTen2 => _imageTen2;
        public Image ImageTen3 => _imageTen3;



        public string ActionName => "Equip";
        [field: SerializeField] public AudioClip actionSFX   { get; private set; }

    public bool PerformAction(GameObject character, int itemIndex, List<ItemParameter> itemState)
        {
            AgentDurability weaponSystem = character.GetComponent<AgentDurability>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapun(this, itemState == null ? UseParametersList : itemState, itemIndex);
                return true;
            }
            return false;
        }
    } 
}
