using Bank_transactions__system_1.Data;
using Bank_transactions__system_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bank_transactions__system_1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var transactions = _context.BankTransactions
    .Include(t => t.Branch)
    .Include(t => t.TransactionType) 
    .ToList();

            //var transactions = _context.BankTransactions
            //    .Include(t => t.Branch)
            //    .ToList();

            //// // Apply + for Credit, - for Debit
            //var totalAmount = transactions
            //  //  .Select(t => t.TransactionType == "Credit" ? t.Amount : -t.Amount)
            //  //.Select(t => t.TransactionType.Name == "Credit" ? t.Amount : -t.Amount)
            //  .Select(t => t.TransactionType != null && t.TransactionType.Name == "Credit" ? t.Amount : -t.Amount)

            //    .Sum();


            var totalAmount = transactions
    .Where(t => t.TransactionType != null)
    .Select(t => t.TransactionType.Name == "Credit" ? t.Amount : -t.Amount)
    .DefaultIfEmpty(0)
    .Sum();

            var viewModel = new TransactionFilterViewModel
            {
                Transactions = transactions,
               TotalAmount = totalAmount
            };

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Branches = new SelectList(_context.BankBranches, "Id", "BranchName");
            ViewBag.TransactionTypes = new SelectList(_context.TransactionTypes, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BankTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.TransactionDate = DateTime.Now;
                _context.BankTransactions.Add(transaction);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Transaction recorded.";
                return RedirectToAction("Create");
            }

            ViewBag.Branches = new SelectList(_context.BankBranches, "Id", "BranchName");

            ViewBag.TransactionTypes = new SelectList(_context.TransactionTypes, "Id", "Name");


            return View(transaction);
        }
        [HttpGet]
        public IActionResult BranchWise()
        {
            var model = new TransactionFilterViewModel
            {
                Branches = _context.BankBranches.ToList(),
                FromDate = DateTime.Now.AddDays(-7),
                ToDate = DateTime.Now
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult BranchWise(TransactionFilterViewModel filter)
        {
            var query = _context.BankTransactions
                .Include(t => t.Branch)
                .Where(t => (!filter.BranchId.HasValue || t.BankBranchId == filter.BranchId)
                         && (!filter.FromDate.HasValue || t.TransactionDate >= filter.FromDate)
                         && (!filter.ToDate.HasValue || t.TransactionDate <= filter.ToDate)
                       //  && (string.IsNullOrEmpty(filter.TransactionType) || t.TransactionType == filter.TransactionType));
                       && (string.IsNullOrEmpty(filter.TransactionType) || t.TransactionType.Name == filter.TransactionType));


            filter.Transactions = query.ToList();
            filter.TotalAmount = filter.Transactions.Sum(t => t.Amount);
            filter.Branches = _context.BankBranches.ToList();

            return View(filter);
        }
    }
}

