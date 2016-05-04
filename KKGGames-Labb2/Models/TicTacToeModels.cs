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
        const int XMAX = 3;
        const int YMAX = 3;
        const int WINSTREAK = 3;

        public Cell[][] CellBoard { get; set; }
        public int[][] PointBoard { get; set; }
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
        private bool CheckDirection(Coordinate coordinate, int xd, int yd, Cell actor)
        {
            int counter = WINSTREAK - 1;
            counter = CheckCell(coordinate, xd, yd, actor, counter);
            if (counter == 0)
                return true;
            counter = CheckCell(coordinate, xd * (-1), yd * (-1), actor, counter);
            if (counter == 0)
                return true;
            return false;
        }
        private int CheckCell(Coordinate coordinate, int xd, int yd, Cell actor, int counter)
        {
            Coordinate currentCoordinate = new Coordinate { X = coordinate.X + xd, Y = coordinate.Y + yd };
            bool isInBounds = currentCoordinate.X >= 0 && currentCoordinate.X < XMAX && currentCoordinate.Y >= 0 &&
                              currentCoordinate.Y < YMAX;
            if (isInBounds && CellBoard[currentCoordinate.X][currentCoordinate.Y] == actor)
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
            InitiatePointBoard();
            GeneratePointBoard();
            PlaceRandomHighestPointCell();
        }

        private void PlaceRandomHighestPointCell()
        {
            List<Coordinate> HighestPoints = new List<Coordinate>();
            int maxPoint = 0;
            for (int x = 0; x < XMAX; x++)
            {
                for (int y = 0; y < YMAX; y++)
                {
                    if (PointBoard[x][y] > maxPoint)
                    {
                        maxPoint = PointBoard[x][y];
                        HighestPoints.Clear();
                    }
                    if (PointBoard[x][y] >= maxPoint)
                        HighestPoints.Add(new Coordinate { X = x, Y = y });
                }
            }
            Random random = new Random();
            int randomHighest = random.Next(0, HighestPoints.Count);
            Coordinate ComputerChosenCell = HighestPoints[randomHighest];
            CellBoard[ComputerChosenCell.X][ComputerChosenCell.Y] = Cell.Computer;
            if (IsWin(ComputerChosenCell, Cell.Computer))
                IsGameComplete = true;
        }
        private void InitiatePointBoard()
        {
            PointBoard = new int[XMAX][];
            for (int i = 0; i < XMAX; i++)
                PointBoard[i] = new int[YMAX];
        }
        private void GeneratePointBoard()
        {
            for (int x = 0; x < XMAX; x++)
            {
                for (int y = 0; y < YMAX; y++)
                {
                    Coordinate coordinate = new Coordinate { X = x, Y = y };
                    if (CellBoard[coordinate.X][coordinate.Y] != Cell.Empty)
                    {
                        PointBoard[coordinate.X][coordinate.Y] = 0;
                        AssignPoints(coordinate, -1, 0);
                        AssignPoints(coordinate, 1, 0);
                        AssignPoints(coordinate, 0, -1);
                        AssignPoints(coordinate, 0, 1);
                        AssignPoints(coordinate, -1, -1);
                        AssignPoints(coordinate, 1, 1);
                        AssignPoints(coordinate, -1, 1);
                        AssignPoints(coordinate, 1, -1);
                    }
                }
            }
        }

        private void AssignPoints(Coordinate coordinate, int xd, int yd)
        {
            Coordinate currentCoordinate = new Coordinate { X = coordinate.X + xd, Y = coordinate.Y + yd };
            bool isInBounds = currentCoordinate.X >= 0 && currentCoordinate.X < XMAX && currentCoordinate.Y >= 0 &&
                             currentCoordinate.Y < YMAX;
            if (isInBounds && CellBoard[currentCoordinate.X][currentCoordinate.Y] == Cell.Empty)
                PointBoard[currentCoordinate.X][currentCoordinate.Y] = 1;
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