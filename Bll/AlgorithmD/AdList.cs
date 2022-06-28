using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public class AdList
    {
        public AdTbl Ad;//עצם מודעה מהטבלה
        public List<Location> Locations=new List<Location>();//רשימת מיקומים אפשרים למודעה
        public int IsEmbedded;//האם המודעה כרגע משובצת
        public int CountOptional;//כמה מיקומים אפשריים יש לה
        public AdList(AdTbl Ad, List<Location> listLocation)
        {
            this.Ad = Ad;
            this.Locations = listLocation;
        }
        public AdList()
        {
        }
    }
}
