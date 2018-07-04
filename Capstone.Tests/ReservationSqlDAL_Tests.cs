using System;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDAL_Tests : CapstoneDBTests
    {
        private Reservation_DAL dal = new Reservation_DAL();

        [TestMethod]
        public void CreateReservation_Test()
        {
            Reservation r = new Reservation();
            r.SiteID = 5;
            r.Name = "Knight Artorias";
            // This is our "From Date"
            r.FirstDateBooked = Convert.ToDateTime("2018-07-08");
            // This is our "To Date"
            r.LastDateBooked = Convert.ToDateTime("2018-07-10");

            //  Test for CreateReservation by looking for existence
            Assert.IsNotNull(r.DateTimeReservationPlaced);
        }

        [TestMethod]
        public void ReadReservation_Test()
        {
            string SQL_RowCount = "SELECT COUNT(*) FROM reservation;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand checkRowCount = new SqlCommand(SQL_RowCount, conn))
                    {
                        conn.Open();
                        int rowCount = (int) checkRowCount.ExecuteScalar();
                        Assert.AreEqual(2, rowCount);

                        dal.CreateReservation(2, "Test", DateTime.Parse("2018-10-10"), DateTime.Parse("2018-10-11"));

                        rowCount = (int)checkRowCount.ExecuteScalar();
                        Assert.AreEqual(3, rowCount);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }
    }
}
