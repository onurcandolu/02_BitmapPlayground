using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_BitmapPlayground.Filters
{
    class MovingAverageFilter : IFilter
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
                            if(x >1 && y>1 && x <width-1 && y < height-1)
                            {
                                var p = input[x, y];
                                Color pixel1 = input[x - 1, y + 1];
                                Color pixel2 = input[x - 1, y - 1];
                                Color pixel3 = input[x + 1, y + 1];
                                Color pixel4 = input[x + 1, y - 1];
                                int pixelR = (pixel1.R + pixel2.R + pixel3.R + pixel4.R) / 4;
                                int pixelG = (pixel1.G + pixel2.G + pixel3.G + pixel4.G) / 4;
                                int pixelB = (pixel1.B + pixel2.B + pixel3.B + pixel4.B) / 4;
                                result[x, y] = Color.FromArgb(p.A, pixelR, pixelG, pixelB);
                            }
                   
                        }
                }
            }), null);

            return result;
        }

        public string Name => "Moving Avarage Filter";

        public override string ToString()
            => Name;
    }
}
