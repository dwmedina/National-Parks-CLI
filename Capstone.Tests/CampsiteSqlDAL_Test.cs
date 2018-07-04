using System;
using System.Collections.Generic;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class CampsiteSqlDAL_Test : CapstoneDBTests
    {
        [TestMethod]
        public void GetCampsites_Test()
        {
            Campground_DAL testCampgroundDAL = new Campground_DAL();
            Campsite_DAL testCampsiteDAL = new Campsite_DAL();
            Park_DAL testParkDAL = new Park_DAL();
            Park testPark = testParkDAL.GetParks()[2];
            IList<Campground> testCampgrounds = testCampgroundDAL.GetCampgroundsByPark(testPark.ParkID);
            List<Campsite> testCampsites = testCampsiteDAL.GetCampsitesByCampground(testCampgrounds[0].CampID, DateTime.Parse("2018-10-10"), DateTime.Parse("2018-10-12"));
            int siteCount = testCampsites.Count;
            // check the count of sites to make sure we're getting the right data
            Assert.AreEqual(1, siteCount);
        }
    }
}
