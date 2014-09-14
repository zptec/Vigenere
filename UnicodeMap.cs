using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vigenère_wpf
{
    class UnicodeMap
    {
        private int low;
        private int high;
        private int range;

        public int Low
        {
            get { return low; }
            set { this.low = value; }
        }

        public int High
        {
            get { return high; }
            set { this.high = value; }
        }

        public int Range
        {
            get { return range; }
            set { this.range = value; }
        }

        public UnicodeMap(int low, int high)
        {
            this.low = low;
            this.high = high;
            this.range = high - low + 1;
        }

        public static bool CharOffset(UnicodeMap[] uniMaps, char c, ref int offset, ref int range)
        {
            foreach (UnicodeMap uniMap in uniMaps)
            {
                if(c >= uniMap.Low && c <= uniMap.High)
                {
                    offset = uniMap.Low;
                    range = uniMap.Range;
                    return true;
                }
            }
            return false;
        }

        internal static bool CharOffset(UnicodeMap[] unicodeMap, char key, ref int offset)
        {
            int range = 0;
            return CharOffset(unicodeMap, key, ref offset, ref range);
        }
    }
}
