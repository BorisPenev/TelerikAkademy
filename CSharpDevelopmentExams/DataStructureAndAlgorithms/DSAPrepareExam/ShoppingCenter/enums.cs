namespace ShoppingCenter
{
    public enum CommandTypes
    {
        AddProduct,
        DeleteProducts,
        FindProductsByName,
        FindProductsByPriceRange,
        FindProductsByProducer,
    }

    public enum ContentItemTypes
    {
        Name,
        Price,
        Producer,
    }
}