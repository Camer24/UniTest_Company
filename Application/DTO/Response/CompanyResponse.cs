using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response
{
    public class CompanyResponse
    {
        public int CompanyId { get; set; }
        public required string Cuit { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string Phone { get; set; }

    }
}
