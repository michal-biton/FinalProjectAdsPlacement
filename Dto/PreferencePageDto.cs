using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PreferencePageDto
    {
        public int IDPreferencePage { get; set; }
        public Nullable<int> QuarterLengthPage { get; set; }
        public Nullable<int> QuarterWidthPage { get; set; }
        public Nullable<int> IDSize { get; set; }
        public Nullable<int> GradePlace { get; set; }

        public PreferencePageTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PreferencePageDto, PreferencePageTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PreferencePageTbl>(this);
        }


        public static PreferencePageDto DalToDto(PreferencePageTbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PreferencePageTbl, PreferencePageDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PreferencePageDto>(p);
        }
    }
}
