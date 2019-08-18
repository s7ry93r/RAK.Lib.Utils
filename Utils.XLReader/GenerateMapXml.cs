using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RAK.Lib.Utils.XLReader
{
    public class GenerateMapXml
    {
        public string xlPath { get; set; }
        public string sqlCreatePath { get; set; }

        public GenerateMapXml(string xlPath, string sqlCreatePath)
        {
            this.xlPath = xlPath;
            this.sqlCreatePath = sqlCreatePath;
        }

    }
}
