using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using WebMVC.Mappers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly SignInManager<LocalIdentityUser> _signInManager;

        public HomeController(
            ILogger<HomeController> logger, 
            IBookService bookService,
            SignInManager<LocalIdentityUser> signInManager)
        {
            _logger = logger;
            _bookService = bookService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string? searchTerm = null, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize is < 1 or > 100) pageSize = 12;

                var offset = (pageNumber - 1) * pageSize;

                var searchDto = new BookSearchDto
                {
                    Limit = pageSize,
                    Offset = offset,
                    SearchTerm = searchTerm
                };

                var result = await _bookService.SearchBooksAsync(searchDto);

                var bookCards = BookMapper.ToBookCardViewModels(result.Items);

                var viewModel = new HomePageViewModel
                {
                    Books = bookCards,
                    SearchQuery = searchTerm,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = result.Total,
                    TotalPages = (int)Math.Ceiling((double)result.Total / pageSize),
                    IsSignedIn = _signInManager.IsSignedIn(User)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading books for home page");
                return View(new HomePageViewModel 
                { 
                    SearchQuery = searchTerm,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
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
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book detail for id {BookId}", id);
                return NotFound();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}