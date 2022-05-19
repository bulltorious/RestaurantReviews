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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Review> _reviewRepository;
        private GenericRepository<Comment> _commentRepository;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
            _reviewRepository = new GenericRepository<Review>(_context);
            _commentRepository = new GenericRepository<Comment>(_context);

        }

        // GET: Reviews
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(_reviewRepository.GetAll());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _reviewRepository.GetById(id);

            if (review == null)
            {
                return NotFound();
            }

            var comments = _commentRepository.GetAll().Where(m => m.ReviewId == id).OrderByDescending(m => m.CreatedDate).ToList();

            review.Comments = comments;

            return View(review);
        }

        // GET: Reviews/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Rating,Text,Bullet,Address,CreatedDate")] Review review)
        {

            _reviewRepository.Insert(review);
            _reviewRepository.Save();

            return RedirectToAction(nameof(Index));


        }

        // GET: Reviews/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _reviewRepository.GetById(id);


            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Rating,Text,Bullet,Address,CreatedDate")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }


            try
            {


                _reviewRepository.Update(review);
                _reviewRepository.Save();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.Id))
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

        // GET: Reviews/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _reviewRepository.GetById(id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            _reviewRepository.Delete(id);
            _reviewRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
