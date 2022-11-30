namespace Machine
{
    class Program
    {
        static List<Movies> movieList = new List<Movies>();
        static List<Transactions> transactionList = new List<Transactions>();

        enum MovieGenre
        {
            Action = 1,
            Family = 2,
            Horror = 3,
            SciFi = 4,
            Comedy = 5,
            Other = 6
        }

        static void Main(string[] args)
        {
            PopulateMovies();
            PopulateTransactions();
            Console.WriteLine();
            Console.WriteLine("1. Managerial Functions");
            Console.WriteLine("2. Customer Functions");
            Console.Write("Select option from 1 or 2. Press any other key to exit: ");
            string option = Console.ReadLine();
            if(option == "1")
            {
                ManagerialFunctions();
            }
            else if (option == "2")
            {
                CustomerFunction();
            }
            else
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Thank you for using. Good Bye!");
            Console.ReadKey();
        }

        private static void PopulateMovies()
        {
            if (File.Exists("movieinventory.txt"))
            {
                string ReadText = File.ReadAllText("movieinventory.txt");
                if (ReadText != "")
                {
                    foreach (var line in ReadText.Split('\n'))
                    {
                        if(line != "")
                        {
                            string[] mov = line.Split('#');
                            Movies movies = new Movies(Convert.ToInt32(mov[0]), mov[1], mov[2], Convert.ToBoolean(mov[3]), Convert.ToInt32(mov[4]));
                            movieList.Add(movies);
                        }
                    }
                }
            }
        }

        private static void PopulateTransactions()
        {
            if (File.Exists("transactions.txt"))
            {
                string ReadText = File.ReadAllText("transactions.txt");
                if (ReadText != "")
                {
                    foreach (var line in ReadText.Split('\n'))
                    {
                        if (line != "")
                        {
                            string[] txn = line.Split('#');
                            Transactions transactions = new Transactions(Convert.ToInt32(txn[0]), txn[1], Convert.ToInt32(txn[2]), txn[3], txn[4], txn[5], txn[6]);
                            transactionList.Add(transactions);
                        }
                    }
                }
            }
        }

        private static void ManagerialFunctions()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Add a new Movie");
                Console.WriteLine("2. Remove a Movie");
                Console.WriteLine("3. Edit a Movie");
                Console.WriteLine("4. Report Menu");
                Console.Write("Select option from 1 to 4. Press any other key to exit: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    AddMovie();
                }
                else if (option == "2")
                {
                    RemoveMovie();
                }
                else if (option == "3")
                {
                    EditMovie();
                }
                else if (option == "4")
                {
                    Reports();
                }
                else
                {
                    break;
                }
            } while (true);
        }

        private static void AddMovie()
        {

            Console.Write("Enter movie title: ");
            string movieTitle = Console.ReadLine();

            Console.WriteLine("1. Action");
            Console.WriteLine("2. Family");
            Console.WriteLine("3. Horror");
            Console.WriteLine("4. Sci-Fi");
            Console.WriteLine("5. Comedy");
            Console.WriteLine("6. Other");
            Console.Write("Select movie genre (1 to 5): ");
            string movieGenreId = Console.ReadLine();

            MovieGenre myEnum = (MovieGenre)Enum.Parse(typeof(MovieGenre), movieGenreId);

            int moviesId = movieList.DefaultIfEmpty().Max(p => p == null ? 0 : p.MovieID + 1);

            Movies movie = new Movies(moviesId, movieTitle, myEnum.ToString(), true, 0);
            movieList.Add(movie);

            UpdateovieInventoryInFile();
     
        }

        private static void UpdateovieInventoryInFile()
        {
            StreamWriter OutPutFile = File.CreateText("movieinventory.txt");
            foreach (Movies movie in movieList)
            {
                OutPutFile.Write(movie.MovieID + "#" + movie.MovieTitle + "#" + movie.Genre + "#" + movie.InStock + "#" + movie.Count + "\n");
            }
            OutPutFile.Close();
        }

        private static void DisplayMovieInventory()
        {
            foreach (Movies movie in movieList)
            {
                Console.WriteLine(movie.MovieID + ". " + movie.MovieTitle + " (" + movie.Genre + ")" + " - " + (movie.InStock == true ? "In Stock" : "Not in stock"));
            }
        }

        private static void RemoveMovie()
        {
            DisplayMovieInventory();

            Console.Write("Select movie id to remove: ");
            int movieId = Convert.ToInt32(Console.ReadLine());

            movieList.RemoveAt(movieId);

            UpdateovieInventoryInFile();
        }

        private static void EditMovie()
        {
            DisplayMovieInventory();

            Console.Write("Select movie id to edit: ");
            int movieId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter movie title: ");
            string movieTitle = Console.ReadLine();

            Console.WriteLine("1. Action");
            Console.WriteLine("2. Family");
            Console.WriteLine("3. Horror");
            Console.WriteLine("4. Sci-Fi");
            Console.WriteLine("5. Comedy");
            Console.WriteLine("6. Other");
            Console.Write("Select movie genre (1 to 5): ");
            string movieGenreId = Console.ReadLine();

            MovieGenre myEnum = (MovieGenre)Enum.Parse(typeof(MovieGenre), movieGenreId);

            movieList[movieId].MovieTitle = movieTitle;
            movieList[movieId].Genre = myEnum.ToString();

            UpdateovieInventoryInFile();

        }

        private static void MoviesAvailableForRent()
        {
            foreach (Movies movie in movieList)
            {
                if(movie.InStock)
                {
                    Console.WriteLine(movie.MovieID + ". " + movie.MovieTitle + " (" + movie.Genre + ")");
                }
            }
        }

        private static void MoviesCurrentlyRented()
        {
            foreach (Movies movie in movieList)
            {
                if (!movie.InStock)
                {
                    Console.WriteLine(movie.MovieID + ". " + movie.MovieTitle + " (" + movie.Genre + ")");
                }
            }
        }


        private static void RentedMovies()
        {
            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            foreach (Transactions transactions in transactionList)
            {
                if (transactions.CustomerEmail == email && transactions.ReturnDate == "none")
                {
                    Console.WriteLine(transactions.TransactionID + ". " + transactions.MovieTitle + " (" + transactions.Genre + ")");
                }
            }
        }

        private static void UpdateoTransactionsInFile()
        {
            StreamWriter OutPutFile = File.CreateText("transactions.txt");
            foreach (Transactions transactions in transactionList)
            {
                OutPutFile.Write(transactions.TransactionID + "#" + transactions.CustomerEmail + "#" + transactions.MovieID + "#" + transactions.MovieTitle + "#" + transactions.Genre + "#" + transactions.RentalDate + "#" + transactions.ReturnDate + "\n");
            }
            OutPutFile.Close();
        }

        private static void RentMovie()
        {
            MoviesAvailableForRent();

            Console.Write("Select movie id to rent: ");
            int movieId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            int transactionId = transactionList.DefaultIfEmpty().Max(p => p == null ? 0 : p.TransactionID + 1);

            Transactions transactions = new Transactions(transactionId, email, movieId, movieList[movieId].MovieTitle, movieList[movieId].Genre, DateTime.Now.ToString("dd/MM/yyyy"), "none");
            transactionList.Add(transactions);

            movieList[movieId].InStock = false;
            movieList[movieId].Count = movieList[movieId].Count + 1;

            UpdateoTransactionsInFile();
            UpdateovieInventoryInFile();
        }

        private static void ReturnMovie()
        {
            RentedMovies();

            Console.Write("Select transaction id to return: ");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            transactionList[transactionId].ReturnDate = DateTime.Now.ToString("dd/MM/yyyy");
            movieList[transactionList[transactionId].MovieID].InStock = true;

            UpdateoTransactionsInFile();
            UpdateovieInventoryInFile();
        }

        private static void CustomerFunction()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. View movies available to rent");
                Console.WriteLine("2. Rent a movie");
                Console.WriteLine("3. View movies currently rented");
                Console.WriteLine("4. Return a movie");
                Console.Write("Select option from 1 to 4. Press any other key to exit: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    MoviesAvailableForRent();
                }
                else if (option == "2")
                {
                    RentMovie();
                }
                else if (option == "3")
                {
                    RentedMovies();
                }
                else if (option == "4")
                {
                    ReturnMovie();
                }
                else
                {
                    break;
                }
            } while (true);
        }

        private static void Reports()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. In stock report");
                Console.WriteLine("2. Movies currently rented");
                Console.WriteLine("3. Rental totals per genre");
                Console.WriteLine("4. Top 5 movies in terms of total rentals");
                Console.Write("Select option from 1 to 4. Press any other key to exit: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine();
                    MoviesAvailableForRent();
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                }
                else if (option == "2")
                {
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine();
                    MoviesCurrentlyRented();
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                }
                else if (option == "3")
                {
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine();

                    Console.WriteLine("Action: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.Action.ToString()).Count());
                    Console.WriteLine("Comedy: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.Comedy.ToString()).Count());
                    Console.WriteLine("Family: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.Family.ToString()).Count());
                    Console.WriteLine("Horror: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.Horror.ToString()).Count());
                    Console.WriteLine("SciFi: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.SciFi.ToString()).Count());
                    Console.WriteLine("Other: " + transactionList.Where(s => s != null && s.Genre == MovieGenre.Other.ToString()).Count());

                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                }
                else if (option == "4")
                {
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine();

                    List<Movies> orderList = movieList.OrderByDescending(s => s.Count).ToList();

                    int counter = 0;
                    foreach(Movies movie in orderList)
                    {
                        if(counter == 5)
                        {
                            break;
                        } 
                        else
                        {
                            Console.WriteLine((counter + 1) +  ". " + movie.MovieTitle);
                        }
                        counter++;
                    }

                    Console.WriteLine("_____________________________");
                    Console.WriteLine("_____________________________");
                }
                else
                {
                    break;
                }
            } while (true);
        }
    }
}