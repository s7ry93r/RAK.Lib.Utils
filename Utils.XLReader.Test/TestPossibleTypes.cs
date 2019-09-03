using System;
using Xunit;
using System.Text;
using RAK.Lib.Utils.XLReader.Generate;
using System.Collections.Generic;

namespace RAK.Lib.Utils.XLReader.Test
{
    public class TestPossibleTypes
    {

        [Fact]
        public void PT_Test1()
        {

            List<string> los = new List<string>() { "21.231", "", "44", "0" };
            int cntr = 0;
            var pt = new PossibleTypesForColumn();
            foreach(var t in los)
            {
                pt.NewRowVal(cntr++, t);
            }
            Assert.True(true);
        }

        [Fact]
        public void PT_TestDate()
        {
            List<string> los = new List<string>() { "1/1/1968", "1900-3-1 10:30 AM", "", "09/12/1999" };
            int cntr = 0;
            var pt = new PossibleTypesForColumn();
            foreach (var t in los)
            {
                pt.NewRowVal(cntr++, t);
            }
            Assert.True(true);
        }


    }


}
