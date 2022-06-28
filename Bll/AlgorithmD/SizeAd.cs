using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public class SizeAd
    {
        public double Length { get; set; }//אורך
        public double Width { get; set; }//רוחב
        public int IsLength { get; set; }//?האם לאורך
        public SizeAd(double Length, double Width, int IsLength)
        {
            this.Length = Length;
            this.Width = Width;
            this.IsLength = IsLength;
        }
        public SizeAd(SizeAd size):this(size.Length,size.Width,size.IsLength)
        {
        }

        public SizeAd()
        {
        }
    }
    
}
