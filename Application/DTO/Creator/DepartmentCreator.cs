using Application.DTO.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Creator
{
    public class DepartmentCreator
    {
        public DepartmentResponse Create(Department department)
        {
            return new DepartmentResponse()
            {
                DepartmentId = department.Id,
                Name = department.Name,
            };
        }
    }
}
