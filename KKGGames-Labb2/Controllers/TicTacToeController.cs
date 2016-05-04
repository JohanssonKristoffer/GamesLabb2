using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KKGGames_Labb2.Models;

namespace KKGGames_Labb2.Controllers
{
    public class TicTacToeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Game
        public ActionResult Game(int xmax, int ymax, int winstreak)
        {
            if (winstreak > xmax || winstreak > ymax)
                return View("Index");
            Session["CellBoard"] = null;
            Session["Xmax"] = xmax;
            Session["Ymax"] = ymax;
            Session["Winstreak"] = winstreak;
            var model = new TicTacToeModel(xmax, ymax, winstreak);

            if(model.IsComputerTurn())
            {
                model.ComputerTurn();
                Session["CellBoard"] = model.CellBoard;
            }
            return View(model);
        }

        // POST: Game
        [HttpPost]
        public ActionResult Game(string chosenCell)
        {
            int xmax = 3, ymax = 3, winstreak = 3;
            if (Session["Xmax"] != null)
                xmax = (int)Session["Xmax"];
            if (Session["Ymax"] != null)
                ymax = (int)Session["Ymax"];
            if (Session["Winstreak"] != null)
                winstreak = (int)Session["Winstreak"];
            var model = new TicTacToeModel(xmax, ymax, winstreak);
            
            if (Session["CellBoard"] != null)
                model.CellBoard = (Cell[][])Session["CellBoard"];

            model.ParseChosenCell(chosenCell);
            if (model.TryPlaceChosenCell())
            {
                if (model.IsGameComplete)
                {
                    Session["CellBoard"] = null;
                    if(model.IsTie)
                        return View("Tie");
                    return View("Win");
                }

                model.ComputerTurn();
                if (model.IsGameComplete)
                {
                    Session["CellBoard"] = null;
                    if (model.IsTie)
                        return View("Tie");
                    return View("Lose");
                }
            }
            Session["CellBoard"] = model.CellBoard;
            return View(model);
        }
    }
}