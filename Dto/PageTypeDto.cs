using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PageTypeDto
    {
        public int IDPageType { get; set; }
        public string DescriptionPageType { get; set; }

        public PageTypeTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PageTypeDto, PageTypeTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PageTypeTbl>(this);
        }


        public static PageTypeDto DalToDto(PageTypeTbl pt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PageTypeTbl, PageTypeDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PageTypeDto>(pt);
        }
    }
}
