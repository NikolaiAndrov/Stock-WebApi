namespace Stock.WebApi.DtoModels.Stock
{
    using Comment;

    public class StockDto
    {
        public StockDto()
        {
            this.Comments = new List<CommentDto>();
        }

        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public long MarketCap { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
