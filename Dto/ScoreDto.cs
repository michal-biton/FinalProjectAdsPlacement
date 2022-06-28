using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class ScoreDto
    {
        public int IDScore { get; set; }
        public string DescriptionScore { get; set; }
        public Nullable<int> Value { get; set; }

        public ScoreTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<ScoreDto, ScoreTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<ScoreTbl>(this);
        }


        public static ScoreDto DalToDto(ScoreTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<ScoreTbl, ScoreDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<ScoreDto>(dt);
        }
    }
}
