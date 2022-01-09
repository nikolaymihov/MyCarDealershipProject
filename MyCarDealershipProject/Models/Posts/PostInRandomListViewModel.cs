namespace MyCarDealershipProject.Models.Posts
{
    using Cars;

    public class PostInRandomListViewModel
    {
        public RandomCarViewModel Car { get; init; }

        public string PublishedOn { get; init; }
    }
}