namespace BookStoreApp.API.Repositories
{
    public class VirtualizeResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalSize { get; set; }

    }
}
