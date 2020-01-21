using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FattElett
{
    class ArrayComparer : EqualityComparer<int[]>
    {
        public override bool Equals(int[] x, int[] y)
        {

            if (x[0] == y[0] && x[1] == y[1])
                return true;
            else
                return false;
        }

        public override int GetHashCode(int[] obj)
        {
            throw new NotImplementedException();
        }
    }
}
