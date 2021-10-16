namespace Riode.Domain.Models.Entities
{
    public class ProductSizeColorItem:BaseEntity
    {
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long SizeId { get; set; }
        public virtual Size Size { get; set; }
        public long ColorId { get; set; }
        public virtual Color Color { get; set; }

    }
}
