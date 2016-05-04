using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using KKGGames_Labb2.Controllers;

namespace KKGGames_Labb2.Models
{
    public class Game21Model
    {
        public int CurrentValue { get; set; }
        public int ChoosenNumber { get; set; }
        public int Counter { get; set; } = 0;
        public string TurnText { get; set; }

        public void UserStarter()
        {
            Random counter = new Random();
            int Counter = counter.Next(0, 2);
        }

        public void ComputerAI()
        {
            Random random = new Random();
            int randomNumber = random.Next(1,2);
            CurrentValue += randomNumber;
            if (CurrentValue > 14)
            {
                int nextOne = (CurrentValue + 1) % 3 == 0 ? 1 : 2;
            }
            Counter++;
        }
        public void TakeTurn()
        {
            if (Counter % 2 == 0)
            {
                TurnText = "Players turn";
            }
            else
            {
                TurnText = "Computers turn";
                ComputerAI();
                }
        }

        public Game21Model()
        {
            Initiate();
            TakeTurn();
        }

        public GameState GetGameStatus()
        {
            Counter++;
            TakeTurn();
            CurrentValue += ChoosenNumber;
            if (CurrentValue == 21)
                return GameState.Win;
            if (CurrentValue > 21)
                return GameState.Lose;
            return GameState.Playing;

        }

        private void Initiate()
        {
            CurrentValue = 0;
        }
    }

    public enum GameState
    {
        Playing, Win, Lose
    }
}