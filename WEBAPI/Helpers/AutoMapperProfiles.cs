using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WEBAPI.Dtos;
using WEBAPI.Entity;

namespace WEBAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {

         public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();


        }
        
    }
}