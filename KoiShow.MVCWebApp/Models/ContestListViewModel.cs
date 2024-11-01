using KoiShow.Data.Models;

namespace KoiShow.MVCWebApp.Models
{
    public class ContestListViewModel
    {
        public List<Contest> Contests { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
