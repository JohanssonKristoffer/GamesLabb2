using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace KKGGames_Labb2.Models
{
    public class Game21Model
    {
        public int CurrentValue { get; set; }
        public int ChoosenNumber { get; set; }

        public Game21Model()
        {
            Initiate();
        }

        public GameState GetGameStatus()
        {
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