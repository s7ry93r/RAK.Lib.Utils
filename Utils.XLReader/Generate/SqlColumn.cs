using System.Data;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class SqlColumn
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public SqlDbType SqlType { get; set; }
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public bool Nullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsIdentity { get; set; }
    }
}