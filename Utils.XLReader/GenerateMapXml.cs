using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RAK.Lib.Utils.XLReader.Generate;

namespace RAK.Lib.Utils.XLReader
{
    public class GenerateMapXml
    {
        public string xlPath { get; set; }
        public string sqlCreatePath { get; set; }

        public ParseSql ParseSql { get; protected set; }

        public GenerateMapXml(string xlPath, string sqlCreatePath)
        {
            this.xlPath = xlPath;
            this.sqlCreatePath = sqlCreatePath;
            this.ParseSql = new ParseSql(sqlCreatePath);
        }

    }
}
