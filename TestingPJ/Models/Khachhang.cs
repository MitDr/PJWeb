using System;
using System.Collections.Generic;

namespace TestingPJ.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            MaDatPhongs = new HashSet<Booking>();
        }

        public string MaKhachHang { get; set; } = null!;
        public string Ho { get; set; } = null!;
        public string? Dem { get; set; }
        public string Ten { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string Cccd { get; set; } = null!;
        public bool GioiTinh { get; set; }
        public int PhanLoai { get; set; }
        public string Sdt { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<Booking> MaDatPhongs { get; set; }
    }
}
