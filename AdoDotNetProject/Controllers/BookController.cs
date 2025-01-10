using AdoDotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace AdoDotNetProject.Controllers
{
    public class BookController : Controller
    {
        private readonly string _connectionString;

		//public BookController(string connectionString)
		//{
		//	_connectionString = connectionString;
		//}

		public BookController(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbConn");
		}

		public IActionResult Index()
        {
			SqlConnection sqlConnection = new SqlConnection(_connectionString);

			sqlConnection.Open();

			SqlCommand cmd = new SqlCommand("select * from Book", sqlConnection);

			SqlDataAdapter adapter = new SqlDataAdapter(cmd);

			DataTable dataTable = new DataTable();

			adapter.Fill(dataTable);

			sqlConnection.Close();

			List<Book> books = new List<Book>();

            foreach (DataRow row in dataTable.Rows)
            {
				//            foreach (var item in row.ItemArray)
				//{
				//	return View(item.ToString());
				//}

				Book book = new Book()
				{
					Author = row["Author"].ToString(),
					AvailableCopies = int.Parse(row["AvailableCopies"].ToString()),
					BookID = Convert.ToInt16(row["BookID"].ToString()),
					PublicationYear = row["PublicationYear"].ToString(),
					Title = row["Title"].ToString()
				};

				books.Add(book);
            }

			return View(books);
        }

		[HttpGet]

		public IActionResult AddBook()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddBook(Book book)
		{
			SqlConnection sqlConnection = new SqlConnection(_connectionString);

			sqlConnection.Open();

			SqlCommand cmd = new SqlCommand
				("insert into Book (Title,Author,PublicationYear,AvailableCopies) values (@title,@Author,@publicationYear,@availableCopies)", sqlConnection);

			cmd.Parameters.AddWithValue("@title", book.Title);
			cmd.Parameters.AddWithValue("@Author", book.Author);
			cmd.Parameters.AddWithValue("@publicationYear", book.PublicationYear);
			cmd.Parameters.AddWithValue("@availableCopies", book.AvailableCopies);

			//cmd.ExecuteNonQuery();

			SqlDataAdapter adapter = new SqlDataAdapter(cmd);

			DataTable datatable = new DataTable();

			adapter.Fill(datatable);

			sqlConnection.Close();

			return RedirectToAction("Index");
		}

		[HttpGet]

		public IActionResult UpdateBook(int id)
		{
			SqlConnection sqlConnection = new SqlConnection( _connectionString);

			sqlConnection.Open();

			SqlCommand cmd = new SqlCommand("select * from Book where BookID = @bookID",sqlConnection);

			cmd.Parameters.AddWithValue("@bookID", id);

			//cmd.ExecuteNonQuery();

			SqlDataReader reader = cmd.ExecuteReader();

			Book book = null;

			if (reader.Read())
			{
				book = new Book
				{
					BookID = Convert.ToInt32(reader["BookID"]),
					Title = reader["Title"].ToString(),
					Author = reader["Author"].ToString(),
					PublicationYear = reader["PublicationYear"].ToString(),
					AvailableCopies = Convert.ToInt32(reader["AvailableCopies"])
				};
			}

			sqlConnection.Close();

			if (book == null)
			{
				return View("HATA"); 
			}

			return View(book);
		}

		[HttpPost]

		public IActionResult UpdateBook(Book book)
		{
			SqlConnection sqlConnection = new SqlConnection(_connectionString);

			sqlConnection.Open();

			SqlCommand cmd = new SqlCommand("update Book set Title = @title, Author = @author, PublicationYear = @publicationYear, AvailableCopies = @availableCopies where BookID = @bookID",sqlConnection);

			cmd.Parameters.AddWithValue("@title", book.Title);
			cmd.Parameters.AddWithValue("@author", book.Author);
			cmd.Parameters.AddWithValue("@publicationYear", book.PublicationYear);
			cmd.Parameters.AddWithValue("@availableCopies", book.AvailableCopies);
			cmd.Parameters.AddWithValue("@bookID", book.BookID);

			cmd.ExecuteNonQuery();

			sqlConnection.Close();

			return RedirectToAction("Index");
		}

		public IActionResult DeleteBook(int id)
		{
			SqlConnection sqlConnection = new SqlConnection(_connectionString);

			sqlConnection.Open();

			SqlCommand cmd = new SqlCommand("delete from Book where BookID = @bookID", sqlConnection);

			cmd.Parameters.AddWithValue("@bookID", id);

			cmd.ExecuteNonQuery();

			sqlConnection.Close();

			return RedirectToAction("Index");
		}
    }
}
