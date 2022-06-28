using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PageDto
    {
        public int IDPage { get; set; }
        public Nullable<int> PageNum { get; set; }
        public Nullable<int> IDPageType { get; set; }
        public Nullable<int> IsExternal { get; set; }
        public Nullable<int> IDSheet { get; set; }
        public PageTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PageDto, PageTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PageTbl>(this);
        }


        public static PageDto DalToDto(PageTbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PageTbl, PageDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PageDto>(p);
        }
    }
}
