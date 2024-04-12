
namespace OrderDotzCatalogByDiscount
{
    public class Rootobject
    {
        public Products products { get; set; }
    }

    public class Navigation
    {
        public object tags { get; set; }
        public object minPoints { get; set; }
        public object maxPoints { get; set; }
    }

    public class Products
    {
        public int _pageSize { get; set; }
        public int _page { get; set; }
        public int _total { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string? sku { get; set; }
        public string? shortName { get; set; }
        public string? name { get; set; }
        public float originalPoints { get; set; }
        public float points { get; set; }
        public float discountRate { get; set; }
        public string? supplier { get; set; }
        public float price { get; set; }
        public string? ean { get; set; }
        public int availableTotal { get; set; }
        public int stockTotal { get; set; }
        public bool isOutlet { get; set; }
    }
}
