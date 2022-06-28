using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
   public class AdplacementDto
    {
        public int IDAdPlacement { get; set; }
        public Nullable<int> IDPlacement { get; set; }
        public Nullable<int> IDAd { get; set; }
        public Nullable<int> IDPage { get; set; }
        public Nullable<double> LeftPositionX { get; set; }
        public Nullable<double> LeftPositionY { get; set; }
        public AdplacementTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<AdplacementDto, AdplacementTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<AdplacementTbl>(this);
        }
        public static AdplacementDto DalToDto(AdplacementTbl ap)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<AdplacementTbl, AdplacementDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<AdplacementDto>(ap);
        }
    }
}
