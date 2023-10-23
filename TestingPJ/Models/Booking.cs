using System;
using System.Collections.Generic;

namespace TestingPJ.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Chitietdvs = new HashSet<Chitietdv>();
            Thuocs = new HashSet<Thuoc>();
            MaKhachHangs = new HashSet<Khachhang>();
        }

        public string MaDatPhong { get; set; } = null!;
        public string MaHoaDon { get; set; } = null!;
        public DateTime NgayDat { get; set; }
        public string? YeuCau { get; set; }
        public int SoNguoi { get; set; }

        public virtual Hoadon MaHoaDonNavigation { get; set; } = null!;
        public virtual ICollection<Chitietdv> Chitietdvs { get; set; }
        public virtual ICollection<Thuoc> Thuocs { get; set; }

        public virtual ICollection<Khachhang> MaKhachHangs { get; set; }
    }
}
