using Inventory.Model;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// ���� �� ����� ��������� ��� ����������.
    /// </summary>
    //  [CreateAssetMenu(fileName = "ControlListsGames.asset", menuName = "���������/������� ���� ���������")]
    public class ControlListsGames : ScriptableObject
    {
        [SerializeField] private SetStructureIDSO _listVidStructure;

        [field: SerializeField] public List<AnimationPart�haracterSO> ListSpeciesData {  get; private set; } // ���� ���� � �� ��������

        [field: SerializeField] public List<ColorSO> ListColorData { get; private set; } // ���� ������ � ����


        [field: SerializeField] public List<ItemSO> ListItemSO { get; private set; } // ���� ���������

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ��������
        //[SerializeField] private VidStructureSO vidStructure---;

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ��������
        //[SerializeField] private VidStructureSO vidStructure---;

        //[field: SerializeField] public List<---> --- { get; private set; } // ���� ������
        //[SerializeField] private VidStructureSO vidStructure---;


        private static Semaphore _semaphore2;

        private void Awake()
        {
            _semaphore2 = new Semaphore(1, 1);
        }

        public int CheckObject(AnimationPart�haracterSO objectData)
        {
            if (ListSpeciesData.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
            else
            {
                ListSpeciesData.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
        }

        public int CheckObject(ColorSO objectData)
        {
            if (ListColorData.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructureData);
            }
            else
            {
                ListColorData.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructureData);
            }
        }

        public int CheckObject(ItemSO objectData)
        {
            if (ListItemSO.Contains(objectData))
            {
                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }
            else
            {
                ListItemSO.Add(objectData);

                return AddIndexLIst(objectData.ID, objectData.Vid.VidStructure);
            }

        }

        private void AddSemaphore() => _semaphore2 = new Semaphore(1, 1);

        private int CheckID(int id, VidStructureSO vidStructure)
        {
            if (_semaphore2 == null)
                AddSemaphore();
            _semaphore2.WaitOne();

            int index = _listVidStructure.CheckStructureID(vidStructure, id);
            
            _semaphore2.Release();
            return index;
        }


        private int AddIndexLIst(int ID, VidStructureSO vidStructure)
        {
            int index;

            if (ID < 1000000000)
            {

                index = _listVidStructure.SetStructureID(vidStructure);
            }
            else
            {
                index = CheckID(ID, vidStructure);
            }
            return index;
        }
    }

    public static class SetID
    {

        public static int AddID<T>(VidStructureSO structureVid, T structure)
        {
            int IDStruct = 0;

            if (typeof(T) == typeof(AnimationPart�haracterSO))
            {
                AnimationPart�haracterSO newStructure = structure as AnimationPart�haracterSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }
            else if (typeof(T) == typeof(ColorSO))
            {
                ColorSO newStructure = structure as ColorSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }
            else if (typeof(T) == typeof(ItemSO))
            {
                ItemSO newStructure = structure as ItemSO;

                IDStruct = structureVid.ControlListsStructure.CheckObject(newStructure);
            }               

            return IDStruct;
        }
    }
}
