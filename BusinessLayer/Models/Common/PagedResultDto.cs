using System.Collections.Generic;

namespace BusinessLayer.Models.Common
{
    public class PagedResultDto<T>
    {
        public int Total { get; set; }
        public required IEnumerable<T> Items { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}

