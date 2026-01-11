using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.Rating.Requests;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Mappers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchFacade _searchFacade;
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly IRatingService _ratingService;
        private readonly SignInManager<LocalIdentityUser> _signInManager;
        private readonly UserManager<LocalIdentityUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            ISearchFacade searchFacade,
            IAuthorService authorService,
            IBookService bookService,
            IPublisherService publisherService,
            IRatingService ratingService,
            SignInManager<LocalIdentityUser> signInManager,
            UserManager<LocalIdentityUser> userManager
        )
        {
            _logger = logger;
            _searchFacade = searchFacade;
            _authorService = authorService;
            _bookService = bookService;
            _publisherService = publisherService;
            _ratingService = ratingService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(
            string? searchTerm = null,
            int bookPageNumber = 1,
            int authorPageNumber = 1,
            int publisherPageNumber = 1)
        {
            try
            {
                var searchResultDto = await _searchFacade.SearchAsync(
                    searchTerm,
                    bookPageNumber,
                    authorPageNumber,
                    publisherPageNumber);

                var bookCards = BookMapper.ToBookCardViewModels(searchResultDto.Books);
                var authorCards = AuthorMapper.ToAuthorCardViewModels(searchResultDto.Authors);
                var publisherCards = PublisherMapper.ToPublisherCardViewModels(searchResultDto.Publishers);

                var viewModel = new HomePageViewModel
                {
                    Books = bookCards,
                    Authors = authorCards,
                    Publishers = publisherCards,
                    SearchQuery = searchTerm,
                    BookPagination = searchResultDto.BookPagination,
                    AuthorPagination = searchResultDto.AuthorPagination,
                    PublisherPagination = searchResultDto.PublisherPagination,
                    IsSignedIn = _signInManager.IsSignedIn(User)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");
                return View(new HomePageViewModel
                {
                    SearchQuery = searchTerm,
                    IsSignedIn = _signInManager.IsSignedIn(User)
                });
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                var viewModel = BookMapper.ToBookDetailViewModel(book);
                viewModel.IsSignedIn = _signInManager.IsSignedIn(User);

                // Get current user ID if signed in
                if (viewModel.IsSignedIn)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        viewModel.CurrentUserId = user.UserId;
                    }
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book detail for id {BookId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> AuthorDetail(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var viewModel = AuthorMapper.ToAuthorDetailViewModel(author);
            return View(viewModel);
        }

        public async Task<IActionResult> PublisherDetail(int id)
        {
            var publisher = await _publisherService.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var viewModel = PublisherMapper.ToPublisherDetailViewModel(publisher);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRating(int bookId, int stars)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Unauthorized();
            }

            if (stars < 1 || stars > 5)
            {
                return BadRequest("Rating must be between 1 and 5 stars");
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                var ratingRequest = new RatingRequestDto
                {
                    Stars = stars,
                    UserId = user.UserId,
                    BookId = bookId
                };

                await _ratingService.CreateAsync(ratingRequest);

                return RedirectToAction("Detail", new { id = bookId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating rating for book {BookId}", bookId);
                return RedirectToAction("Detail", new { id = bookId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRating(int ratingId, int bookId, int stars)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Unauthorized();
            }

            if (stars < 1 || stars > 5)
            {
                return BadRequest("Rating must be between 1 and 5 stars");
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                var ratingRequest = new RatingRequestDto
                {
                    Stars = stars,
                    UserId = user.UserId,
                    BookId = bookId
                };

                await _ratingService.UpdateAsync(ratingId, ratingRequest);

                return RedirectToAction("Detail", new { id = bookId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating rating {RatingId} for book {BookId}", ratingId, bookId);
                return RedirectToAction("Detail", new { id = bookId });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}