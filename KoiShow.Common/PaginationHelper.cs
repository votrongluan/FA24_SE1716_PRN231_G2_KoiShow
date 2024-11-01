using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Common
{
    public class PaginationHelper
    {
        public static List<T> GetPaged<T>(List<T>? source, int pageIndex, int pageSize)
        {
            if (source == null || source.Count == 0)
            {
                source = new List<T>();
                return source;
            }
            return source.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();
        }

        public static int GetTotalPages<T>(List<T> source, int pageSize)
        {
            if (source == null || source.Count == 0)
            {
                source = new List<T>();
                return 0;
            }
            return (int)Math.Ceiling(source.Count / (double)pageSize);
        }
    }
}
