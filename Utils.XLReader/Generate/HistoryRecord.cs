using System;
using System.Collections.Generic;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class HistoryRecord
    {
        public string Name { get; set; }
        public bool NullRows { get; set; }
        public List<Type> TypeList { get; set; }
    }
}
