using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Dto;
using Bll.AlgorithmD;

namespace Bll
{
    public class AlgoritemPlacement
    {
        public AdsPlacementEntities db = new AdsPlacementEntities();
        public int LengthBars = 4/*פסי אורך*/, WidthBars = 4/*פסי רוחב*/;
        public int lCm = 6, wCm = 4;//כמה סנטימטר בין פס לפס
        //לדוגמא אם העיתון הוא 24 סמ*16 סמ
        //יהיו 4 פסי אורך ו4 פסי רוחב וכל קוביה תיהיה 4*6
        public int NumPartsOnPage = 16;//מספר החלקים בעמוד
        public int SizeSheet = 1;
        public int SheetType = 3;
        public int NumPagsInThird1;//מספר העמודים בשליש 1
        public int NumPagsInThird2;//מספר העמודים בשליש 2
        public int NumPagsInThird3;//מספר העמודים בשליש 3
        public int NumPagsOnSheet;//מספר העמודים בגיליון 
        public int[,] MatGradePlace;//מטריצת ציון מקום
        public long[,] MatNicknamePlace;//מטריצת כינויי מקום
        public List<PageList> listPages;
        public List<PageTbl> listPage;
        public List<PageList> listPagesInSheet;
        public List<AdList> listAdsWeek;
        public List<PriorityTbl> listPriority;
        public List<UserTbl> listUser;
        public List<AdList> listAds;
        public List<AdTbl> listAd;
        public List<PreferencePageTbl> listPreferencePage;
        public List<PreferenceSheetTbl> listPreferenceSheet;
        public Dictionary<int, List<AdplacementTbl>> dAdplacement;
        public List<PlacementTbl> listPlacement;
        public List<AdplacementTbl> listAdsplacement;
        public SheetTbl Sheet = new SheetTbl();
        public Dictionary<int, Dictionary<int, SizeAd>[,]> dPages =
            new Dictionary<int, Dictionary<int, SizeAd>[,]>();
        public Dictionary<int, Dictionary<int, SizeAd>[,]> dPagesCopy;
        public Dictionary<int, Dictionary<int, SizeAd>[,]> dPagesPlacement =
            new Dictionary<int, Dictionary<int, SizeAd>[,]>();
        public int IsHole = 0;
        public int IsStart=0;
        public int IsEnd = 0;
        public int IDPlacement;
        public AlgoritemPlacement()
        {
            listPriority = db.PriorityTbl.ToList();
            listUser = db.UserTbl.ToList();
            listAd = db.AdTbl.ToList();
            listPreferencePage = db.PreferencePageTbl.ToList();
            listPreferenceSheet = db.PreferenceSheetTbl.ToList();
            dAdplacement = new Dictionary<int, List<AdplacementTbl>>();
            listAds = new List<AdList>();
            listPages = new List<PageList>();
            listPlacement = new List<PlacementTbl>();
            listPagesInSheet = new List<PageList>();
            listAdsWeek = new List<AdList>();
            MatNicknamePlace = new long[WidthBars + 1, LengthBars + 1];
            MatGradePlace = new int[WidthBars + 1, LengthBars + 1];
            dPagesCopy = new Dictionary<int, Dictionary<int, SizeAd>[,]>();
            DataInitialization();
        }
        public void DataInitialization()//אתחול נתונים
        {
            BuildListAds();
            BuildListAdsWeek();
            ThirdSheet();
            OpenSheet();
            OpenPage();
            BuildListPages();
            BuildListPagesInSheet();
            BuildMatPages();
            BuildPagesPlacement();
            BuildGradePlace();
            NicknamePlace();
            UpdateSumScore();
        }
        public void BuildListAds()//מאתחלת את רשימת המודעות עם המיקומים
        {
            foreach (AdTbl item in listAd)
            {
                List<Location> l = new List<Location>();
                listAds.Add(new AdList { Ad = item, Locations = l });
            }
        }
        public void BuildListAdsWeek()//מאתחלת ברשימה של מודעות השבוע
                                      //רק את המודעות שלא שובצו
        {
            foreach (AdList item in listAds)
                if (item.Ad.IsEmbedded == 0)//האם המודעה לא שובצה
                    listAdsWeek.Add(item);
        }
        public void ThirdSheet()//מעדכנת את מספר העמודים בכל גיליון וכן לשליש בעיתון
                                //ע"י שסוכמת את כל גדלי המודעות ומעגלת כלפי מעלה
        {
            float sum = 0, NumPage, NumPageInThird; double NumPageR, NumPageInThirdR;
            foreach (AdList ad in listAdsWeek)
                sum += (float)(ad.Ad.SizeTbl.SizeInFragments);//סוכמת את גדול המודעה עפ"י חלק מהעמוד
            NumPage = sum / NumPartsOnPage;//מחלקת את סכום החלקים למספר החלקים בכל עמוד
            NumPageR = Math.Ceiling(NumPage);//מעגלת כלפי מעלה
            NumPagsOnSheet = (int)NumPageR;
            if (NumPageR % 3 == 0)
                NumPagsInThird1 = NumPagsInThird2 = NumPagsInThird3 = (int)(NumPageR / 3);
            NumPageInThird = (float)(NumPageR / 3);//מחלקת את סכום העמודים לשליש בעיתון
            NumPageInThirdR = Math.Round(NumPageInThird);//מעגלת
            NumPagsInThird1 = NumPagsInThird2 = (int)NumPageInThirdR;
            NumPagsInThird3 = (int)(NumPageR - NumPagsInThird1 - NumPagsInThird2);
        }
        public void OpenSheet()//כשמתחיל שיבוץ נפתח גיליון חדש
        {
            Sheet.NumOfSheet = db.SheetTbl.Max(x => x.NumOfSheet) + 1;
            Sheet.NumOfPage = NumPagsOnSheet;
            Sheet.IDSize = SizeSheet;
            Sheet.IDSheetType = SheetType;
            db.SheetTbl.Add(Sheet);
            db.SaveChanges();
        }
        public void OpenPage()//כשמתחיל שיבוץ נפתחים עמודים בהתאם לגדלי המודעות
        {
            int ex = 1;
            for (int i = 0; i < NumPagsOnSheet; i++)
            {
                PageTbl page = new PageTbl();
                page.IDSheet = Sheet.IDSheet;
                if (i == 0)
                    page.IDPageType = 1;
                else
                    page.IDPageType = 2;
                page.PageNum = i + 1;
                page.IsExternal = ex;
                db.PageTbl.Add(page);
                db.SaveChanges();
                if (ex == 0) ex = 1; else ex = 0;
            }
            listPage = db.PageTbl.ToList();
        }
        public void BuildListPages()//מאתחלת את רשימת העמודים עם המיקומים
        {
            foreach (PageTbl item in listPage)
            {
                List<AdList> l = new List<AdList>();
                listPages.Add(new PageList { Page = item, List = l });
            }
        }
        public void BuildListPagesInSheet()//בונה עמודים לגיליון
        {
            foreach (PageList item in listPages)
            {
                if (item.Page.IDSheet == Sheet.IDSheet)
                    listPagesInSheet.Add(item);
            }
        }
        public void BuildMatPages()//מאתחלת לכל עמוד מטריצה שבה יהיו המודעות
        {
            foreach (PageList item in listPagesInSheet)
                dPages.Add(item.Page.IDPage, new Dictionary<int, SizeAd>[WidthBars, LengthBars]);
        }
        public void BuildPagesPlacement()//מאתחלת את מטריצת השיבוץ
        {
            foreach (PageList item in listPagesInSheet)
                dPagesPlacement.Add(item.Page.IDPage, new Dictionary<int, SizeAd>[WidthBars, LengthBars]);
        }
        public void BuildGradePlace()//בונה את מטריצת הציון מקום
        {
            int s = 1;
            for (int i = 1; i < MatGradePlace.GetLength(0); i++)
                for (int j = 1; j < MatGradePlace.GetLength(1); j++)
                    MatGradePlace[i, j] = s++;
        }
        public void NicknamePlace()//מכנה כל מיקום במטריצה בשם
        {
            int i; long Ptow;
            for (i = 0, Ptow = 1; i < MatNicknamePlace.GetLength(0); i++)//עובר על השורות במטריצת העמוד
                for (int j = 0; j < MatNicknamePlace.GetLength(1); j++, Ptow *= 2)//עובר על העמודות במטריצת העמוד
                    MatNicknamePlace[i, j] = Ptow;//מכנה כל מיקום "בכינוי" של חזקות 2
        }
        public void UpdateSumScore()//מעדכנת ללקוח את סך הניקוד שלו
        {
            int sum = 0;
            foreach (UserTbl user in listUser)
            {
                foreach (PriorityTbl Priority in listPriority)
                    if (Priority.IDUser == user.IDUser)
                        sum = (int)(sum + Priority.ScoreTbl.Value);
                db.UserTbl.Find(user.IDUser).SumScore = sum;
                db.SaveChanges();
            }
        }
        public void CopyPlacementsDic()//תעתיק את הדיקשנרי של העמודים
        {
            for (int i = 0; i < dPages.Count; i++)
            {
                var item = dPages.ElementAt(i);
                var mat = CopyAdMatrix(item.Value);
                dPagesCopy.Add(item.Key, mat);
            }
        }
        public Dictionary<int, SizeAd>[,] CopyAdMatrix(Dictionary<int, SizeAd>[,] source)//תעתיק את מטריצת העמודים
        {
            Dictionary<int, SizeAd>[,] newMatrix = new Dictionary<int, SizeAd>[WidthBars, LengthBars];
            for (int i = 0; i < WidthBars; i++)
                for (int j = 0; j < LengthBars; j++)
                {
                    if (source[i, j] == null)
                        continue;
                    newMatrix[i, j] = CopyDictionay(source[i, j]);
                }
            return newMatrix;
        }
        public Dictionary<int, SizeAd> CopyDictionay(Dictionary<int, SizeAd> dictionaries)//תעתיק את הדיקשנרי הפנימי
        {
            Dictionary<int, SizeAd> newDictionary = new Dictionary<int, SizeAd>();
            foreach (var item in dictionaries)
                newDictionary.Add(item.Key, new SizeAd(item.Value));
            return newDictionary;
        }
        public void FindPossibleLocations()//עוברת על כל העמודים לכל אחד על המטריצה שלו 
                                           //ומעדכנת את המיקומים האפשריים למודעות בכל מיקום
                                           //וכן מוסיפה לרשימה של העמוד את המודעה
                                           //ומעדכנת לליסט של מודעה את המיקומים האפשריים
        {
            int numPage = 1;
            foreach (var Pages in dPages)//עובר על הדיקשינרי של העמודים שמכיל מזהה עמוד ומטריצת עמוד
            {
                var pagePlacement = dPagesPlacement.FirstOrDefault(x => x.Key == Pages.Key);
                var listAdsWeekNotEmb = ListAdsEmb();
                if (listAdsWeekNotEmb.Count == 0)//אם כל המודעות להשבוע שובצו
                    return;
                for (int i = 0; i < Pages.Value.GetLength(0); i++)//עובר על השורות במטריצת העמוד
                    for (int j = 0; j < Pages.Value.GetLength(1); j++)//עובר על העמודות במטריצת העמוד
                        if (pagePlacement.Value[i, j] == null)
                            foreach (AdList Ad in listAdsWeekNotEmb)//עובר על רשימת המודעות לעיתון זה
                                ToCheck(Pages.Value, i, j, Pages.Key, Ad, numPage);//לבדיקה אם מיקום זה אפשרי
                numPage++;
            }
        }
        public void ToCheck(Dictionary<int, SizeAd>[,] pvalue, int i, int j, int pkey, AdList ad, int numPage)//לבדיקה אם מיקום זה אפשרי למודעה
        {
            int numInThirdFrom = -1, numInThirdTo = -1, isExsist = -1, pre = -1;
            var t = listPreferenceSheet.FirstOrDefault(x => x.IDPreferenceSheet == ad.Ad.IDPreferenceSheet);
            if (t == null && IsHole != 3)//כשאין העדפות
                return;
            if (t != null)
            {
                pre = (int)ad.Ad.PreferencePageTbl.GradePlace;
                ThirdFromAndTo(ref numInThirdFrom, ref numInThirdTo, (int)t.ThirdSheet);//מעדכנת למשתנים את תחומי השליש בעיתון שרצה הלקוח
            }
            ToFindLocation tf = new ToFindLocation(InitSize(ad), pre,
              MatGradePlace[i + 1, j + 1], (double)(i * lCm + ad.Ad.SizeTbl.Length),
              (double)(j * wCm + ad.Ad.SizeTbl.Width), numInThirdFrom, numInThirdTo, numPage, ad, i, j);//מאתחלת עצם עבור בדיקת מיקום
            if (check(tf))//האם מיקום זה אפשרי למודעה
                AddPlace(tf, pvalue, i, j, isExsist, pkey, ad);//מוסיף מיקום זה
        }
        public void AddPlace(ToFindLocation tf, Dictionary<int, SizeAd>[,] pvalue, int i, int j, int isExsist, int key, AdList ad)//מוסיפה מיקום זה למודעה ולעמוד
        {

            if (pvalue[i, j] == null)//אם מיקום זה עוד לא אותחל
                pvalue[i, j] = new Dictionary<int, SizeAd>();//תאתחל
            if (pvalue[i, j].Count == 0)
                isExsist = -1;
            else
                isExsist = pvalue[i, j].FirstOrDefault(x => x.Key == ad.Ad.IDAd).Key;
            if (isExsist == -1 || isExsist == 0)
            {
                pvalue[i, j].Add(ad.Ad.IDAd, InitSize(ad));//מעדכנת למיקום מודעות אפשריות
                AddPossibleLocationsToAd(i, j, key, ad.Ad.IDAd);//שולחת לפונקציה שמעדכנת למודעה
                                                                //את המיקומים שיכולה להיות
                AddPossibleLocationsToPage(key, ad);//שולחת לפונקציה שמוסיפה לעמוד מודעה אפשרית
            }
        }
        public void ThirdFromAndTo(ref int numInThirdFrom, ref int numInThirdTo, int third)//מעדכנת למשתנים את תחומי השליש בעיתון שרצה הלקוח
        {
            switch (third)
            {
                case 1:
                    numInThirdFrom = 0;
                    numInThirdTo = NumPagsInThird1;
                    break;
                case 2:
                    numInThirdFrom = NumPagsInThird1;
                    numInThirdTo = 2 * NumPagsInThird2;
                    break;
                case 3:
                    numInThirdFrom = 2 * NumPagsInThird2;
                    numInThirdTo = numInThirdFrom + NumPagsInThird3;
                    break;
            }
        }
        public SizeAd InitSize(AdList ads)//מאתחלת עצם מסוג גודל מודעה בנתונים של המודעה
        {
            SizeAd sa = new SizeAd((double)ads.Ad.SizeTbl.Length, (double)ads.Ad.SizeTbl.Width,
                 (int)ads.Ad.SizeTbl.IsLength);
            return sa;
        }
        public bool check(ToFindLocation tf)//בודקת באיזה שלב נמצא האלגוריתם 
                                           //ולפיו בודקת האם המודעה
                                           //יכולה להשתבץ במיקום זה עפ"י גודל העדפות ושליש
                                           //בהתאם לשלב האלגוריתם-ההתיחסות להעדפות
        {
            var valid = listPreferencePage.Where(y => y.QuarterLengthPage - 1 == tf.I &&
            y.QuarterWidthPage - 1 == tf.J && y.IDSize == tf.Ad.Ad.IDSize).ToList();
            switch (IsHole)
            {
                case 0:
                    if (Level1(tf))
                        return true;
                    break;
                case 1:
                   if( Level2(tf,valid))
                     return true;
                    break;
                case 2:
                    if(Level3(tf,valid))
                     return true;
                    break;
                case 3:
                    if(Level4(tf, valid))
                      return true;
                    break;
            }
            return false;
        }
        public bool Level1(ToFindLocation tf)//שלב ראשון
        {
            if (IfIsSize(tf.Len, tf.Width) == 1 //אם אתה נכנס בעמוד ממיקום זה
                 && tf.GradePlace == tf.GradeHere //ואם ציון העדפה שלך הוא כמו ציון מיקום זה
                 && IsThird(tf.NumInThirdFrom, tf.NumInThirdTo, tf.NumPage) == 1//ואתה אכן בשליש שביקשת
                 && IsExternalOrInternal(tf.Ad, tf.NumPage) == 1) // עמוד חיצוני או פנימי
                return true;
            return false;
        }
        public bool Level2(ToFindLocation tf, List<PreferencePageTbl> valid)//שלב שני
        {
            if (valid != null && IsQuarterLength(tf.Ad, tf.I) == 1//האם אתה במיקום אורך שבקשת
                     && IfIsSize(tf.Len, tf.Width) == 1//וגם אתה נכנס בעמוד ממיקום זה
                     && IsThird(tf.NumInThirdFrom, tf.NumInThirdTo, tf.NumPage) == 1//ואתה אכן בשליש שביקשת
                     && IsExternalOrInternal(tf.Ad, tf.NumPage) == 1) // עמוד חיצוני או פנימי
                return true;
            return false;
        }
        public bool Level3(ToFindLocation tf, List<PreferencePageTbl> valid)//שלב שלישי
        {
            if (valid != null && IfIsSize(tf.Len, tf.Width) == 1// אתה נכנס בעמוד ממיקום זה
                    && IsThird(tf.NumInThirdFrom, tf.NumInThirdTo, tf.NumPage) == 1)//ואתה אכן בשליש שביקשת
                return true;
            return false;
        }
        public bool Level4(ToFindLocation tf, List<PreferencePageTbl> valid)//שלב רביעי
        {
            if (valid.Count != 0 && IfIsSize(tf.Len, tf.Width) == 1)// אתה נכנס בעמוד ממיקום זה  
                return true;
            return false;
        }
        public int IfIsSize(double len, double width)//גם אתה נכנס בעמוד ממיקום זה
        {
            if (len <= LengthBars * lCm && width <= WidthBars * wCm)
                return 1;
            return 0;
        }
        public int IsThird(int numInThirdFrom, int numInThirdTo, int numPage)// האם אתה בשליש שביקשת
        {
            if (numInThirdFrom <= numPage && numInThirdTo >= numPage)
                return 1;
            return 0;
        }
        public int IsExternalOrInternal(AdList ads, int numPage) // עמוד חיצוני או פנימי
        {
            if (ads.Ad.PreferenceSheetTbl.ExternalOrInternalPage == numPage % 2)
                return 1;
            return 0;
        }
        public int IsQuarterLength(AdList ads, int i)//האם אתה במיקום אורך שבקשת
        {
            if (ads.Ad.PreferencePageTbl.QuarterLengthPage == i + 1)
                return 1;
            return 0;
        }
        public void AddPossibleLocationsToAd(int i, int j, int idP, int ida)//מעדכנת לליסט של מודעה
                                                                            //את המיקומים האפשריים
        {
            AdList a = listAdsWeek.Find(x => x.Ad.IDAd == ida);
            Location l = new Location(i, j, idP);
            a.Locations.Add(l);
            CountOptional(1, a);
        }
        public void AddPossibleLocationsToPage(int idP, AdList ad)//מקבלת מודעה ועמוד ומוסיפה לרשימה של העמוד את המודעה
        {
            PageList p = listPagesInSheet.Find(x => x.Page.IDPage == idP);
            if (p.List.Exists(x => x.Ad == ad.Ad))
                return;
            else
                p.List.Add(ad);
        }
        public void StartPlacement()//שולחת לפונקציה שמחזירה את המודעה עם מספר המיקומים המינמלי
                                    //ושלולחת לפונקציה בהתאם למספר המיקומים
        {
            AdList ad = FindMinLocationBySort();
            if (ad == null)
                return;
            if (ad.CountOptional == 1)
            {
                if (IsStart == 0)
                    InitPlacement();
                PlaceAd(ad, ad.Locations.FirstOrDefault(x => x.Status == LoactionStatus.Optional));
                if (IsEnd == 0)
                {
                    EndPlacement();
                    IsEnd++;
                }
            }
            else
                Algorithm(ad);
        }
        public List<AdList> ListAdsEmb()//מחזירה את המדודעות שלא שובצו
        {
            return listAdsWeek.Where(x => x.IsEmbedded == 0).ToList();
        }
        public AdList FindMinLocationBySort()// מוצאת את המודעות עם מספר המיקומים המינימלי ע"י מיון
                                             // ואחרי שמוצאת ממיינת שוב לפי ניקוד לקוח ותאריך
                                             // ומוצאת את המודעה האופטימלית שממנה יתחיל השיבוץ  
        {
            AdList ad;
            var ifHaveLoc = listAdsWeek.Where(x => x.CountOptional != 0 && x.IsEmbedded == 0).OrderBy(x => x.CountOptional).ToList();//ממין לפי מספר מיקומים
            if (ifHaveLoc.Count() == 0)
                return null;
            else
            {
                int count = ifHaveLoc.Min(y => y.CountOptional);
                var minLoc = ifHaveLoc.Where(x => x.CountOptional == count).ToList();//שם ברשימה את המודעות עם המיקום המינימלי
                if (minLoc.Count() == 1)
                    ad = minLoc.FirstOrDefault();
                else
                {
                    //ממיין לפי ניקוד לקוח ואחכ ממיין לפי תאריך הגשת בקשה
                    var Score = minLoc.OrderByDescending(x => x.Ad.UserTbl.SumScore).ThenBy(x => x.Ad.DateRequest).ToList();
                    ad = Score.FirstOrDefault();
                }
            }
            return ad;
        }
        public void InitPlacement()//מאתחלת מבנה נתונים לשיבוץ החדש ופותחת שיבוץ
        {
            dPagesCopy.Clear();
            CopyPlacementsDic();
            NewPlacement();
            IsEnd = 0;
        }
        public void NewPlacement()//פותחת שיבוץ חדש
        {
            PlacementTbl p = new PlacementTbl();
            db.PlacementTbl.Add(p);
            db.SaveChanges();
            listAdsplacement = new List<AdplacementTbl>();
            IsStart++;
            IDPlacement = p.IDPlacement;
        }
        public void PlaceAd(AdList ad, Location l)//מקבלת עצם מודעה ומיקום וממקמת את המודעה במיקום זה 
                                                  //היא שולחת לפונקציה שמתקנת את המיקומים שנפגעו כתוצאה מהשיבוץ
                                                  //וכן שולחת לפונקציה שמחפשת את המיקום המינימלי החדש 
        {
            if (ad.IsEmbedded == 1)
                return;
            Dictionary<int, SizeAd> adD = new Dictionary<int, SizeAd>();
            var page = dPagesPlacement.FirstOrDefault(x => x.Key == l.IdPage);
            SizeAd sa = InitSize(ad);//מאתחלת עצם מסוג גודל מודעה בנתונים של המודעה
            adD.Add(ad.Ad.IDAd, sa);
            ad.Locations.Find(x => x.I == l.I && x.J == l.J && x.IdPage == l.IdPage).Status = LoactionStatus.Close;
            CountOptional(0, ad);
            for (int i = l.I; i < l.I + ad.Ad.SizeTbl.Length / lCm && i < WidthBars; i++)
                for (int j = l.J; j < l.J + ad.Ad.SizeTbl.Width / wCm && j < LengthBars; j++)
                    page.Value[i, j] = adD;
            foreach (Location item in ad.Locations)
                if (item.Status == LoactionStatus.Optional)
                {
                    item.Status = LoactionStatus.Freeze;
                    CountOptional(0, ad);
                }
            ad.IsEmbedded = 1;
            AddAdPlacement(ad.Ad.IDAd, page.Key, l.I, l.J);
            FixLocation(l.IdPage, ad, l);
            StartPlacement();
        }
        public void CountOptional(int isAnd, AdList ad)
        {
            if (isAnd == 0)
                ad.CountOptional--;
            else
                ad.CountOptional++;
        }
        public void AddAdPlacement(int idAd, int idPage, double leftPositionX, double leftPositionY)//מכניסה למסד נתונים שיבוץ מודעה נוכחית
        {
            AdplacementTbl ap = new AdplacementTbl();
            ap.IDAd = idAd;
            ap.IDPage = idPage;
            ap.LeftPositionX = leftPositionX;
            ap.LeftPositionY = leftPositionY;
            ap.IDPlacement = IDPlacement;
            listAdsplacement.Add(ap);
        }
        public void FixLocation(int page, AdList ad, Location Location)//מתקנת את  המיקומים שנפגעו כתוצאה מהשיבוץ
        {
            long NicknamePlace2, NicknamePlace, Check;
            NicknamePlace = SumPlace(Location, ad);
            var CurrentPage = dPagesCopy.FirstOrDefault(x => x.Key == page);
            PageList p = listPagesInSheet.FirstOrDefault(x => x.Page.IDPage == CurrentPage.Key);
            var plist = p.List.Where(x => x.IsEmbedded == 0).ToList();
            foreach (var AdItem in plist)
            {
                var loc = AdItem.Locations.Where(x => x.IdPage == page && x.Status == LoactionStatus.Optional).ToList();
                foreach (var locationAd in loc)
                {
                    NicknamePlace2 = SumPlace(locationAd, AdItem);
                    Check = (NicknamePlace & NicknamePlace2);//בדיקה האם נחתכים
                    if (Check != 0)
                        if (CurrentPage.Value[locationAd.I, locationAd.J] != null)
                        {
                            AdList adItemInList = listAdsWeek.Find(x => x.Ad.IDAd == AdItem.Ad.IDAd);
                            Location locInAd = adItemInList.Locations.FirstOrDefault(x => x.I == locationAd.I &&
                            x.J == locationAd.J && x.IdPage == locationAd.IdPage);
                            locInAd.Status = LoactionStatus.Freeze;
                            CountOptional(0, adItemInList);
                            CurrentPage.Value[locationAd.I, locationAd.J].Remove(AdItem.Ad.IDAd);
                        }
                }
            }
        }
        public long SumPlace(Location location, AdList ad)//מקבלת אורך ורוחב וסוכמת את
                                                          //כינויי המקום ומחזירה את הסכום
        {
            int w = location.J, w1 = (int)(ad.Ad.SizeTbl.Width / wCm + location.J),
                 h = location.I, h1 = (int)(ad.Ad.SizeTbl.Length / lCm + location.I);
            long sum = 0;
            for (int i = h; i < h1; i++)
                for (int j = w; j < w1; j++)
                    sum += MatNicknamePlace[i, j];
            return sum;
        }
        public void Algorithm(AdList ad)//מקבלת מודעה שלה יש את מספר המיקומים המינימלי
                                        //ואחרי סינון עדיף להתחיל ממנה 
        {
            int i = 0; List<Location> loc;
            var optionalLoc = ad.Locations.Where(x => x.Status == LoactionStatus.Optional).ToList();
            loc = MinDemand(optionalLoc);//
            if (loc.Count() == 1)
            {
                if (IsStart == 0)
                    InitPlacement();
                PlaceAd(ad, loc.First());//תמקם מודעה במיקום l
                if (IsEnd == 0)
                {
                    EndPlacement();
                    IsEnd++;
                }
            }
            else
                foreach (Location l in loc)//עוברת על המיקומים של המודעה
                {
                    i++;
                    if (IsStart == 0)
                        InitPlacement();
                    if (l.Status == LoactionStatus.Optional)
                        PlaceAd(ad, l);//תמקם מודעה במיקום l
                    if (IsEnd == 0)
                    {
                        EndPlacement();
                        IsEnd++;
                    }
                    if (optionalLoc.Count != i)
                        CancelPlace(ad, l, loc);
                }
        }
        public List<Location> MinDemand(List<Location> optionalLoc)//מחזירה מיקומים בעלי ביקוש נמוך
        {
            int min = int.MaxValue; List<Location> loc = new List<Location>();
            foreach (Location l in optionalLoc)//עוברת על המיקומים של המודעה
            {
                int findmin = dPages.FirstOrDefault(x => x.Key == l.IdPage).Value[l.I, l.J].Count();
                if (findmin <= min)
                {
                    min = findmin;
                    loc.Add(l);
                }
            }
            return loc;
        }
        public void EndPlacement()//סיום סבב שיבוץ-מילוי חורים שמירת השיבוץ ושחרור מקומות
        {
            var listAdsEmb = ListAdsEmb();
            if (listAdsEmb.Count != 0)
                FillHole();//פונקציה למילוי החורים בעיתון
            var z = dAdplacement.FirstOrDefault(x => x.Key == IDPlacement);
            if ((z.Value == null) || dAdplacement.Count == 0)
            {
                dAdplacement.Add(IDPlacement, listAdsplacement);
                SaveUpdatedPlacement(Sheet.IDSheet, GetMark());//שמירת פרטי שיבוץ
                FreezeToOptional();//שינוי סטטוס המודעות
                IsStart = 0;
            }
            else
                return;
        }
        public void FillHole()//פונקציה למילוי החוירם בעיתון
        {
            int count = -1;
            while (IsHole < 3 && count != 0)
            {
                var ifNotEmb = ListAdsEmb();
                count = ifNotEmb.Count;
                if (count == 0)
                    return;
                IsHole++;
                FindPossibleLocations();
                dPagesCopy.Clear();
                CopyPlacementsDic();
                StartPlacement();
            }
        }
        public void SaveUpdatedPlacement(int idSheet, float mark)//מכניסה למסד נתונים שיבוץ 
        {
            if (IDPlacement == -1)
                return;
            PlacementTbl p = db.PlacementTbl.FirstOrDefault(x => x.IDPlacement == IDPlacement);
            p.DatePlacement = DateTime.Now;
            p.IDSheet = idSheet;
            p.MarkPlacement = mark;
            listPlacement.Add(p);
            db.SaveChanges();
        }
        public void FreezeToOptional()//לפני כל שיבוץ חדש משנה את המיקומים שהוקפאו בשיבוץ הקודם לאופטימלים
        {
            foreach (var item in listAdsWeek)
                foreach (var loc in item.Locations)
                    if (loc.Status != LoactionStatus.Optional)
                    {
                        loc.Status = LoactionStatus.Optional;
                        CountOptional(1, item);
                    }
        }
        public void CancelPlace(AdList ad, Location l, List<Location> loc)//מחזירה את שיבוץ המודעה חזרה
        {
            dPagesPlacement.Clear();
            BuildPagesPlacement();
            ReturnFixLocation(ad, l, loc);
        }
        public void ReturnFixLocation(AdList currentAd, Location l, List<Location> loc)//שחזור מצב קודם בשיבוץ
        {
            var list = dAdplacement.FirstOrDefault(x => x.Key == IDPlacement);
            if (list.Value == null || list.Value.Count() == 0)
                return;
            currentAd = currentAdloc(loc, currentAd, l);
            var adPlaceId = list.Value.FirstOrDefault(x => x.IDAd == currentAd.Ad.IDAd);// מודעה נוכחית 
            int index = list.Value.IndexOf(adPlaceId);
            var ads = list.Value.Where((x, i) => i < index).ToList();//את כל המודעות עד המודעה הנוכחית
            currentAd.IsEmbedded = 0;
            if (ads.Count() == 0)
                return;
            List<AdList> listAdsEmb = new List<AdList>();
            foreach (var item in listAdsWeek.Where(x => x.IsEmbedded == 1).ToList())
            {
                if (list.Value.Exists(x => x.IDAd == item.Ad.IDAd))
                    listAdsEmb.Add(item);
            }
            foreach (var item in listAdsEmb)
            {
                item.IsEmbedded = 0;
                AdplacementTbl ap = ads.Find(x => x.IDAd == item.Ad.IDAd);
                if (ap != null)
                {
                    item.CountOptional = 0;
                    foreach (var lo in item.Locations)
                        UpdateCount(lo, ap, item);
                    item.IsEmbedded = 0;
                }
            }
        }
        public AdList currentAdloc(List<Location> loc, AdList currentAd, Location l)
        {
            currentAd.CountOptional = 0;
            int indexl = loc.IndexOf(l);
            var locations = loc.Where((x, i) => i > indexl).ToList();//את כל המיקומים עד המודעה הנוכחית
            foreach (var item in locations)
            {
                item.Status = LoactionStatus.Optional;
                CountOptional(1, currentAd);
            }
            var locations2 = loc.Where((x, i) => i <= indexl).ToList();
            foreach (var item in locations2)
                item.Status = LoactionStatus.Freeze;
            return currentAd;
        }
        public void UpdateCount(Location loc, AdplacementTbl ap, AdList item)
        {
            if (loc.I == ap.LeftPositionX && loc.J == ap.LeftPositionY &&
                            loc.IdPage == ap.IDPage)
            {
                loc.Status = LoactionStatus.Optional;
                CountOptional(1, item);
            }
            else
                loc.Status = LoactionStatus.Freeze;
        }
        public float GetMark()//תתן ציון לשיבוץ
        {
            //הציון מתחלק כך: חורים בעיתון 20%, 
            //מודעות שלא שובצו 50%, 
            //האם הלקוחות עם הניקוד הגבוה נענו 20%, 
            //האם מודעות הלקוחות שובצו היכן שרצו 10%
            var l = dAdplacement.FirstOrDefault(x => x.Key == IDPlacement);
            listAdsplacement = l.Value;
            float SumAdEmbeddedPercent, SumHolePercent, ifMaxScoreEmbeddedMarkPercent, isLocationWantPercent;
            var listSortByScore = listAdsWeek.OrderBy(w => w.Ad.UserTbl.SumScore).ToList();//ממיין את הרשימה לפי ניקוד לקוח
            SumAdEmbeddedPercent = SumAdEmbedded();//כמה מודעות שובצו באחוזים
            SumHolePercent = SumHoleInSheet();//כמה 'אין' חורים באחוזים 
            ifMaxScoreEmbeddedMarkPercent = IfMaxScoreEmbedded(listSortByScore);//כמה מודעות עם הניקוד הגבוה שובצו באחוזים  
            isLocationWantPercent = ToLocationWant(listSortByScore);//כמה מודעות מוקמו במיקומים שהלקוחות רצו באחוזים 
            return SumAdEmbeddedPercent + SumHolePercent + ifMaxScoreEmbeddedMarkPercent + isLocationWantPercent;
        }
        public float BecomesPercent(int Part, int whole, int Percent)//מחזירה כאחוזים
        {
            if (whole == 0)
                return 0;
            double a = 0;
            a = (double)Part / (double)whole;
            return (float)(a * Percent);//מחזירה כאחוזים
        }
        public float SumAdEmbedded()//מחזירה כמה מודעות שובצו באחוזים
        {
            int sumAdNotEmbedded = 0;
            var IsEmbedded = ListAdsEmb();//שמה ברשימה את כל המודעות שלא שובצו
            sumAdNotEmbedded = IsEmbedded.Count();
            return 50 - BecomesPercent(sumAdNotEmbedded, listAdsWeek.Count(), 50);//כמה מודעות שובצו באחוזים
        }
        public float SumHoleInSheet()//סוכמת כמה "חורים" יש בעיתון ומחזירה את באחוזים
        {
            int SumHoleInSheet = 0;
            foreach (var Pages in dPagesPlacement)
                for (int i = 0; i < Pages.Value.GetLength(0); i++)//עובר על השורות במטריצת העמוד
                    for (int j = 0; j < Pages.Value.GetLength(1); j++)//עובר על העמודות במטריצת העמוד
                        if (Pages.Value[i, j] == null)//אם במיקום זה אין אף מודעה
                            SumHoleInSheet++;//סוכם כמה "חורים" יש בעיתון
            return 20 - BecomesPercent(SumHoleInSheet, NumPagsOnSheet * NumPartsOnPage, 20);
        }
        public float IfMaxScoreEmbedded(List<AdList> listSortByScore)//הפונקציה בודקת האם המודעות שללקוח שלהם יש ניקוד גבוה שובצו 
                                                                     //ואם כן האם במקום שרצו
        {
            int count = 0;
            if (listSortByScore.Count() > 10)
                count = listSortByScore.Count() / 10;//עשירית מהמודעות
            else
                count = 1;
            var tenth = listSortByScore.Take(count).ToList();//לוקח עשירית ראשונה-מהמודעות עם בעלי הניקוד הגבוה
            int sumEmbedded = 0; float sumMaxScoreEmbeddedPercent = 0, sumEmbeddedPercent = 0, mark = 0;
            var IsEmbedded = tenth.Where(x => x.IsEmbedded == 1).ToList();//אם המודעה שובצה תסכום 
            sumEmbedded = IsEmbedded.Count();
            sumMaxScoreEmbeddedPercent = IsLocationWant(tenth);//תבדוק האם זה במיקום שרצה הלקוח
            sumEmbeddedPercent = BecomesPercent(sumEmbedded, tenth.Count(), 10);
            mark = sumEmbeddedPercent + sumMaxScoreEmbeddedPercent;
            return mark;
        }
        public float ToLocationWant(List<AdList> listSortByScore)//בודקת האם יתר המודעות שובצו היכן שהלקוח רצה
        {
            listSortByScore.Reverse();//הופך את המיון לסדר הפוך
            var listSortByScoreReverse = listSortByScore.Take(listSortByScore.Count()
                - (listSortByScore.Count() / 10)).ToList();//מוציא מהרשימה את העשירית עם הניקוד הגבוה היות ומחושבת כבר
            return IsLocationWant(listSortByScoreReverse);
        }
        public float IsLocationWant(List<AdList> list)//האם המודעות שובצו במקום שהלקוח ביקש
        {
            int x = -1, y = -1, third; float sumMaxScoreEmbedded = 0, sumMaxScoreEmbeddedPercent = 0;
            foreach (var item in list.Where(z => z.Ad.PreferencePageTbl != null))
            {
                if (item.IsEmbedded != 0)
                {
                    var ap = listAdsplacement.FirstOrDefault(z => z.IDAd == item.Ad.IDAd);
                    if (ap != null)
                    {
                        x = (int)(ap.LeftPositionX + 1);
                        y = (int)(ap.LeftPositionY + 1);
                        var a = listAdsWeek.FirstOrDefault(z => z.Ad.IDAd == ap.IDAd);
                        var p = listPagesInSheet.FirstOrDefault(z => z.Page.IDPage == ap.IDPage);
                        third = IfIsThird(ap, a, p);
                        if (MatGradePlace[x, y] == item.Ad.PreferencePageTbl.GradePlace && third == 1)//האם המודעה השתבצה במקום שהלקוח ביקש 
                            sumMaxScoreEmbedded++;
                        else if (item.Ad.PreferencePageTbl.QuarterLengthPage == ap.LeftPositionX + 1 && third == 1)//האם היתה התחשבות בחלק מההעדפות 
                            sumMaxScoreEmbedded = (float)(sumMaxScoreEmbedded + 0.75);
                        else if (third == 2)
                            sumMaxScoreEmbedded = (float)(sumMaxScoreEmbedded + 0.5);
                    }
                }
            }
            sumMaxScoreEmbeddedPercent = BecomesPercent((int)sumMaxScoreEmbedded, list.Count(), 10);
            return sumMaxScoreEmbeddedPercent;
        }
        public int IfIsThird(AdplacementTbl ap, AdList a, PageList p)//האם העדפות בעיתון נענו
        {
            int numInThirdFrom = -1, numInThirdTo = -1, isThird;
            if (a.Ad.PreferencePageTbl == null)
                return 0;
            ThirdFromAndTo(ref numInThirdFrom, ref numInThirdTo, (int)a.Ad.PreferenceSheetTbl.ThirdSheet);//מעדכנת למשתנים את תחומי השליש בעיתון שרצה הלקוח
            isThird = IsThird(numInThirdFrom, numInThirdTo, (int)p.Page.PageNum);
            if (a.Ad.PreferenceSheetTbl.ExternalOrInternalPage == p.Page.PageNum % 2 && isThird == 1)
                return 1;
            else if (isThird == 1)
                return 2;
            return 0;
        }
        public ToPlacement[] OptimalPlacementSelection(int idSheet)//בוחרת את השיבוץ האפטימלי לגיליון
        {
            var lp = listPlacement.Where(x => x.IDSheet == idSheet).ToList();
            double mark = 0;
            if (lp.Count() != 0)
            {
                mark = (double)lp.Max(x => x.MarkPlacement);
                PlacementTbl placement = (PlacementTbl)lp.FirstOrDefault(x => x.MarkPlacement == mark);
                IDPlacement = lp.FirstOrDefault().IDPlacement;
                FinalPlacement();
                ToPlacement[] arr = Result();
                return arr;
            }
            else
                return null;
        }
        public void FinalPlacement()//שמירת נתוני השיבוץ הסופי שנבחר
        {
            SavaAdsPlacement();
            var list = dAdplacement.FirstOrDefault(x => x.Key == IDPlacement);
            foreach (var item in list.Value)
            {
                AdplacementTbl adPla = list.Value.FirstOrDefault(x => x.IDAd == item.IDAd);
                AdTbl ad = db.AdTbl.FirstOrDefault(x => x.IDAd == item.IDAd);
                ad.LeftPositionX = adPla.LeftPositionX;
                ad.LeftPositionY = adPla.LeftPositionY;
                ad.IDPage = adPla.IDPage;
                // ad.IsEmbedded = 1;
                adPla.PlacementTbl.SheetTbl.DateDistribution = DateTime.Now;
                db.SaveChanges();
            }
        }
        public void SavaAdsPlacement()
        {
            var l = dAdplacement.FirstOrDefault(x => x.Key == IDPlacement);
            listAdsplacement = l.Value;
            foreach (var item in listAdsplacement)
                db.AdplacementTbl.Add(item);
            db.SaveChanges();
        }
        public ToPlacement[] Result()//מחזירה את תוצאת השיבוץ כמערך עצמים
        {
            var listAdEmb = listAdsWeek.Where(x => x.IsEmbedded == 1).ToList();
            ToPlacement[] arr = new ToPlacement[listAdEmb.Count];
            for (int i = 0; i < arr.Length; i++)
            {
                AdList ad = listAdEmb[i];
                ToPlacement tp = new ToPlacement();
                if (ad.Ad.AdImageTbl != null)
                    tp.FilePath = ad.Ad.AdImageTbl.FilePath;
                tp.LeftPositionX = (float)ad.Ad.LeftPositionX;
                tp.LeftPositionY = (float)ad.Ad.LeftPositionY;
                tp.Length = (float)ad.Ad.SizeTbl.Length;
                tp.Width = (float)ad.Ad.SizeTbl.Width;
                tp.IDPage = (int)ad.Ad.PageTbl.PageNum;
                tp.IDAd = ad.Ad.IDAd;
                if (ad.Ad.PreferencePageTbl != null)
                    tp.pp = ad.Ad.PreferencePageTbl.DescriptionPreferencePage;
                if (ad.Ad.PreferenceSheetTbl != null)
                    tp.psh = ad.Ad.PreferenceSheetTbl.DescriptionPreferenceSheet;
                arr[i] = tp;
            }
            return arr;
        }
    }
}
