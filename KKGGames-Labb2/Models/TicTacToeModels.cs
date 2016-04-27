using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKGGames_Labb2.Models
{
    public class TicTacToeModel
    {
        const int XMAX = 3, YMAX = XMAX;

        public Cell[][] CellBoard { get; set; }
        public Coordinate ChosenCell { get; set; }

        public TicTacToeModel()
        {
            CellBoard = new Cell[XMAX][];
            for (int i = 0; i < XMAX; i++)
                CellBoard[i] = new Cell[YMAX];
        }

        public void ParseChosenCell(string chosenCell)
        {
            ChosenCell = new Coordinate
            {
                X = int.Parse(chosenCell.Split(',')[0]),
                Y = int.Parse(chosenCell.Split(',')[1])
            };
        }

        public bool TryPlaceChosenCell()
        {
            if (CellBoard[ChosenCell.X][ChosenCell.Y] == Cell.Empty)
            {
                CellBoard[ChosenCell.X][ChosenCell.Y] = Cell.Player;
                return true;
            }
            return false;
        }

        public void ComputerTurn()
        {
            //TBI
        }
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum Cell
    {
        Empty,
        Player,
        Computer
    }
}