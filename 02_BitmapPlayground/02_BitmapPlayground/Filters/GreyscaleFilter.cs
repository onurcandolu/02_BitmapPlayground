using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_BitmapPlayground.Filters
{
    class GreyscaleFilter:IFilter
    {
        public Color[,] Apply(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Color[,] result = new Color[width, height];

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state) 
            { 
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var p = input[x, y];
                        int grayScale = (int)((p.R * 0.3) + (p.G * 0.59) + (p.B * 0.11));
                        result[x, y] = Color.FromArgb(p.A, grayScale, grayScale, grayScale);
                    }
                }               
            }), null);
         

            return result;

        }

        public string Name => "GrayScale Filter";

        public override string ToString()
            => Name;
    }
}
