using KoiShow.Data.Models;
using KoiShow.Data.Repository;

namespace KoiShow.Data;

public class UnitOfWork
{
    private FA24_SE1716_PRN231_G2_KoiShowContext context;
    private ContestResultRepository contestResultRepository;
    private ContestRepository contestRepository;
    private AccountRepository accountRepository;
    private PointRepository pointRepository;

    public UnitOfWork()
    {
        context ??= new FA24_SE1716_PRN231_G2_KoiShowContext();
    }

    public ContestResultRepository ContestResultRepository
    {
        get
        {
            return contestResultRepository ??= new ContestResultRepository(context);
        }
    }

    public ContestRepository ContestRepository
    {
        get
        {
            return contestRepository ??= new ContestRepository(context);
        }
    }

    public AccountRepository AccountRepository
    {
        get
        {
            return accountRepository ??= new AccountRepository(context);
        }
    }

    public PointRepository PointRepository
    {
        get
        {
            return pointRepository ??= new PointRepository(context);
        }
    }
}   