using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class CategoryDto
    {
        public int IDCategory { get; set; }
        public string DescriptionCategory { get; set; }

        public CategoryTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<CategoryDto, CategoryTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<CategoryTbl>(this);
        }


        public static CategoryDto DalToDto(CategoryTbl c)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<CategoryTbl, CategoryDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<CategoryDto>(c);
        }
    }
}
