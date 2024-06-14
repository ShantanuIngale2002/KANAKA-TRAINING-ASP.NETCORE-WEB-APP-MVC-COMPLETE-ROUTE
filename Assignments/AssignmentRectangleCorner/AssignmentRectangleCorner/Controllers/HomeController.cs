using AssignmentRectangleCorner.Model;
using AssignmentRectangleCorner.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentRectangleCorner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestCaseRepository testcaseRepo;
        public HomeController(ITestCaseRepository repo)
        {
            this.testcaseRepo = repo;
        }


        public IActionResult QuestionPage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult QuestionPage(TestCaseModel model)
        {
            float result = testcaseRepo.PerformTheOperation(model);
            model.Answer = result;
            return View(model);
        }


        public IActionResult GetAllRecords()
        {
            IEnumerable<TestCaseModel> data = testcaseRepo.GetAllTestCases();
            return View(data);
        }


    }
}
