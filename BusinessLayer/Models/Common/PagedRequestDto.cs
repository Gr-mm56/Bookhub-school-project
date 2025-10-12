namespace BusinessLayer.Models.Common
{
    public class PagedRequestDto
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
    }
}

