using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    //סטטוס מיקום עבור מודעה האם משובץ שם ,יש שם מודעה כרגע , פנוי
    public enum LoactionStatus { Optional, Freeze, Close }
    public class Location
    {
        public LoactionStatus Status { get; set; }//סטטוס מיקום
        public int I { get; set; }//I מיקום
        public int J { get; set; } //J מיקום 
        public int IdPage { get; set; }//מזהה עמוד
        public Location(int i, int j, int idP)
        {
            this.I = i;
            this.J = j;
            this.IdPage = idP;
        }
        public Location()
        {
        }
        public override string ToString()
        {
            return "i " + this.I + "j " + this.J + "IdPage " + this.IdPage;
        }
    }
}
