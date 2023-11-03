namespace CookingRecipeApi.Utility
{
    public class Pagination<T>
    {
        public List<T> List { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }

        public Pagination(IQueryable<T> query, int perPage, int currentPage)
        {
            var total = query.Count();
            int tmpByInt = total / perPage;
            double tmpByDouble = (double)total / (double)perPage;
            int totalPages = 1;
            if (tmpByDouble > (double)tmpByInt)
            {
                totalPages = tmpByInt + 1;
            }
            else
            {
                totalPages = tmpByInt;
            }
            query = query.Skip((currentPage - 1) * perPage).Take(perPage);

            List = query.ToList();
            TotalPages = totalPages;
            TotalRecords = total;
            PerPage = perPage;
            CurrentPage = currentPage;
        }

        public Pagination(List<T> data, int totalPages, int totalRecords, int perPage, int currentPage)
        {
            List = data;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            PerPage = perPage;
            CurrentPage = currentPage;
        }
    }
}
