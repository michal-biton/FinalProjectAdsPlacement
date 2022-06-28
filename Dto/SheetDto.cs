using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class SheetDto
    {
        public int IDSheet { get; set; }
        public Nullable<System.DateTime> DateDistribution { get; set; }
        public Nullable<int> NumOfPage { get; set; }
        public Nullable<int> NumOfSheet { get; set; }
        public Nullable<int> IDSheetType { get; set; }
        public Nullable<int> IDSize { get; set; }
        public SheetTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<SheetDto, SheetTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<SheetTbl>(this);
        }


        public static SheetDto DalToDto(SheetTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<SheetTbl, SheetDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<SheetDto>(dt);
        }
    }
}
