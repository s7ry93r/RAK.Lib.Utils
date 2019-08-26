using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Linq;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class ParseXls
    {
        public string xlPath { get; protected set; }

        public DataSet xlData { get; protected set; } 

        public List<XlsColumn> xlsColumns { get; protected set; }

        public ParseXls(string xlPath)
        {
            this.xlPath = xlPath;
            this.xlsColumns = new List<XlsColumn>();

            var extension = (new FileInfo(xlPath)).Extension;
            if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
            {
                getXLSDataSet();
                inspectDataSet();
            }
            else if (extension.ToLower() == ".csv")
            {
                throw new NotImplementedException("csv files are not handled yet.");
            }
            else
            {
                throw new NotImplementedException("This is not a known excel type.");
            }
        }

        protected void inspectDataSet()
        {
            var t0 = xlData.Tables[0];
            for(int i = 0; i < t0.Rows.Count; i++)
            {
                for(int j = 0; j < t0.Columns.Count; j++)
                {
                    var c = (from x in this.xlsColumns where x.Name == t0.Columns[j].ColumnName select x).FirstOrDefault();
                    if (c == null)
                    {
                        var xlc = new XlsColumn()
                        {
                            Name = t0.Columns[j].ColumnName,
                            Position = j,
                            PossibleDataTypes = new List<Type>(),
                            Nullable = string.IsNullOrEmpty(t0.Columns[j].ToString()) ? true : false,
                            HasDupes = false,
                        };
                        xlc.PossibleDataTypes.Add(t0.Columns[j].DataType);
                        xlc.PossibleDataTypes.Add("".GetType());
                        this.xlsColumns.Add(xlc);
                    }
                    else
                    {

                    }
                }
            }
        }

        protected void getXLSDataSet()
        {
            using (var stream = File.Open(this.xlPath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    this.xlData = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        // Gets or sets a value indicating whether to set the DataColumn.DataType 
                        // property in a second pass.
                        UseColumnDataType = true,

                        // Gets or sets a callback to determine whether to include the current sheet
                        // in the DataSet. Called once per sheet before ConfigureDataTable.
                        FilterSheet = (tableReader, sheetIndex) => true,

                        // Gets or sets a callback to obtain configuration options for a DataTable. 
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            // Gets or sets a value indicating the prefix of generated column names.
                            EmptyColumnNamePrefix = "Column",

                            // Gets or sets a value indicating whether to use a row from the 
                            // data as column names.
                            UseHeaderRow = true,

                            // Gets or sets a callback to determine which row is the header row. 
                            // Only called when UseHeaderRow = true.
                            ReadHeaderRow = (rowReader) => {
                                // F.ex skip the first row and use the 2nd row as column headers:
                                rowReader.Read();
                            },

                            // Gets or sets a callback to determine whether to include the 
                            // current row in the DataTable.
                            FilterRow = (rowReader) => {
                                return true;
                            },

                            // Gets or sets a callback to determine whether to include the specific
                            // column in the DataTable. Called once per column after reading the 
                            // headers.
                            FilterColumn = (rowReader, columnIndex) => {
                                return true;
                            }
                        }
                    });
                }
            }
        }
    }
}
