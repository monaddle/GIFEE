using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multisian
{
    public static class GIFEE
    {
        public static double[] GeneralizedFalseNearestNeighbors(string[] cols, int len, int width, string filename)
        {
            double[] a = new double[cols.Length];
            Random rand = new Random();

            for (int i = 0; i < a.Length; i++)
                a[i] = rand.NextDouble();

            return a;
        }
    }
}
