using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Reservation_DAL : Master_DAL
	{
		private const string SQL_CreateReservation = "INSERT INTO reservation (site_id, name, from_date, to_date, create_date)  " +
													 "VALUES(@site_id, @name, @from_date, @to_date, @create_date);";

		private const string SQL_RetrieveMostRecentReservation = "SELECT MAX(reservation_id) FROM reservation;";

		// Create reservation
		public void CreateReservation(int siteID, string name, DateTime? arrivalDate, DateTime? departureDate)
		{
			// Converts DateTime object arrivalDate to string
			string arrivalConversion1 = arrivalDate.ToString();
			// Splits DateTime object to isolate date
			string[] arrivalConversion2 = arrivalConversion1.Split(' ');
			// Stores ONLY date, as string, inside a variable
			string arrivalConversion3 = arrivalConversion2[0];

			// ... Ditto
			string departureConversion1 = departureDate.ToString();
			string[] departureConversion2 = departureConversion1.Split(' ');
			string departureConversion3 = departureConversion2[0];

			try
			{
				using (var conn = new SqlConnection(dbConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand(SQL_CreateReservation, conn))
					{
						cmd.Parameters.AddWithValue("@site_id", siteID);
						cmd.Parameters.AddWithValue("@name", name);
						cmd.Parameters.Add("@from_date", SqlDbType.Date).Value = arrivalConversion3;
						cmd.Parameters.Add("@to_date", SqlDbType.Date).Value = departureConversion3;
						cmd.Parameters.AddWithValue("@create_date", DateTime.Now);

						conn.Open();

						cmd.ExecuteNonQuery();

						using (SqlCommand confirmation = new SqlCommand(SQL_RetrieveMostRecentReservation, conn))
						{
							int newestReservation = (int)confirmation.ExecuteScalar();

							Console.WriteLine($"Your reservation has been made and the confirmation id is {newestReservation}");
						}
					}
				}
			}
			catch (SqlException)
			{
				Console.WriteLine("Sorry, your reservation could not be created.");
				throw;
			}
		}

		//  Get the most recent reservation by ID
		public int RetrieveMostRecentReservation()
		{
			int newestReservation = 0;

			try
			{
				using (var conn = new SqlConnection(dbConnectionString))
				{
					SqlCommand cmd = new SqlCommand(SQL_RetrieveMostRecentReservation, conn);

					newestReservation = (int)cmd.ExecuteScalar();
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Your reservation could not be retrieved.");
			}

			return newestReservation;
		}

		// THE ABOVE METHOD DOES NOT WORK--
		// WHY?

		// BONUS:  Get reservations within next 30 days
		// Returns a list of Reservations
	}
}
