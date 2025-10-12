using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Common
{
    public class PagedRequestDto
    {
        [Range(1, 100)]
        public int Limit { get; set; } = 20;
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;
    }
}

