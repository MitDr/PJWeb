using System;
using System.Collections.Generic;

namespace TestingPJ.Models
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Bookings = new HashSet<Booking>();
        }

        public string MaHoaDon { get; set; } = null!;
        public DateTime NgayThanhToan { get; set; }
        public string HìnhThucThanhToan { get; set; } = null!;
        public int? TongTien { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
