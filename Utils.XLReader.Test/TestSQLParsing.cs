using System;
using Xunit;
using System.IO;

namespace RAK.Lib.Utils.XLReader.Test
{
    public class TestSQLParsing
    {
        private string getCWD()
        {
            return Directory.GetCurrentDirectory();
        }

        private string getParentDir(string pwd)
        {
            return (new DirectoryInfo(pwd).Parent).FullName;
        }

        private string getSqlPath()
        {
            var newDir = getParentDir(getParentDir(getParentDir(getCWD())));
            var sqlPath = Path.Combine(newDir, @"Create.dbo.People.sql");
            return sqlPath;
        }


        [Fact]
        public void TestGenerateSQL()
        {
            var xlr = new GenerateMapXml(null, getSqlPath());
            Assert.Equal("Id", xlr.ParseSql.cols[0].Name);

        }
    }
}
