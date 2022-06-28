using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.AlgorithmD
{
   public class InsertAd
    {
      public PPage[] arrPPage;
       public PSheet[] arrPSheet;
        public InsertAd()
        {
        }
    }
    public class PPage
    {
        public int idPPage;
        public string dPPage;
        public PPage()
        {

        }
    }
    public class PSheet
    {
        public int idpSheet;
        public string dPSheet;
        public PSheet()
        {

        }
    }
    public class SSize
    {
        public int idSSize;
        public string dSSize;
        public SSize()
        {

        }
    }
    public class DetailsAd
    {
        public int idAd;
        public string dSSize;
        public string dPPage;
        public string dPSheet;
        public string FillPath;
        public DetailsAd()
        {

        }
    }
}
