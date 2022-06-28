using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PriorityDto
    {
        public int IDPriority { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<int> IDScore { get; set; }

        public PriorityTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PriorityDto, PriorityTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PriorityTbl>(this);
        }


        public static PriorityDto DalToDto(PriorityTbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PriorityTbl, PriorityDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PriorityDto>(p);
        }
    }
}
