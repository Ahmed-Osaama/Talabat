using Shared.Enums;

namespace Shared
{
    public class ProductSpecificationsParameters
    {
        private const int MaxPageSize = 10;
        private const int defaultPageSize = 6;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pageSize = defaultPageSize;

        public int MyProperty
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? pageSize = MaxPageSize : value; }
        }

    }
}
