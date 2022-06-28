using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.AlgorithmD;
using Dal;
using Dto;

namespace Bll
{
    public class ClassDb
    {
        AdsPlacementEntities db = new AdsPlacementEntities();
        //user
        public RequestResult GetAllUsers()
        {
            List<UserDto> lst = new List<UserDto>();
            foreach (var item in db.UserTbl.ToList())
            {
                lst.Add(UserDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddUser(UserDto u)
        {
            db.UserTbl.Add(u.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveUser(UserDto u)
        {
            db.UserTbl.Remove(u.DtoTODal());
            db.SaveChanges();
        }
        public UserDto FindUser(string password, string name)
        {
            var user = db.UserTbl.Where(x => x.Password == password && x.UserFName == name).ToList();
            if (user.Count() != 0)
                return UserDto.DalToDto(user.FirstOrDefault());
            return null;
        }
        //Ad
       public int FindImage(string fillPath)
        {
            AdImageTbl ai = new AdImageTbl();
            string file= fillPath;
            file += ".png";
            ai.FilePath = file;
            db.AdImageTbl.Add(ai);
            db.SaveChanges();
           var id= db.AdImageTbl.FirstOrDefault(x => x.FilePath == file);
            if (id != null)
                return id.IDAdImage;
            return -1;
        }
        public RequestResult FindAdToUser(int idUser)
        {
            UserTbl u = db.UserTbl.FirstOrDefault(x => x.IDUser == idUser);
            if (u.IDPermission != 3)
                return GetAllAdsNEmb();
            var ads = db.AdTbl.Where(x => x.IDUser == idUser && x.IsEmbedded == 0).ToList();
            List<DetailsAd> addto = new List<DetailsAd>();
            if (ads.Count() != 0)
            {
                foreach (var item in ads)
                {
                    DetailsAd d = new DetailsAd();
                    d = AddDetailsAd(item);
                    addto.Add(d);
                }
                return new RequestResult() { Data = addto, Message = "success", Status = true };
            }
            return null;
        }
        public DetailsAd AddDetailsAd(AdTbl item)
        {
            DetailsAd d = new DetailsAd();
            d.idAd = item.IDAd;
            d.dSSize = item.SizeTbl.DescriptionSize;
            if(item.PreferencePageTbl!=null)
            d.dPPage = item.PreferencePageTbl.DescriptionPreferencePage;
            if (item.PreferenceSheetTbl != null)
                d.dPSheet = item.PreferenceSheetTbl.DescriptionPreferenceSheet;
            if (item.AdImageTbl != null)
                d.FillPath = item.AdImageTbl.FilePath;
            return d;
        }
        public RequestResult GetAllAdsNEmb()
        {
            List<DetailsAd> lst = new List<DetailsAd>();
            foreach (var item in db.AdTbl.ToList())
                if (item.IsEmbedded == 0)
                {
                    DetailsAd d = new DetailsAd();
                    d = AddDetailsAd(item);
                    lst.Add(d);
                }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddAd(AdDto a)
        {
            if (a.IDPreferencePage == 0)
                a.IDPreferencePage = null;
            if (a.IDPreferenceSheet == 0)
                a.IDPreferenceSheet = null;
            //a.IsEmbedded = 0;
            db.AdTbl.Add(a.DtoTODal());
            db.SaveChanges();
        }
        public void UpdateAd(AdDto a)
        {
            var aa = db.AdTbl.FirstOrDefault(x => x.IDAd == a.IDAd);
            aa = a.DtoTODal();
            db.SaveChanges();
        }
        public RequestResult ReturnPreferens(int idsize)
        {
            var list = db.PreferencePageTbl.Where(x => x.IDSize == idsize).ToList();
            var list2 = db.PreferenceSheetTbl.ToList();
            PPage[] pPages = new PPage[list.Count()];
            PSheet[] pSheet = new PSheet[list2.Count()];
            InsertAd ins = new InsertAd();
            for (int i = 0; i < list.Count(); i++)
            {
                PPage p = new PPage();
                p.idPPage = list[i].IDPreferencePage;
                p.dPPage = list[i].DescriptionPreferencePage;
                pPages[i] = p;
            }
            for (int i = 0; i < list2.Count(); i++)
            {
                PSheet p = new PSheet();
                p.idpSheet = list2[i].IDPreferenceSheet;
                p.dPSheet = list2[i].DescriptionPreferenceSheet;
                pSheet[i] = p;
            }
            ins.arrPPage = pPages;
            ins.arrPSheet = pSheet;
            return new RequestResult() { Data = ins, Message = "success", Status = true };
        }
        public void RemoveAd(int a)
        {
            AdTbl i=db.AdTbl.FirstOrDefault(x=>x.IDAd==a);
            i.IDUser = null;
            i.IsEmbedded = 1;
            db.SaveChanges();
        }
        //AdImage
        public RequestResult GetAllAdImages()
        {
            List<AdImageDto> lst = new List<AdImageDto>();
            foreach (var item in db.AdImageTbl.ToList())
            {
                lst.Add(AdImageDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddAdImage(AdImageDto ai)
        {
            db.AdImageTbl.Add(ai.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveAdImage(AdImageDto ai)
        {
            db.AdImageTbl.Remove(ai.DtoTODal());
            db.SaveChanges();
        }
        //Deal
        public RequestResult GetAllDeals()
        {
            List<DealDto> lst = new List<DealDto>();
            foreach (var item in db.DealTbl.ToList())
            {
                lst.Add(DealDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddDeal(DealDto d)
        {
            db.DealTbl.Add(d.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveDeal(DealDto d)
        {
            db.DealTbl.Remove(d.DtoTODal());
            db.SaveChanges();
        }
        //DealType
        public RequestResult GetAllDealTypes()
        {
            List<DealTypeDto> lst = new List<DealTypeDto>();
            foreach (var item in db.DealTypeTbl.ToList())
            {
                lst.Add(DealTypeDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddDealType(DealTypeDto dt)
        {
            db.DealTypeTbl.Add(dt.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveDealType(DealTypeDto dt)
        {
            db.DealTypeTbl.Remove(dt.DtoTODal());
            db.SaveChanges();
        }
        //Score
        public RequestResult GetAllScores()
        {
            List<ScoreDto> lst = new List<ScoreDto>();
            foreach (var item in db.ScoreTbl.ToList())
            {
                lst.Add(ScoreDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddScore(ScoreDto s)
        {
            db.ScoreTbl.Add(s.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveScore(ScoreDto s)
        {
            db.ScoreTbl.Remove(s.DtoTODal());
            db.SaveChanges();
        }

        //Size
        public RequestResult GetAllSizes()
        {
            var sizes = db.SizeTbl.ToList();
            SSize[] arrsize = new SSize[sizes.Count()];
            for (int i = 0; i < sizes.Count(); i++)
            {
                SSize s = new SSize();
                s.idSSize = sizes[i].IDSize;
                s.dSSize = sizes[i].DescriptionSize;
                arrsize[i] = s;
            }
            return new RequestResult() { Data = arrsize, Message = "success", Status = true };
        }
        public void AddSize(SizeDto s)
        {
            db.SizeTbl.Add(s.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveSize(SizeDto s)
        {
            db.SizeTbl.Remove(s.DtoTODal());
            db.SaveChanges();
        }
        //Sheet
        public RequestResult GetAllSheets()
        {
            List<SheetDto> lst = new List<SheetDto>();
            foreach (var item in db.SheetTbl.ToList())
            {
                lst.Add(SheetDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddSheet(SheetDto s)
        {
            db.SheetTbl.Add(s.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveSheet(SheetDto s)
        {
            db.SheetTbl.Remove(s.DtoTODal());
            db.SaveChanges();
        }
        //SheetType
        public RequestResult GetAllSheetTypes()
        {
            List<SheetTypeDto> lst = new List<SheetTypeDto>();
            foreach (var item in db.SheetTypeTbl.ToList())
            {
                lst.Add(SheetTypeDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddSheetType(SheetTypeDto st)
        {
            db.SheetTypeTbl.Add(st.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveSheetType(SheetTypeDto st)
        {
            db.SheetTypeTbl.Remove(st.DtoTODal());
            db.SaveChanges();
        }
        //Category
        public RequestResult GetAllCategorys()
        {
            List<CategoryDto> lst = new List<CategoryDto>();
            foreach (var item in db.CategoryTbl.ToList())
            {
                lst.Add(CategoryDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddCategory(CategoryDto c)
        {
            db.CategoryTbl.Add(c.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveCategory(CategoryDto c)
        {
            db.CategoryTbl.Remove(c.DtoTODal());
            db.SaveChanges();
        }
        //Priority
        public RequestResult GetAllPrioritys()
        {
            List<PriorityDto> lst = new List<PriorityDto>();
            foreach (var item in db.PriorityTbl.ToList())
            {
                lst.Add(PriorityDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPriority(PriorityDto p)
        {
            db.PriorityTbl.Add(p.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePriority(PriorityDto p)
        {
            db.PriorityTbl.Remove(p.DtoTODal());
            db.SaveChanges();
        }
        //PreferencePage
        public RequestResult GetAllPreferencesPage()
        {
            List<PreferencePageDto> lst = new List<PreferencePageDto>();
            foreach (var item in db.PreferencePageTbl.ToList())
            {
                lst.Add(PreferencePageDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPreferencePage(PreferencePageDto pp)
        {
            db.PreferencePageTbl.Add(pp.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePreferencePage(PreferencePageDto pp)
        {
            db.PreferencePageTbl.Remove(pp.DtoTODal());
            db.SaveChanges();
        }
        //PreferenceSheet
        public RequestResult GetAllPreferencesSheet()
        {
            List<PreferenceSheetDto> lst = new List<PreferenceSheetDto>();
            foreach (var item in db.PreferenceSheetTbl.ToList())
            {
                lst.Add(PreferenceSheetDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPreferenceSheet(PreferenceSheetDto ps)
        {
            db.PreferenceSheetTbl.Add(ps.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePreferenceSheet(PreferenceSheetDto ps)
        {
            db.PreferenceSheetTbl.Remove(ps.DtoTODal());
            db.SaveChanges();
        }
        //Permission
        public RequestResult GetAllPermissions()
        {
            List<PermissionDto> lst = new List<PermissionDto>();
            foreach (var item in db.PermissionTbl.ToList())
            {
                lst.Add(PermissionDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPermission(PermissionDto p)
        {
            db.PermissionTbl.Add(p.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePermission(PermissionDto p)
        {
            db.PermissionTbl.Remove(p.DtoTODal());
            db.SaveChanges();
        }
        //Page
        public RequestResult GetAllPages()
        {
            List<PageDto> lst = new List<PageDto>();
            foreach (var item in db.PageTbl.ToList())
            {
                lst.Add(PageDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPage(PageDto p)
        {
            db.PageTbl.Add(p.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePage(PageDto p)
        {
            db.PageTbl.Remove(p.DtoTODal());
            db.SaveChanges();
        }
        //PageType
        public RequestResult GetAllPageTypes()
        {
            List<PageTypeDto> lst = new List<PageTypeDto>();
            foreach (var item in db.PageTypeTbl.ToList())
            {
                lst.Add(PageTypeDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPageType(PageTypeDto pt)
        {
            db.PageTypeTbl.Add(pt.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePageType(PageTypeDto pt)
        {
            db.PageTypeTbl.Remove(pt.DtoTODal());
            db.SaveChanges();
        }
        //Placement
        public RequestResult GetAllPlacement()
        {
            List<PlacementDto> lst = new List<PlacementDto>();
            foreach (var item in db.PlacementTbl.ToList())
            {
                lst.Add(PlacementDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddPlacement(PlacementDto p)
        {
            db.PlacementTbl.Add(p.DtoTODal());
            db.SaveChanges();
        }
        public void RemovePlacement(PlacementDto p)
        {
            db.PlacementTbl.Remove(p.DtoTODal());
            db.SaveChanges();
        }
        //Adplacement
        public RequestResult GetAllAdplacement()
        {
            List<AdplacementDto> lst = new List<AdplacementDto>();
            foreach (var item in db.AdplacementTbl.ToList())
            {
                lst.Add(AdplacementDto.DalToDto(item));
            }
            return new RequestResult() { Data = lst, Message = "success", Status = true };
        }
        public void AddAdplacement(AdplacementDto p)
        {
            db.AdplacementTbl.Add(p.DtoTODal());
            db.SaveChanges();
        }
        public void RemoveAdplacement(AdplacementDto ap)
        {
            db.AdplacementTbl.Remove(ap.DtoTODal());
            db.SaveChanges();
        }
    }
}
