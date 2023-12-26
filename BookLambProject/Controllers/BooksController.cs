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
            { //Book seeding
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
            new Book
            {
                Title = "1984",
                ReleaseDate = DateTime.Parse("2018-01-04"),
                Genre = "Fiction",
                ImagePath = "1984.jpg",
                Author = "George Orwell",
                Description = "One of Britain's most popular novels, George Orwell's Nineteen Eighty-Four is set in a society terrorised by a totalitarian ideology propagated by The Party. \r\n\r\nWinston Smith works for the Ministry of Truth in London, chief city of Airstrip One. Big Brother stares out from every poster, the Thought Police uncover every act of betrayal. When Winston finds love with Julia, he discovers that life does not have to be dull and deadening, and awakens to new possibilities. Despite the police helicopters that hover and circle overhead, Winston and Julia begin to question the Party; they are drawn towards conspiracy. Yet Big Brother will not tolerate dissent - even in the mind. For those with original thoughts they invented Room 101. . . ",
                Publisher = "Penguin Books",
                Price = 270.0m
            },
            new Book
            {
                Title = "Harry Potter and the Philosopher's Stone",
                ReleaseDate = DateTime.Parse("2001-04-04"),
                Genre = "Fiction",
                ImagePath = "Sorcerers_stone.jpg",
                Author = "J. K. Rowling",
                Description = "Celebrate 20 years of Harry Potter magic with four special editions of Harry Potter and the Philosopher's Stone. Gryffindor, Slytherin, Hufflepuff, Ravenclaw ...Twenty years ago these magical words and many more flowed from a young writer's pen, an orphan called Harry Potter was freed from the cupboard under the stairs - and a global phenomenon started. Harry Potter and the Philosopher's Stone has been read and loved by every new generation since. To mark the 20th anniversary of first publication, Bloomsbury is publishing four House Editions of J.K. Rowling's modern classic. These stunning editions will each feature the individual house crest on the jacket and line illustrations exclusive to that house, by Kate Greenaway Medal winner Levi Pinfold. Exciting new extra content will include fact files and profiles of favourite characters, and each book will have sprayed edges in the house colours. Available for a limited period only, these highly collectable editions will be a must-have for all Harry Potter fans in 2017.",
                Publisher = "Bloomsbury",
                Price = 478.0m
            },
            new Book
            {
                Title = "It",
                ReleaseDate = DateTime.Parse("2017-02-07"),
                Genre = "Horror",
                ImagePath = "it.jpg",
                Author = "Stephen King",
                Description = "To the children, the town was their whole world. To the adults, knowing better, Derry Maine was just their home town: familiar, well-ordered for the most part. A good place to live.It was the children who saw - and felt - what made Derry so horribly different. In the storm drains, in the sewers, IT lurked, taking on the shape of every nightmare, each one's deepest dread. Sometimes IT reached up, seizing, tearing, killing . . .",
                Publisher = "Hodder & Stoughton Ltd",
                Price = 335.0m
            },
            new Book
            {
                Title = "White Fang",
                ReleaseDate = DateTime.Parse("2019-08-01"),
                Genre = "Fiction",
                ImagePath = "white_fang.png",
                Author = "Jack London",
                Description = "First serialized in Outing Magazine, worldwide known author Jack Londons second novel White Fang first published in 1906. The story takes place in Russia, America and Canada and it is about White Fangs journey to domestication. On the other hand this novel is a thematic mirror to Jack Londons most famous work, The Call of the Wild which is about a captured, domesticated dog embracing his wild ancestry to survive and thrive in the wild.",
                Publisher = "Ren Kitap",
                Price = 99.0m
            },
            new Book
            {
                Title = "The Alchemist",
                ReleaseDate = DateTime.Parse("2015-05-11"),
                Genre = "Clasic",
                ImagePath = "the_alchemist.jpg",
                Author = "Harper Collins Publishers",
                Description = "Santiago, a young shepherd living in the hills of Andalucia, feels that there is more to life than his humble home and his flock. One day he finds the courage to follow his dreams into distant lands, each step galvanised by the knowledge that he is following the right path: his own. The people he meets along the way, the things he sees and the wisdom he learns are life-changing.",
                Publisher = "Paulo Coelho",
                Price = 120.0m
            },
            new Book
            {
                Title = "Twilight",
                ReleaseDate = DateTime.Parse("2006-12-12"),
                Genre = "Fiction",
                ImagePath = "twilight.jpg",
                Author = "Stephenie Meyer",
                Description = "In this exquisite fantasy, Bella adores beautiful Edward, and he returns her love. But Edward must control the blood lust she arouses in him because--he's a vampire. This deeply romantic and extraordinarily suspenseful novel captures the struggle between defying instincts and satisfying desires.",
                Publisher = "Megan Tingley Books",
                Price = 138.0m
            },
            new Book
            {
                Title = "Hobbit",
                ReleaseDate = DateTime.Parse("2013-7-1"),
                Genre = "Fiction",
                ImagePath = "hobbit.jpg",
                Author = "J. R. R. Tolkien",
                Description = "The first new illustrated edition of The Hobbit for more than 15 years contains 150 brand new colour illustrations. Artist Jemima Catlin's charming and lively interpretation brings Tolkien's beloved characters to life in a way that will entice and entertain a new generation of readers.Bilbo Baggins is a hobbit who enjoys a comfortable and quiet life. His contentment is disturbed one day when the wizard, Gandalf, and the dwarves arrive to take him away on an adventure.Smaug certainly looked fast asleep, when Bilbo peeped once more from the entrance. He was just about to step out on to the floor when he caught a sudden thin ray of red from under the drooping lid of Smaug's left eye. He was only pretending to sleep! He was watching the tunnel entrance!",
                Publisher = "İthaki Yayınları",
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
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null; //ViewBag bool for detecting logged in or not
            return _context.Book != null ? 
                          View(await _context.Book.ToListAsync()) :
                          Problem("Entity set 'BookLambProjectContext.Book'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
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
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,ImagePath,Author,Description,Publisher,Price")] Book book)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
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
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
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
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
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
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
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
