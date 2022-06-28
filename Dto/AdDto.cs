using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class AdDto
    {
        public int IDAd { get; set; }
        public Nullable<int> IDAdImage { get; set; }
        public Nullable<int> IDSize { get; set; }
        public Nullable<int> IDPreferencePage { get; set; }
        public Nullable<int> IDPreferenceSheet { get; set; }
        public Nullable<int> IsEmbedded { get; set; }
        public Nullable<double> LeftPositionX { get; set; }
        public Nullable<double> LeftPositionY { get; set; }
        public Nullable<int> IDDeal { get; set; }
        public Nullable<int> IDPage { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<int> NumInSequence { get; set; }
        public Nullable<int> IsGenizah { get; set; }
        public Nullable<int> IDCategory { get; set; }
        public Nullable<System.DateTime> DateRequest { get; set; }

        public AdTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<AdDto, AdTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<AdTbl>(this);
        }
        public static AdDto DalToDto(AdTbl a)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<AdTbl, AdDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<AdDto>(a);
        }
    }
}
