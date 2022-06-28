using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class PermissionDto
    {
        public int IDPermission { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public PermissionTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PermissionDto, PermissionTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PermissionTbl>(this);
        }
        public static PermissionDto DalToDto(PermissionTbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PermissionTbl, PermissionDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PermissionDto>(p);
        }
    }
}
