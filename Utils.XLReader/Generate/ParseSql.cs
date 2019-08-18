using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using TB.ComponentModel;
//using UTC = TB.ComponentModel.UniversalTypeConverter;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class ParseSql
    {
        public string createSQL { get; set; }

        public List<SqlColumns> cols { get; set; }

        private Dictionary<string, SqlDbType> dataTypeWords = new Dictionary<string, SqlDbType>()
        {
            {@"image", SqlDbType.Image},
            {@"binary",SqlDbType.Binary},
            {@"varbinary", SqlDbType.VarBinary},

            {@"varchar", SqlDbType.VarChar},
            {@"char", SqlDbType.Char},
            {@"text", SqlDbType.Text},
            {@"nvarchar", SqlDbType.NVarChar},
            {@"nchar", SqlDbType.NChar},
            {@"ntext", SqlDbType.NText},


            {@"datetime", SqlDbType.DateTime},
            {@"bit", SqlDbType.Bit},

            {@"int", SqlDbType.Int},
            {@"bigint", SqlDbType.BigInt},
            {@"smallint", SqlDbType.SmallInt},
            {@"decimal", SqlDbType.Decimal},
            {@"float",SqlDbType.Float},
            {@"numeric", SqlDbType.Decimal},
            {@"money", SqlDbType.Money},
        };

        private string colRegex =
            @".*\[([\w_ \$\-]*)\]\s*\[([\w]*)\]\s*(\(([\d ,MAX]*)\))?\s*(NOT NULL|NULL)?\s*(IDENTITY\s*\(([\d ,]*)\))?,?";

        private string pkRegex = @".*CONSTRAINT\s{0,4}[\w\. _\[\]]*\s{0,4}PRIMARY\s{1,2}KEY[.\s\w]*\([.\s]*\[?([\w\. _]*)\]?[.\w\s]*\)";

        public ParseSql(string createSql)
        {
            this.createSQL = createSql;
            this.cols = new List<SqlColumns>();
        }

        public void Parse()
        {
            var matches = Regex.Matches(this.createSQL, this.colRegex, RegexOptions.IgnoreCase);

            var pkMatch = Regex.Match(this.createSQL, this.pkRegex, RegexOptions.IgnoreCase);
            string pk = null;
            if (pkMatch.Success)
            {
                pk = pkMatch.Groups[1].Value;
            }

            var i = 0;

            foreach (Match match in matches)
            {
                string g4 = match.Groups[4].Value; //"" | 100 | 18,2
                int? len = getLength(g4);
                var tpl = getPrecisionAndScale(g4);
                int? prec = tpl.Item1;
                int? scale = tpl.Item2;
                string g6 = match.Groups[6].Value; //"" | identity(1, 1)

                this.cols.Add(
                    
                    new SqlColumns()
                    {
                        Position =  i++,
                        Name = match.Groups[1].Value,
                        SqlType = dataTypeWords[match.Groups[2].Value.ToLower()],
                        Length = len,
                        Precision = prec,
                        Scale = scale,
                        Nullable = match.Groups[5].Value.ToLower() == "null",
                        IsIdentity = g6 != "",
                        IsPrimaryKey = match.Groups[1].Value.ToLower() == pk.ToLower(),
                    }
                    
                );
            }
        }

        internal int? getLength(string g4)
        {
            var m = Regex.Match(g4.Trim(), @"^(\d{1,5}|max)$", RegexOptions.IgnoreCase);
            int? len = null;
            if (m.Success)
            {
                len = m.Value.ToLower() == "max" ? (int?)8192 : m.Value.To<int?>();
            }
            return len;
        }

        internal Tuple<int?, int?> getPrecisionAndScale(string g4)
        {
            var m = Regex.Match(g4.Trim(), @"^(\d{1,3})\s{0,2},\s{0,2}(\d{1,3})$");
            int? prec = null;
            int? scale = null;
            if (m.Success)
            {
                prec = m.Groups[1].Value.To<int?>();
                scale = m.Groups[2].Value.To<int?>();
            }
            return new Tuple<int?, int?>(prec, scale);
        }

    }
}
