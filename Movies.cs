namespace Machine
{
    public class Movies
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public bool InStock { get; set; }
        public int Count { get; set; }

        public Movies(int MovieID, string MovieTitle, string Genre, bool InStock, int Count)
        {
            this.MovieID = MovieID;
            this.MovieTitle = MovieTitle;
            this.Genre = Genre;
            this.InStock = InStock;
            this.Count = Count;
        }
    }
}