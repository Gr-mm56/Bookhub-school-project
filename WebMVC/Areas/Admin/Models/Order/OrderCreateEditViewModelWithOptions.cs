namespace WebMVC.Areas.Admin.Models.Order;

public class OrderCreateEditViewModelWithOptions
{
    public required OrderCreateEditViewModel Order { get; set; }

    public List<UserOption> Users { get; set; } = [];

    public List<PurchaseItemOption> PurchaseItems { get; set; } = [];
}

public class UserOption
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }
}

public class PurchaseItemOption
{
    public int Id { get; set; }

    public required int BookId { get; set; }

    public required string BookTitle { get; set; }

    public required double BookPrice { get; set; }

    public required int Count { get; set; }
}
