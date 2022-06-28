using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
   public class PlacementDto
    {
        public int IDPlacement { get; set; }
        public Nullable<System.DateTime> DatePlacement { get; set; }
        public Nullable<int> IDSheet { get; set; }
        public Nullable<double> MarkPlacement { get; set; }
        public PlacementTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PlacementDto, PlacementTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PlacementTbl>(this);
        }
        public static PlacementDto DalToDto(PlacementTbl a)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PlacementTbl, PlacementDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PlacementDto>(a);
        }
    }
}
