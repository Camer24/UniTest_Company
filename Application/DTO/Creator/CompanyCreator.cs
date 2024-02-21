using Application.DTO.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Creator
{
    public class CompanyCreator
    {
        public CompanyResponse Create(Company company)
        {
            return new CompanyResponse()
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Adress = company.Adress,
                Cuit = company.Cuit,
                Phone = company.Phone
            };
        }
    }
}
