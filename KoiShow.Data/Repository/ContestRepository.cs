using KoiShow.Data.Base;
using KoiShow.Data.Models;

namespace KoiShow.Data.Repository;

public class ContestRepository : GenericRepository<Contest>
{
    public ContestRepository()
    {
        
    }

    public ContestRepository(FA24_SE1716_PRN231_G2_KoiShowContext context)
    {
        _context = context;
    }
}