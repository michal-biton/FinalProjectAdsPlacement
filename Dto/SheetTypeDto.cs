using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class SheetTypeDto
    {
        public int IDSheetType { get; set; }
        public string DescriptionSheetType { get; set; }

        public SheetTypeTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<SheetTypeDto, SheetTypeTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<SheetTypeTbl>(this);
        }


        public static SheetTypeDto DalToDto(SheetTypeTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<SheetTypeTbl, SheetTypeDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<SheetTypeDto>(dt);
        }
    }
}
