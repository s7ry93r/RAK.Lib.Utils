using System;
using System.Collections.Generic;
using TB.ComponentModel;
using TB.ComponentModel.Conversions;
using System.Text.RegularExpressions;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class PossibleTypesForColumn
    {
        private List<HistorySnapshot> Snapshots;
        private List<KnownType> KnownList;

        private bool hasSnapshots { get { return Snapshots.Count > 0; } }
        private HistorySnapshot getCurrentSnapshot { get { return Snapshots[Snapshots.Count - 1]; } }
        public bool HasNulls { get { return getCurrentSnapshot.EndNullRows; } }
        public List<Type> GetTypeList { get { return getCurrentSnapshot.EndTypeList; } }


        public PossibleTypesForColumn()
        {
            Snapshots = new List<HistorySnapshot>();
            KnownList = (new KnownTypes()).KnownList;
        }

        private bool DateCheck(string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            var yy = @"((19|20)\d\d)";
            var mm = @"([1-9]|0[1-9]|1[012])";
            var dd = @"([1-9]|0[1-9]|[12][0-9]|3[01])";
            var sep = @"[\-\/.]";

            var f1 = string.Format("^{1}{0}{2}{0}{3}.{{0,20}}$", sep, yy, mm, dd);
            var f2 = string.Format("^{1}{0}{2}{0}{3}.{{0,20}}$", sep, mm, dd, yy);

            var m1 = Regex.Match(val, f1);
            var m2 = Regex.Match(val, f2);

            return m1.Success || m2.Success;
        }


        public void NewRowVal(int pos, string val)
        {
            var thisList = new List<Type>();
            var ss = new HistorySnapshot(pos, val, hasSnapshots ? getCurrentSnapshot : null);
            ss.CurrentNullRows = string.IsNullOrEmpty(val);

            foreach (KnownType kt in KnownList)
            {
                Type thisType = ss.CurrentNullRows ? kt.NullableType : kt.RegularType;
                if (val.IsConvertibleTo(thisType))
                {
                    if (kt.TypeName == "datetime")
                    {
                        if (!DateCheck(ss.CurrentValue)) continue;
                    }
                    ss.CurrentTypeList.Add(thisType);
                }
            }

            ss.Resolve();

            Snapshots.Add(ss);

        }


    }
}
