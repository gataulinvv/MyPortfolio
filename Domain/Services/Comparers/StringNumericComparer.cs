using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Comparers
{
    /// <summary>
    /// Sorts string from
    /// 2, 1, 4d, 4e, 4c, 4a, 4b, A1, 20, B2, A2, a3, 5, 6, 4f, 1a
    /// to
    /// 1, 1a, 2, 4a, 4b, 4c, 4d, 4e, 4f, 5, 6, 20, A1, A2, a3, B2
    /// </summary>
    public class StringNumericComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null) return 0;
            if (y == null) return 0;

            x = x.ToLower();
            y = y.ToLower();

            var lengthX = x.Length;
            var lengthY = y.Length;
            var markerX = 0;
            var markerY = 0;

            // Walk through two the strings with two markers.
            while (markerX < lengthX && markerY < lengthY)
            {
                var ch1 = x[markerX];
                var ch2 = y[markerY];

                // Some buffers we can build up characters in for each chunk.
                var spaceX = new char[lengthX];
                var locX = 0;
                var spaceY = new char[lengthY];
                var locY = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    spaceX[locX++] = ch1;
                    markerX++;

                    if (markerX < lengthX)
                    {
                        ch1 = x[markerX];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(spaceX[0]));

                do
                {
                    spaceY[locY++] = ch2;
                    markerY++;

                    if (markerY < lengthY)
                    {
                        ch2 = y[markerY];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(spaceY[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                var strX = new string(spaceX);
                var strY = new string(spaceY);

                int result;

                if (char.IsDigit(spaceX[0]) && char.IsDigit(spaceY[0]))
                {
                    int thisNumericChunk = int.Parse(strX);
                    int thatNumericChunk = int.Parse(strY);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = strX.CompareTo(strY);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return lengthX - lengthY;
        }

        
    }
}
