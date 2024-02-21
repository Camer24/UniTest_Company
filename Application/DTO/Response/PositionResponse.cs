using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response
{
    public class PositionResponse
    {
        public int PositionId { get; set; }
        public string Description { get; set; }
        public int Hierarchy { get; set; }
        public decimal MaxAmount { get; set; }
    }
}
