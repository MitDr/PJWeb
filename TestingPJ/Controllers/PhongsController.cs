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
    public class PhongsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public PhongsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Phongs
        public async Task<IActionResult> Index()
        {
            return _context.Phongs != null ?
                        View(await _context.Phongs.ToListAsync()) :
                        Problem("Entity set 'Quan_ly_buong_phongContext.Phongs'  is null.");
        }

        // GET: Phongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .FirstOrDefaultAsync(m => m.TenPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        // GET: Phongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenPhong,LoaiPhong,GiaCa,ViewPhong,SoGiuong,LoaiPhongTam,LoaiGiuong,KichThuoc,BanCong")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phong);
        }

        // GET: Phongs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null)
            {
                return NotFound();
            }
            return View(phong);
        }

        // POST: Phongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenPhong,LoaiPhong,GiaCa,ViewPhong,SoGiuong,LoaiPhongTam,LoaiGiuong,KichThuoc,BanCong")] Phong phong)
        {
            if (id != phong.TenPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongExists(phong.TenPhong))
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
            return View(phong);
        }

        // GET: Phongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .FirstOrDefaultAsync(m => m.TenPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        // POST: Phongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Phongs == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Phongs'  is null.");
            }
            var phong = await _context.Phongs.FindAsync(id);
            if (phong != null)
            {
                _context.Phongs.Remove(phong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongExists(string id)
        {
            return (_context.Phongs?.Any(e => e.TenPhong == id)).GetValueOrDefault();
        }

		[HttpGet("available-rooms")]
		public IActionResult GetAvailableRooms(DateTime ngayDen, DateTime ngayDi)
		{
            var availableRooms = _context.Thuocs
        .Where(t => t.NgayDen >= ngayDi || t.NgayDi <= ngayDen)
        .Join(_context.Phongs,
              thuoc => thuoc.TenPhong,
              phong => phong.TenPhong,
              (thuoc, phong) => new { phong.TenPhong, phong.LoaiPhong, phong.GiaCa, phong.SoGiuong, phong.LoaiGiuong })  // Add other properties here
        .ToList();

            return Ok(availableRooms);
		}
        public IActionResult BookRoom()
        {
            return View();
        }
        [HttpGet("Phongs/available-rooms2")]
        public IActionResult GetAvailableRooms2(DateTime ngayDen, DateTime ngayDi)
        {
            var availableRooms = _context.Thuocs
        .Where(t => t.NgayDen >= ngayDi || t.NgayDi <= ngayDen)
        .Join(_context.Phongs,
              thuoc => thuoc.TenPhong,
              phong => phong.TenPhong,
              (thuoc, phong) => new { phong.TenPhong, phong.LoaiPhong, phong.GiaCa, phong.SoGiuong, phong.LoaiGiuong })  // Add other properties here
        .ToList();

            return Ok(availableRooms);
        }
    }
}
