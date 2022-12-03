using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MSSMHexRepository : IHexIdRepository
    {
        private readonly TaskOrganiserContext _context;

        public MSSMHexRepository(TaskOrganiserContext context)
        {
            _context = context;
        }
        public async Task<string> AddHex(Identificator identificator)
        {
            _context.Identificators.Add(identificator);
            await _context.SaveChangesAsync();
            return identificator.HexIdentificator;
        }

        public async Task<Identificator> GetUser(string hexIdentificator)
        {
            var identificator = await _context.Identificators.SingleOrDefaultAsync(i => i.HexIdentificator== hexIdentificator);

            return identificator;
        }
    }
}
