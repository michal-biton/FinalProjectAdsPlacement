using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class SizeDto
    {
        public int IDSize { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<int> IsLength { get; set; }
        public Nullable<double> SizeInFragments { get; set; }
        public string DescriptionSize { get; set; }
        public SizeTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<SizeDto, SizeTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<SizeTbl>(this);
        }


        public static SizeDto DalToDto(SizeTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<SizeTbl, SizeDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<SizeDto>(dt);
        }
    }
}
