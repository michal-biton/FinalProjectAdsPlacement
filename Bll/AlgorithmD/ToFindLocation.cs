using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;


namespace Bll.AlgorithmD
{
    public class ToFindLocation
    {
        public SizeAd Sa;//גודל
        public int GradePlace;//ציון מקום שרצה הלקוח
        public int GradeHere;//ציון מקום זה
        public double Len;//אורך
        public double Width;//רוחב
        public int NumInThirdFrom;//מעמוד בשליש
        public int NumInThirdTo;//עד עמוש בשליש
        public int NumPage;//מספר עמוד
        public AdList Ad;//מודעה
        public int I;//I מיקום
        public int J;//J מיקום
        public ToFindLocation()
        {
        }
        public ToFindLocation(SizeAd sa, int gradePlace, int gradeHere, double len,
            double width, int numInThirdFrom, int numInThirdTo, int numPage, AdList ad, int i, int j)
        {
            this.Sa = sa;
            this.GradePlace = gradePlace;
            this.GradeHere = gradeHere;
            this.Len = len;
            this.Width = width;
            this.NumInThirdFrom = numInThirdFrom;
            this.NumInThirdTo = numInThirdTo;
            this.NumPage = numPage;
            this.Ad = ad;
            this.I = i;
            this.J = j;
        }
    }
}
