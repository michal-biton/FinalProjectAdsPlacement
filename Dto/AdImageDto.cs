using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class AdImageDto
    {
        public int IDAdImage { get; set; }
        public string FilePath { get; set; }
        public Nullable<int> IDAd { get; set; }

        public AdImageTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<AdImageDto, AdImageTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<AdImageTbl>(this);
        }
        public static AdImageDto DalToDto(AdImageTbl ai)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<AdImageTbl, AdImageDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<AdImageDto>(ai);
        }
    }
}
