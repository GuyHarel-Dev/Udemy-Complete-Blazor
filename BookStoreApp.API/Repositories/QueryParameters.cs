using Humanizer.Localisation.NumberToWords;
using Microsoft.Build.Evaluation;

namespace BookStoreApp.API.Repositories
{
    public class QueryParameters
    {
        private int _pageSize = 15;
        public int StartIndex { get; set; }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

    }
}
