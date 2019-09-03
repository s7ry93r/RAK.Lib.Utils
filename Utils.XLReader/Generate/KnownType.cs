using System;
using System.Text;

namespace RAK.Lib.Utils.XLReader.Generate
{

    public class KnownType
    {
        public string TypeName { get; set; }
        public Type RegularType { get; set; }
        public Type NullableType { get; set; }
        public bool List1 { get; set; }
        public bool List2 { get; set; }
    }
}
