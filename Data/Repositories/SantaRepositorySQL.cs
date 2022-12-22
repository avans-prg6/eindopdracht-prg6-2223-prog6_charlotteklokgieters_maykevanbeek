using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasWishlist_Data.Repositories
{
    public class SantaRepositorySQL : ISantaRepository
    {
        private readonly SantaDbContext _context;

        public SantaRepositorySQL(SantaDbContext context)
        {
            _context = context;
        }

        bool ISantaRepository.NameExists(string name)
        {
            if (_context.Users.Where(x => x.UserName == name).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }
    }
}
