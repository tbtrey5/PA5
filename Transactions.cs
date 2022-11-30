namespace Machine
{
    public class Transactions
    {
        public int TransactionID { get; set; }
        public string CustomerEmail { get; set; }
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public string RentalDate { get; set; }
        public string ReturnDate { get; set; }

        public Transactions(int TransactionID, string CustomerEmail, int MovieID, string MovieTitle, string Genre, string RentalDate, string ReturnDate)
        {
            this.TransactionID = TransactionID;
            this.CustomerEmail = CustomerEmail;
            this.RentalDate = RentalDate;
            this.ReturnDate = ReturnDate;
            this.MovieID = MovieID;
            this.MovieTitle = MovieTitle;
            this.Genre = Genre;
        }
    }
}