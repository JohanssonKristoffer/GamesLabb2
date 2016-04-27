using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI.HtmlControls;

namespace KKGGames_Labb2.Models
{
    public class Game21Model
    {
        public int CurrentValue { get; set; }
        public int ChoosenNumber { get; set; }
        public bool IsDone { get; set; }

        public Game21Model()
        {
            Initiate();
        }

        public string GetResult()
        {
            string result = null;
            CurrentValue += ChoosenNumber;
            if (CurrentValue >= 21)
            {
                result = "You Win!";
                IsDone = true;
            }
            if (CurrentValue > 21)
                result = "You Lose!";
            return result;
        }

        private void Initiate()
        {
            CurrentValue = 0;
        }
    }
}