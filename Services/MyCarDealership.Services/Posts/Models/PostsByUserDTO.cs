namespace MyCarDealership.Services.Posts.Models
{
    using System.Collections.Generic;

    public class PostsByUserDTO
    {
        public IEnumerable<PostByUserDTO> Posts { get; init; }
    }
}