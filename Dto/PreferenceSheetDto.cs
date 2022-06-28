using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PreferenceSheetDto
    {
        public int IDPreferenceSheet { get; set; }
        public Nullable<int> ThirdSheet { get; set; }
        public Nullable<int> ExternalOrInternalPage { get; set; }
        public PreferenceSheetTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PreferenceSheetDto, PreferenceSheetTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PreferenceSheetTbl>(this);
        }


        public static PreferenceSheetDto DalToDto(PreferenceSheetTbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PreferenceSheetTbl, PreferenceSheetDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PreferenceSheetDto>(p);
        }
    }
}
