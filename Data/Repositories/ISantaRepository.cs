using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasWishlist_Data.Repositories
{
    public interface ISantaRepository
    {
        bool NameExists(string nane);
    }
}
