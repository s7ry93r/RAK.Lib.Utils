using System;
using System.Collections.Generic;
using System.Linq;

namespace RAK.Lib.Utils.XLReader.Generate
{
    public class KnownTypes
    {
        public List<KnownType> KnownList { get; private set; }

        public KnownTypes()
        {
            KnownList = new List<KnownType>(){
            new KnownType(){TypeName = "string", RegularType = typeof(string), NullableType = typeof(string), List1 = false, List2 = false},
            new KnownType(){TypeName = "byte", RegularType = typeof(byte), NullableType = typeof(byte?), List1 = false, List2 = false},
            new KnownType(){TypeName = "short", RegularType = typeof(short), NullableType = typeof(short?), List1 = false, List2 = false},
            new KnownType(){TypeName = "int", RegularType = typeof(int), NullableType = typeof(int?), List1 = false, List2 = false},
            new KnownType(){TypeName = "long", RegularType = typeof(long), NullableType = typeof(long?), List1 = false, List2 = false},
            new KnownType(){TypeName = "bool", RegularType = typeof(bool), NullableType = typeof(bool?), List1 = false, List2 = false},
            new KnownType(){TypeName = "float", RegularType = typeof(float), NullableType = typeof(float?), List1 = false, List2 = false}, //single
			new KnownType(){TypeName = "decimal", RegularType = typeof(decimal), NullableType = typeof(decimal?), List1 = false, List2 = false},
            new KnownType(){TypeName = "double", RegularType = typeof(double), NullableType = typeof(double?), List1 = false, List2 = false},
            new KnownType(){TypeName = "datetime", RegularType = typeof(DateTime), NullableType = typeof(DateTime?), List1 = false, List2 = false},
        };
        }

        public void ResetListFlags()
        {
            foreach (var kt in KnownList)
            {
                kt.List1 = false;
                kt.List2 = false;
            }
        }

        public KnownType Find(Type t, bool useNullable)
        {
            if (useNullable)
                return (from kt in KnownList where kt.NullableType == t select kt).FirstOrDefault();
            else
                return (from kt in KnownList where kt.RegularType == t select kt).FirstOrDefault();
        }

        public Type getNullableVersionOf(Type regType)
        {
            return (from kt in KnownList where kt.RegularType == regType select kt.NullableType).FirstOrDefault();
        }

        public Type getRegularVersionOf(Type nullType)
        {
            return (from kt in KnownList where kt.NullableType == nullType select kt.RegularType).FirstOrDefault();
        }

    }
}
