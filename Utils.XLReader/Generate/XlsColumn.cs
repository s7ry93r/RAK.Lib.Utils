using System;
using System.Collections.Generic;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class XlsColumn
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public List<Type> PossibleDataTypes { get; set; }
        public bool HasDupes { get; set; }
        public SqlColumn MatchedSqlColumns { get; set; }
    }
}
