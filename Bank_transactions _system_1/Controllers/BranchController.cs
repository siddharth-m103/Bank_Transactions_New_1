using Bank_transactions__system_1.Data;
using Bank_transactions__system_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bank_transactions__system_1.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string search)
        {
            var bankList = _context.BankBranches.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                bankList = bankList.Where(b => b.BranchName.Contains(search));
            }

            return View(bankList.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankBranch branch)
        {
            if (ModelState.IsValid)
            {


                _context.BankBranches.Add(branch);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Branch created successfully.";
                return RedirectToAction("Index");
            }
            return View(branch);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var branch = _context.BankBranches.FirstOrDefault(b => b.Id == id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BankBranch branch)
        {
            if (ModelState.IsValid)
            {
                _context.BankBranches.Update(branch);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Branch updated successfully.";
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        [HttpGet]
        public IActionResult GetBranchLimits(int id)
        {
            var branch = _context.BankBranches.FirstOrDefault(b => b.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return Json(new
            {
                creditLimit = branch.CreditLimit,
                debitLimit = branch.DebitLimit
            });
        }

    }
}
