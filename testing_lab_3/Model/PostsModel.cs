using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Model
{
    class Booking
    {
        public string bookingId { get; set; }
    }
    public class Bookingdates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }

    public class PostsModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public Bookingdates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}
