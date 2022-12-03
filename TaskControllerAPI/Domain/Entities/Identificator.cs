using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Identificator
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string HexIdentificator { get; set; }
    }
}
