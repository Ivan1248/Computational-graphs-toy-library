using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoAlg
{
    public class Dataset<Tx, Ty> : IEnumerable<DataPoint<Tx,Ty>>
    {
        private readonly List<DataPoint<Tx, Ty>> data = new List<DataPoint<Tx, Ty>>();
        public DataPoint<Tx, Ty> this[int i] => data[i];
        public void Add(Tx x, Ty y) => data.Add(new DataPoint<Tx, Ty>(x,y));
        public IEnumerator<DataPoint<Tx, Ty>> GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => data.Count;
    }

    public class DataPoint<Tx, Ty>
    {
        public readonly Tx x;
        public readonly Ty y;
        public DataPoint(Tx x, Ty y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
