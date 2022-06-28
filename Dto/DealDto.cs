using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class DealDto
    {
        public int IDDeal { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> IDDealType { get; set; }
        public Nullable<int> IDPageType { get; set; }

        public DealTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<DealDto, DealTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<DealTbl>(this);
        }


        public static DealDto DalToDto(DealTbl d)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<DealTbl, DealDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<DealDto>(d);
        }
    }
}
