﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingPJ.Models;

namespace TestingPJ.Controllers
{
    public class BookingsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public BookingsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var quan_ly_buong_phongContext = _context.Bookings.Include(b => b.MaHoaDonNavigation);
            return View(await quan_ly_buong_phongContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.MaHoaDonNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["MaHoaDon"] = new SelectList(_context.Hoadons, "MaHoaDon", "MaHoaDon");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDatPhong,MaHoaDon,NgayDat,YeuCau,SoNguoi")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHoaDon"] = new SelectList(_context.Hoadons, "MaHoaDon", "MaHoaDon", booking.MaHoaDon);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["MaHoaDon"] = new SelectList(_context.Hoadons, "MaHoaDon", "MaHoaDon", booking.MaHoaDon);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDatPhong,MaHoaDon,NgayDat,YeuCau,SoNguoi")] Booking booking)
        {
            if (id != booking.MaDatPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.MaDatPhong))
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
            ViewData["MaHoaDon"] = new SelectList(_context.Hoadons, "MaHoaDon", "MaHoaDon", booking.MaHoaDon);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.MaHoaDonNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(string id)
        {
          return (_context.Bookings?.Any(e => e.MaDatPhong == id)).GetValueOrDefault();
        }
    }
}
