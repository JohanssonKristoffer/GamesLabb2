using System;
using System.Collections.Generic;

namespace KKGGames_Labb2.Models
{
    public class TicTacToeModel
    {
        public Cell[][] CellBoard { get; set; }
        public bool IsGameComplete { get; private set; }
        public bool IsTie { get; private set; }

        public int Xmax { get; set; } = 3;
        public int Ymax { get; set; } = 3;
        private int Winstreak { get; set; } = 3;

        private int[][] PointBoard { get; set; }
        private List<Coordinate[]> PossiblePlacementList { get; set; }
        private Coordinate ChosenCell { get; set; }

        public TicTacToeModel()
        {
            CellBoard = new Cell[Xmax][];
            for (int i = 0; i < Xmax; i++)
                CellBoard[i] = new Cell[Ymax];
        }
        public TicTacToeModel(int xmax, int ymax, int winstreak)
        {
            Xmax = xmax;
            Ymax = ymax;
            Winstreak = winstreak;
            CellBoard = new Cell[Xmax][];
            for (int i = 0; i < Xmax; i++)
                CellBoard[i] = new Cell[Ymax];
        }

        public bool IsComputerTurn()
        {
            Random random = new Random();
            int randomnumber = random.Next(0, 2);
            if (randomnumber == 0)
                return true;
            return false;
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
                if (IsWin(ChosenCell, Cell.Player) || CheckTie())
                    IsGameComplete = true;
                return true;
            }
            return false;
        }

        private bool CheckTie()
        {
            for (int x = 0; x < Xmax; x++)
            {
                for (int y = 0; y < Ymax; y++)
                {
                    if (CellBoard[x][y] == Cell.Empty)
                        return false;
                }
            }
            IsTie = true;
            return true;
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
            int counter = Winstreak - 1;
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
            bool isInBounds = currentCoordinate.X >= 0 && currentCoordinate.X < Xmax && currentCoordinate.Y >= 0 &&
                              currentCoordinate.Y < Ymax;
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
            CreatePossiblePlacementList();
            FilterPossiblePlacementList();
            InitiatePointBoard();
            GeneratePointBoard();
            PlaceRandomHighestPointCell();
        }
        private void CreatePossiblePlacementList()
        {
            PossiblePlacementList = new List<Coordinate[]>();
            CreateHorizonalPlacements();
            CreateVerticalPlacements();
            CreateDiagonal1Placements();
            CreateDiagonal2Placements();
        }
        private void CreateHorizonalPlacements()
        {
            for (int x = 0; x < Xmax - Winstreak + 1; x++)
            {
                for (int y = 0; y < Ymax; y++)
                {
                    Coordinate[] horizontalCombo = new Coordinate[Winstreak];
                    for (int xd = 0; xd < Winstreak; xd++)
                        horizontalCombo[xd] = new Coordinate { X = x + xd, Y = y };
                    PossiblePlacementList.Add(horizontalCombo);
                }
            }
        }
        private void CreateVerticalPlacements()
        {
            for (int x = 0; x < Xmax; x++)
            {
                for (int y = 0; y < Ymax - Winstreak + 1; y++)
                {
                    Coordinate[] verticalCombo = new Coordinate[Winstreak];
                    for (int yd = 0; yd < Winstreak; yd++)
                        verticalCombo[yd] = new Coordinate { X = x, Y = y + yd };
                    PossiblePlacementList.Add(verticalCombo);
                }
            }
        }
        private void CreateDiagonal1Placements()
        {
            for (int x = 0; x < Xmax - Winstreak + 1; x++)
            {
                for (int y = 0; y < Ymax - Winstreak + 1; y++)
                {
                    Coordinate[] diagonal1Combo = new Coordinate[Winstreak];
                    for (int d = 0; d < Winstreak; d++)
                        diagonal1Combo[d] = new Coordinate { X = x + d, Y = y + d };
                    PossiblePlacementList.Add(diagonal1Combo);
                }
            }
        }
        private void CreateDiagonal2Placements()
        {
            for (int x = Winstreak - 1; x < Xmax; x++)
            {
                for (int y = 0; y < Ymax - Winstreak + 1; y++)
                {
                    Coordinate[] diagonal2Combo = new Coordinate[Winstreak];
                    for (int d = 0; d < Winstreak; d++)
                        diagonal2Combo[d] = new Coordinate { X = x - d, Y = y + d };
                    PossiblePlacementList.Add(diagonal2Combo);
                }
            }
        }

        private void FilterPossiblePlacementList()
        {
            List<Coordinate[]> tempList = new List<Coordinate[]>();
            bool currentIsDefensive = false;
            int currentWeight = 0;
            foreach (var winCombo in PossiblePlacementList)
            {
                int weightComputer = 0;
                int weightPlayer = 0;
                foreach (var coordinate in winCombo)
                {
                    if (CellBoard[coordinate.X][coordinate.Y] == Cell.Computer)
                        weightComputer++;
                    if (CellBoard[coordinate.X][coordinate.Y] == Cell.Player)
                        weightPlayer++;
                }
                if (weightComputer == 0 || weightPlayer == 0)
                {
                    bool isDefensive = weightPlayer > weightComputer;
                    int weight = weightComputer + weightPlayer;

                    if (weight > currentWeight || (weight == currentWeight && !isDefensive && currentIsDefensive))
                    {
                        tempList.Clear();
                        currentWeight = weight;
                        currentIsDefensive = isDefensive;
                        tempList.Add(winCombo);
                    }
                    else if (weight == currentWeight && isDefensive == currentIsDefensive)
                        tempList.Add(winCombo);
                }
                PossiblePlacementList = tempList;
            }

        }

        private void InitiatePointBoard()
        {
            PointBoard = new int[Xmax][];
            for (int i = 0; i < Xmax; i++)
                PointBoard[i] = new int[Ymax];
        }
        private void GeneratePointBoard()
        {
            int count = 0;
            foreach (var winCombo in PossiblePlacementList)
            {
                count++;
                foreach (var coordinate in winCombo)
                {
                    if (CellBoard[coordinate.X][coordinate.Y] == Cell.Empty)
                        PointBoard[coordinate.X][coordinate.Y] += 1;
                }
            }
            if (count == 0)
                for (int x = 0; x < Xmax; x++)
                {
                    for (int y = 0; y < Ymax; y++)
                    {
                        if (CellBoard[x][y] == Cell.Empty)
                        {
                            PointBoard[x][y] += 1;
                            return;
                        }
                    }
                }
        }
        private void PlaceRandomHighestPointCell()
        {
            List<Coordinate> HighestPoints = new List<Coordinate>();
            int maxPoint = 0;
            for (int x = 0; x < Xmax; x++)
            {
                for (int y = 0; y < Ymax; y++)
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
            if (IsWin(ComputerChosenCell, Cell.Computer) || CheckTie())
                IsGameComplete = true;
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