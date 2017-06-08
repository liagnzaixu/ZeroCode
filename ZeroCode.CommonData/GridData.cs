using System;
using System.Collections.Generic;

namespace ZeroCode.CommonData
{
    public class GridData<T>
    {
        public GridData():this(new List<T>(), 0)
        {

        }

        public GridData(IEnumerable<T> rows,int total)
        {
            Rows = rows;
            Total = total;
        }
            
        public IEnumerable<T> Rows { get; set; }
        public int Total { get; set; }
    }
}
