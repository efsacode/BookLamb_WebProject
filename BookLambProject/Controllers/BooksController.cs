using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookLambProject.Data;
using BookLambProject.Models;

namespace BookLambProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookLambProjectContext _context;

        public BooksController(BookLambProjectContext context)
        {
            _context = context;
            context.Database.EnsureCreated();

            if (context.Book.Any())
            {
                return;
            }

            var books = new Book[]
            {
            new Book
            {
                Title = "Dördüncü Kanat",
                ReleaseDate = DateTime.Parse("2022-08-08"),
                Genre = "Fiction",
                ImagePath = "dorduncuKanat.jpg",
                Author = "Rebecca Yarros",
                Description = "Welcome to the exclusive and brutal world of the dragonrider-only battle academy from New YorkTimes bestselling author Rebecca Yarros!",
                Publisher = "Olimpos",
                Price = 550.0m
            },
            new Book
            {
                Title = "Yirmi Bir",
                ReleaseDate = DateTime.Parse("2023-01-04"),
                Genre = "Horor",
                ImagePath = "yirmibir.jpg",
                Author = "Wulf Dorn",
                Description = "When sixteen-year-old Nikka opens her eyes in her hospital room, she can't believe what happened. While she was just having fun at a party with her best friend Zoe, she suddenly has to face the fact that she was attacked and remained dead for twenty-one minutes. Moreover, no one had seen her friend Zoe since the party, and the police's search efforts were unsuccessful.",
                Publisher = "Pegasus",
                Price = 500.0m
            },
            new Book
            {
                Title = "Tehlikeler Gezegeni",
                ReleaseDate = DateTime.Parse("2019-05-05"),
                Genre = "Fiction",
                ImagePath = "tehlikelerGezegeni.jpg",
                Author = "Tuncel Altınköprü",
                Description = "Ufaklık and Gizem find themselves on a giant spaceship against their will. Their mission, if they can achieve it:\r\nFirst of all, arriving at Donya, the Planet of Dangers...\r\n Reaching the planet that was completely submerged due to wars, fights, hostile attitudes and behaviors...\r\n If the waters covering the planet have receded in the meantime, releasing the animals on the ship to nature... Recreating the diversity of living things on the planet...\r\nTo provide a suitable environment for people struggling to survive in Donyo to live together and in peace...\r\nHowever, great dangers awaited Ufaklık and Gizem in Donyo. Could they overcome these dangers and return to Earth?",
                Publisher = "Genç Hayat",
                Price = 60.0m
            },
            new Book
            {
                Title = "The Lightning Thief",
                ReleaseDate = DateTime.Parse("2018-01-04"),
                Genre = "Fiction",
                ImagePath = "the_lightning_thief.jpg",
                Author = "Rick Riordan",
                Description = "Percy Jackson is a good kid, but he can't seem to focus on his schoolwork or control his temper. And lately, being away at boarding school is only getting worse-Percy could have sworn his pre-algebra teacher turned into a monster and tried to kill him. When Percy's mom finds out, she knows it's time that he knew the truth about where he came from, and that he go to the one place he'll be safe. She sends Percy to Camp Half-Blood, a summer camp for demigods (on Long Island), where he learns that the father he never knew is Poseidon, God of the Sea. Soon a mystery unfolds and together with his friends-one a satyr and the other the demigod daughter of Athena-Percy sets out on a quest across the United States to reach the gates of the Underworld (located in a recording studio in Hollywood) and prevent a catastrophic war between the gods.",
                Publisher = "Doğan Egmont",
                Price = 110.0m
            },

            };

            foreach (var book in books)
            {
                context.Book.Add(book);
            }

            context.SaveChanges();
        
    }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Book != null ? 
                          View(await _context.Book.ToListAsync()) :
                          Problem("Entity set 'BookLambProjectContext.Book'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,ImagePath,Author,Description,Publisher,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,ImagePath,Author,Description,Publisher,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'BookLambProjectContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
