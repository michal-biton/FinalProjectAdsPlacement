using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class DealTypeDto
    {
        public int IDDealType { get; set; }
        public string DescriptionDealType { get; set; }

        public DealTypeTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<DealTypeDto, DealTypeTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<DealTypeTbl>(this);
        }


        public static DealTypeDto DalToDto(DealTypeTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<DealTypeTbl, DealTypeDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<DealTypeDto>(dt);
        }
    }
}
