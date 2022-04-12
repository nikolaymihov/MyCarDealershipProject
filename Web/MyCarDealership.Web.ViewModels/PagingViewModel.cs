namespace MyCarDealership.Web.ViewModels
{
    using System;

    public abstract class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.PostsCount / this.PostsPerPage);

        public int PostsCount { get; set; }

        public int PostsPerPage { get; set; }

        public int FirstPage => 1;

        public int LastPage => this.PagesCount;
    }
}
