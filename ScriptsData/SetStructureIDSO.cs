using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Structure.Model
{
    /// <summary>
    /// лист со всеми ID объектов.
    /// </summary>
    //  [CreateAssetMenu(fileName = "ID_VidStructure.asset", menuName = "Системное/Создать Лист ID Структур")]
    public class SetStructureIDSO: ScriptableObject
    {
        [SerializeField] private List<int> _listStructureID; //  лист ID
        [SerializeField] private List<VidStructureSO> _listStructureVids; // Лист Видов структур объектов
        [SerializeField] private List<int> _listStructureVidsIndex; // Лист началого index для структуры 

        private static int _zeroSumm = 1000000000;

        private static Semaphore _semaphore1;

        public int SetStructureID(int index) => _listStructureID[index];// предоставить ID под индексом

        private void Awake()
        {
            _semaphore1 = new Semaphore(1, 1);
        }

        //public int ProvideIDAnimation(AnimationPartСharacterSO animation)
        //{
        //    int id;
        //    if (_controlLists.ListSpeciesData.Contains(animation))
        //    {
        //        id = _listStructureID[_controlLists.ListSpeciesData.IndexOf(animation)];
        //    }
        //    else
        //    {
        //        _semaphore1.WaitOne();
        //        id =
        //        _semaphore1.Release();
        //    }

        //    return id;
        //}

        public int SetStructureID(VidStructureSO structureVid) // Добавить новую ID
        {
            if (_semaphore1 == null)
                _semaphore1 = new Semaphore(1, 1);

            _semaphore1.WaitOne();

            int vidID = _zeroSumm + (structureVid.NumStructure * (_zeroSumm / 100));

            if (AddListStructureVids(structureVid))
            {
                int indexNewVid = _listStructureVids.IndexOf(structureVid);

                if (_listStructureVids.Count > 1 && indexNewVid > 0)
                {
                    if (structureVid.NumStructure == _listStructureVids[indexNewVid - 1].NumStructure)
                    {
                        vidID = _listStructureID[_listStructureVidsIndex[indexNewVid - 1]] +
                            _zeroSumm + (structureVid.NumStructure * (_zeroSumm / 1000));

                        _listStructureID.Insert(indexNewVid, vidID);                        
                    }
                    else if (indexNewVid == _listStructureVids.Count - 1)
                    {
                        _listStructureID.Add(vidID);
                    }
                    else 
                    {
                        _listStructureID.Insert(indexNewVid, vidID);
                    }
                }
                else if (_listStructureVids.Count == 1)
                {
                    _listStructureID.Add(vidID);
                }
                else
                {
                    _listStructureID.Insert(indexNewVid, vidID);
                }
            }
            else
            {
                int indexNewVid = _listStructureVids.IndexOf(structureVid);

                int index = CheckOneID(indexNewVid);

                if (index == 0)
                {
                    vidID = _listStructureID[_listStructureID.Count - 1] + 1;
                    _listStructureID.Add(vidID);
                }
                else if (index == -1)
                {
                    Debug.Log(vidID + " Eror!!!");
                }
                else
                {
                    vidID = _listStructureID[index-1]+1;
                    _listStructureID.Insert(index, vidID);
                    ReListStructureVidsIndex(indexNewVid);
                }

            }

            _semaphore1.Release();

            return vidID;
        }

        

        private int CheckOneID(int index) // Найти первую свободную ID в промежутке
        {
            int IndexActual = _listStructureVidsIndex[index]; 

            if (index >= _listStructureVidsIndex.Count - 1)
            {
                for (int i = _listStructureID[_listStructureVidsIndex[index]]; i < (_listStructureID[_listStructureID.Count-1]+1); i++)
                {
                    if (i == _listStructureID[_listStructureID.Count - 1])
                    {
                        return 0;
                    }
                    else if (i != _listStructureID[IndexActual])
                    {
                        return IndexActual;
                    }
                    IndexActual++;
                }

                return -1;
            }
            else
            {
                for (int i = _listStructureID[_listStructureVidsIndex[index]]; i < _listStructureID[_listStructureVidsIndex[index + 1]]; i++)
                {

                    if (i == _listStructureID[_listStructureVidsIndex[index + 1]])
                    {
                        int x = -1; //CheckOneID(index + 1);
                        return x;
                    }
                    else if (i != _listStructureID[IndexActual])
                    {
                        return IndexActual;
                    }
                    IndexActual++;
                }

                return -1;
            }
        }

        public int CheckStructureID(VidStructureSO vidStructure,int structureID) =>
            CheckStructureID(_listStructureVids.IndexOf(vidStructure), structureID);

        private int CheckStructureID(int index, int structureID) // Проверить наличие ID в промежутке и добавить если отсутствуют
        {
            int indexID = 0;
            int IndexActual = _listStructureVidsIndex[index];

            if (index >= _listStructureVidsIndex.Count - 1)
            {

                for (int i = _listStructureID[_listStructureVidsIndex[index]]; i < _listStructureID[_listStructureID.Count-1]+1; i++ )
                {
                    if (structureID == _listStructureID[IndexActual])
                        return _listStructureID[IndexActual];

                    if (structureID > _listStructureID[IndexActual])
                        indexID = IndexActual;

                    IndexActual++;
                }
            }
            else
            {
                for (int i = _listStructureID[_listStructureVidsIndex[index]]; i < _listStructureID[_listStructureVidsIndex[index + 1]]; i++)
                {
                    if (structureID == _listStructureID[IndexActual])
                        return _listStructureID[IndexActual];

                    if (structureID > _listStructureID[IndexActual])
                        indexID = IndexActual;

                    IndexActual++;
                }
            }

            if (indexID >= _listStructureVidsIndex.Count - 1)
            {
                _listStructureID.Add(structureID);
                return structureID;
            }
            else
            {
                _listStructureID.Insert(indexID, structureID);
                ReListStructureVidsIndex(index);
                return structureID;
            }
        }

        private bool AddListStructureVids(VidStructureSO structureVid)  // Добавить новый Вид и его начальное id
        {
            if (!_listStructureVids.Contains(structureVid))
            {
                if (_listStructureVids.Count > 0)
                {
                    int invexVid = 0;
                    int DobleID = 0;

                    for (int i = 0; i < _listStructureVids.Count; ++i)
                    {
                        if (_listStructureVids[i].NumStructure == structureVid.NumStructure)
                            DobleID = i;

                        if (_listStructureVids[i].NumStructure < structureVid.NumStructure)
                            invexVid++;
                    }

                    if (DobleID > 0)
                        invexVid = DobleID + 1;

                    if (invexVid >= _listStructureVids.Count)
                    {
                        _listStructureVids.Add(structureVid);
                        _listStructureVidsIndex.Add(_listStructureID.Count);
                    }
                    else
                    {
                        _listStructureVids.Insert(invexVid, structureVid);
                        _listStructureVidsIndex.Insert(invexVid, _listStructureVidsIndex[invexVid]);
                        ReListStructureVidsIndex(invexVid);
                    }
                }
                else
                {
                    _listStructureVids.Add(structureVid);
                    _listStructureVidsIndex.Add(0);
                }

                return true;
            }

            return false;
        }

        private void ReListStructureVidsIndex(int index) // сместить Лист индексов на 1 с указанного индекса.
        {
            if (index < _listStructureVidsIndex.Count - 2)
            {
                for (int i = index + 1; i < _listStructureVidsIndex.Count; i++)
                {
                    _listStructureVidsIndex[i]++;
                }
            }
            else if (index == _listStructureVidsIndex.Count - 2)
            {
                _listStructureVidsIndex[index + 1]++;
            }
        }
    }
}
