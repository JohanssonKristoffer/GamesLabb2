using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

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

        public bool IsDone()
        {
            CurrentValue += ChoosenNumber;
            if (CurrentValue >= 21)
                return true;
            return false;
        }

        private void Initiate()
        {
            CurrentValue = 0;
        }
    }
}