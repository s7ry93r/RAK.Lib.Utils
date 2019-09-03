using System;
using System.Collections.Generic;
using System.Linq;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class HistorySnapshot
    {
        protected KnownTypes KnownTypesLookup;

        protected List<HistoryRecord> RecordList { get; set; }

        public HistorySnapshot(int pos, string val, HistorySnapshot prevSnapshot = null)
        {
            KnownTypesLookup = new KnownTypes();
            CurrentPos = pos;
            CurrentValue = val;
            RecordList = new List<HistoryRecord>() {
            new HistoryRecord(){Name="start", NullRows = (prevSnapshot == null) ? false : prevSnapshot.EndNullRows , TypeList = (prevSnapshot == null) ? new List<Type>() : prevSnapshot.EndTypeList },
            new HistoryRecord(){Name="current", NullRows=false, TypeList = new List<Type>()},
            new HistoryRecord(){Name="end", NullRows=false, TypeList = new List<Type>()},
        };
        }

        protected HistoryRecord getRecord(string name)
        {
            return (from r in RecordList where r.Name == name select r).FirstOrDefault();
        }

        public bool StartNullRows { get { return getRecord("start").NullRows; } }

        public List<Type> StartTypeList { get { return getRecord("start").TypeList; } }

        public string CurrentValue { get; protected set; }

        public int CurrentPos { get; protected set; }

        public bool CurrentNullRows
        {
            get { return getRecord("current").NullRows; }
            set { getRecord("current").NullRows = value; }
        }

        public List<Type> CurrentTypeList
        {
            get { return getRecord("current").TypeList; }
            set { getRecord("current").TypeList = value; }
        }

        public bool EndNullRows
        {
            get { return getRecord("end").NullRows; }
            protected set { getRecord("end").NullRows = value; }
        }
        public List<Type> EndTypeList
        {
            get { return getRecord("end").TypeList; }
            protected set { getRecord("end").TypeList = value; }
        }

        public void Resolve()
        {
            KnownTypesLookup.ResetListFlags();
            EndNullRows = CurrentNullRows || StartNullRows;
            if (StartTypeList.Count == 0)
            {
                EndTypeList = CurrentTypeList;
            }
            else
            {
                foreach (var t in CurrentTypeList)
                {
                    KnownTypesLookup.Find(t, CurrentNullRows).List1 = true;
                }
                foreach (var t in StartTypeList)
                {
                    KnownTypesLookup.Find(t, StartNullRows).List2 = true;
                }

                foreach (var kt in KnownTypesLookup.KnownList)
                {
                    if (kt.List2)
                    {
                        if ((!CurrentNullRows) && (!StartNullRows)) //both regular
                        {
                            if (kt.List1)
                                EndTypeList.Add(kt.RegularType);
                        }
                        else if (CurrentNullRows && (!StartNullRows)) //curr is null
                        {
                            EndTypeList.Add(kt.NullableType);
                        }
                        else if ((!CurrentNullRows) && StartNullRows) //prev is null
                        {
                            if (kt.List1)
                                EndTypeList.Add(kt.NullableType);
                        }
                        else //both null
                        {
                            if (kt.List1)
                                EndTypeList.Add(kt.NullableType);
                        }
                    }
                }



  
            }


        }


    }
}
