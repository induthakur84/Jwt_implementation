namespace Jwt_implementation.Dto
{
    public class ProductResponseDto
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }

    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }



    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }
}
