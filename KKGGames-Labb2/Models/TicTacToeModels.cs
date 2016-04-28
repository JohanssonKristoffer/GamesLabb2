using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace KKGGames_Labb2.Models
{
    public class TicTacToeModel
    {
        const int XMAX = 3, YMAX = XMAX;
        const int WINSTREAK = 3;

        public Cell[][] CellBoard { get; set; }
        public Coordinate ChosenCell { get; set; }
        public bool IsGameComplete { get; set; }

        public TicTacToeModel()
        {
            CellBoard = new Cell[XMAX][];
            for (int i = 0; i < XMAX; i++)
                CellBoard[i] = new Cell[YMAX];
            IsGameComplete = false;
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
                if (IsWin(ChosenCell, Cell.Player))
                    IsGameComplete = true;
                return true;
            }
            return false;
        }

        private bool IsWin(Coordinate coordinate, Cell actor)
        {
            if (CheckDirection(coordinate, -1, 0, actor))
                return true;
            if (CheckDirection(coordinate, 0, -1, actor))
                return true;
            if (CheckDirection(coordinate, 1, -1, actor))
                return true;
            if (CheckDirection(coordinate, -1, -1, actor))
                return true;
            return false;
        }

        public bool CheckDirection(Coordinate coordinate, int xd, int yd, Cell actor)
        {
            int counter = WINSTREAK - 1;
            counter = CheckCell(coordinate, xd, yd, actor, counter);
            if (counter == 0)
                return true;
            counter = CheckCell(coordinate, xd*(-1), yd*(-1), actor, counter);
            if (counter == 0)
                return true;
            return false;
        }

        public int CheckCell(Coordinate coordinate, int xd, int yd, Cell actor, int counter)
        {
            Coordinate currentCoordinate = new Coordinate { X = coordinate.X + xd, Y = coordinate.Y + yd };
            bool InBoundsCondition = currentCoordinate.X >= 0 && currentCoordinate.X < XMAX && currentCoordinate.Y >= 0 && currentCoordinate.Y < YMAX;
            if (InBoundsCondition && CellBoard[currentCoordinate.X][currentCoordinate.Y] == actor)
            {
                counter--;
                if (counter != 0)
                    counter = CheckCell(currentCoordinate, xd, yd, actor, counter);
                return counter;
            }
            return counter;
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