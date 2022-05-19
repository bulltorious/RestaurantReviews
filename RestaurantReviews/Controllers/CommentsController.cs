#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Models;
using RestaurantReviews.Saphyre;

namespace RestaurantReviews.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Review> _reviewRepository;
        private GenericRepository<Comment> _commentRepository;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
            _reviewRepository = new GenericRepository<Review>(_context);
            _commentRepository = new GenericRepository<Comment>(_context);
        }

        // GET: Comments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(_commentRepository.GetAll());
        }

        // GET: Comments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ReviewId"] = new SelectList(_context.Review, "Id", "Name");

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Text,CreatedDate,ReviewId")] Comment comment)
        {
            var targetReview = _reviewRepository.GetById(comment.ReviewId);

            if (targetReview != null)
            {

                _commentRepository.Insert(comment);
                _commentRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ReviewId"] = new SelectList(_context.Review, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromReview(string name, string email, string commentText, int reviewId)
        {
            if (!(String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(commentText)))
            {

                _commentRepository.Insert(new Comment { Name = name, Email = email, Text = commentText, ReviewId = reviewId, CreatedDate = DateTime.Now });
                _commentRepository.Save();
            }


            return LocalRedirect($"~/Reviews/Details/{reviewId}");

        }

        // GET: Comments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ReviewId"] = new SelectList(_context.Review, "Id", "Id", comment.ReviewId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Text,Title,CreatedDate,ReviewId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

                try
                {
                _commentRepository.Update(comment);
                _commentRepository.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            ViewData["ReviewId"] = new SelectList(_context.Review, "Id", "Id", comment.ReviewId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Review)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            _commentRepository.Delete(id);
            _commentRepository.Save();
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
