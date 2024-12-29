using Lab13.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            QuizModel quiz = QuizModel.instance;

            quiz.reset();

            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            QuizModel quiz = QuizModel.instance;

            Expression expression = quiz.addExpression();

            return View(expression);
        }

        [HttpPost]
        public IActionResult Quiz(int id, int userAnswer, string action)
        {
            QuizModel quiz = QuizModel.instance;

            if (ModelState.IsValid)
            {
                var answer = (Request.Form["userAnswer"] != "") ? Int32.Parse(Request.Form["userAnswer"]) : 0;

                quiz.checkAnswer(id, answer);

                if (action == "Next")
                {
                    Expression expression = quiz.addExpression();

                    return View(expression);
                }

                return View("QuizResult", quiz);

            }
            else
            {
                Expression expression = quiz.findExpression(id);

                if (expression != null)
                {
                    ViewData["data"] = "Incorrect data";

                    return View(expression);
                }
                else
                {
                    return Error();
                }
            }
        }
        public IActionResult QuizResult()
        {
            QuizModel quiz = QuizModel.instance;

            return View("QuizResult", quiz);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
