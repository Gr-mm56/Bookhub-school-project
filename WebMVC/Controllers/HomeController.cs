using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
        private readonly SignInManager<LocalIdentityUser> _signInManager;

        public HomeController(
            ILogger<HomeController> logger,
            ISearchFacade searchFacade,
            IAuthorService authorService,
            IBookService bookService,
            SignInManager<LocalIdentityUser> signInManager)
        {
            _logger = logger;
            _searchFacade = searchFacade;
            _authorService = authorService;
            _bookService = bookService;
            _signInManager = signInManager;
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
                var publisherCards = BookMapper.ToPublisherCardViewModels(searchResultDto.Publishers);

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