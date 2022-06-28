using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class UserDto
    {
        public int IDUser { get; set; }
        public string IDCustomer { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public string Address { get; set; }
        public Nullable<int> AddressNum { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> IDPriority { get; set; }
        public Nullable<int> IDPermission { get; set; }
        public Nullable<int> SumScore { get; set; }
        public UserTbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UserDto, UserTbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<UserTbl>(this);
        }
        public static UserDto DalToDto(UserTbl dt)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<UserTbl, UserDto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<UserDto>(dt);
        }
    }
}
