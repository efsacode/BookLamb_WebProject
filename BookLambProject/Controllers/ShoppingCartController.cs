using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookLambProject.Data;
using BookLambProject.Models;
using BookLambProject.Utilities;

namespace BookLambProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly BookLambProjectContext _context;

        public ShoppingCartController(BookLambProjectContext context)
        {
            _context = context;
        }

        [Authentication]
        public IActionResult Index()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            // Retrieve shopping cart items and display them
            var cartItems = _context.ShoppingCart.Include(item => item.Book).ToList();

            return View(cartItems);
        }

        public async Task<IActionResult> AddToCart(int bookId)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            var book = await _context.Book.FindAsync(bookId);

            if (book != null)
            {
                // Checks if the item is already in the cart
                var cartItem = _context.ShoppingCart.SingleOrDefault(
                    c => c.BookId == bookId);

                if (cartItem == null)
                {
                    // If not adds it to the cart
                    cartItem = new ShoppingCart
                    {
                        Book = book,
                        Quantity = 1
                    };
                    _context.ShoppingCart.Add(cartItem);
                }
                else
                {
                    // If yes, increase the quantity
                    cartItem.Quantity++;
                }

                await _context.SaveChangesAsync();
            }



            return RedirectToAction("Index");

        }
        // GET: ShoppingCart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,Quantity")] ShoppingCart shoppingCart)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", shoppingCart.BookId);
            return View(shoppingCart);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", shoppingCart.BookId);
            return View(shoppingCart);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,Quantity")] ShoppingCart shoppingCart)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", shoppingCart.BookId);
            return View(shoppingCart);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCart == null)
            {
                return Problem("Entity set 'Book_LambContext.ShoppingCart'  is null.");
            }
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCart.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
            return (_context.ShoppingCart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
