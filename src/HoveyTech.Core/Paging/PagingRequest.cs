namespace HoveyTech.Core.Paging
{
    public interface  IPagingRequest
    {
        int Page { get; set; }

        int PageSize { get; set; }

        void NextPage();
    }

    public class PagingRequest : IPagingRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public PagingRequest()
        {
            Page = 1;
            PageSize = 20;
        }

        public void NextPage()
        {
            Page += 1;
        }

        public void PreviousPage()
        {
            if (Page <= 1)
            {
                Page = 1;
                return;
            }

            Page -= 1;
        }
    }
}
