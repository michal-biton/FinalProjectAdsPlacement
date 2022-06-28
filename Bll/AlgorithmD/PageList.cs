using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.AlgorithmD
{
   public class PageList
    {
        public PageTbl Page;//עצם עמוד מהטבלה
        public List<AdList> List = new List<AdList>();//רשימה של מודעות שיכולות להשתבץ בעמוד זה 
        public PageList()
        {

        }
    }
}
