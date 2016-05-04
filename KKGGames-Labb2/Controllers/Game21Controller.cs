using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KKGGames_Labb2.Models;

namespace KKGGames_Labb2.Controllers
{
    public class Game21Controller : Controller
    {
        // GET: Game21
        public ActionResult Index()
        {
            Game21Model model = new Game21Model();
            if (model.IsComputerTurn())
                model.ComputerAi();
            return View(model);
        }

        public ActionResult Game21()
        {
            return View();
        }

        // POST: Game21
        [HttpPost]
        public ActionResult Index(Game21Model model)
        {
            ModelState.Remove("CurrentValue");
            ModelState.Remove("Counter");
            switch (model.PlayerTurn())
            {
                case GameState.Win:
                    return View("Win");
                case GameState.Lose:
                    return View("Lose");
                default:
                    switch (model.ComputerAi())
                    {
                        case GameState.Win:
                            return View("Win");
                        case GameState.Lose:
                            return View("Lose");
                        default:
                            return View(model);
                    }
            }
        }
    }
}