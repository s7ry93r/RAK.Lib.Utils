using System.IO;
using ExcelDataReader;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class ExcelRecon
    {
        public  string XlPath { get; protected set; }

        public ExcelRecon(string xlPath)
        {
            this.XlPath = xlPath;
        }

        public void Recon()
        {
            using (var stream = File.Open(XlPath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}