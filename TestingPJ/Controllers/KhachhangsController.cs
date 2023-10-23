using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingPJ.Models;

namespace TestingPJ.Controllers
{
    public class KhachhangsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public KhachhangsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Khachhangs
        public async Task<IActionResult> Index()
        {
              return _context.Khachhangs != null ? 
                          View(await _context.Khachhangs.ToListAsync()) :
                          Problem("Entity set 'Quan_ly_buong_phongContext.Khachhangs'  is null.");
        }

        // GET: Khachhangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // GET: Khachhangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Khachhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhachHang,Ho,Dem,Ten,NgaySinh,Cccd,GioiTinh,PhanLoai,Sdt,Email")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: Khachhangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        // POST: Khachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKhachHang,Ho,Dem,Ten,NgaySinh,Cccd,GioiTinh,PhanLoai,Sdt,Email")] Khachhang khachhang)
        {
            if (id != khachhang.MaKhachHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.MaKhachHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: Khachhangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Khachhangs == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Khachhangs'  is null.");
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(string id)
        {
          return (_context.Khachhangs?.Any(e => e.MaKhachHang == id)).GetValueOrDefault();
        }
        public IActionResult SearchCustomers()
        {
            return View();
        }

        [HttpGet("Khachhangs/search-customer")]
        public IActionResult SearchCustomer(string maKhachHang, string sdt)
        {
            var query = _context.Khachhangs.AsQueryable();

            if (!string.IsNullOrEmpty(maKhachHang))
            {
                query = query.Where(k => k.MaKhachHang.Contains(maKhachHang));
            }

            if (!string.IsNullOrEmpty(sdt))
            {
                query = query.Where(k => k.Sdt.Contains(sdt));
            }

            var results = query.ToList();

            return Ok(results);
        }
    }
}
