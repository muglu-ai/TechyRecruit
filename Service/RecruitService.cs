using TechyRecruit.Data;
using TechyRecruit.Models;

namespace TechyRecruit.Service;

public interface IRecruitService
{
    List<RecruitModel> GetRecruits();
}

public class RecruitService : IRecruitService
{
    private readonly TechyRecruitContext _context;

    public RecruitService(TechyRecruitContext context)
    {
        _context = context;
    }

    public List<RecruitModel> GetRecruits()
    {
        return _context.RecruitModel.ToList();
    }
}