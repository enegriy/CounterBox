
using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterBox
{
    enum Color
    {
        red,
        blu,
        gre
    }

    class Program
    {
        private static Random rand = new Random();

        static void Main()
        {
            int columns = 5;
            int rows = 5;

            int areaId = 0;

            Color[,] grid = new Color[columns, rows];
            int[,] areas = new int[columns, rows];

            FillGrid(columns, rows, grid);
            ShowGrid(columns, rows, grid);

            var areasColor = new Dictionary<int, Color>();
            var counterAreas = new Dictionary<int, int>();
            var unionAreas = new Dictionary<int, int>();


            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var up = i - 1;
                    var left = j - 1;

                    var currentColor = grid[i, j];

                    bool isExistArea = false;

                    if (up >= 0)
                    {
                        if (currentColor == grid[up, j])
                        {
                            areas[i, j] = areas[up, j];
                            counterAreas[areas[i, j]] += 1;
                            isExistArea = true;
                        }
                    }

                    if (!isExistArea && left >= 0)
                    {
                        if (currentColor == grid[i, left])
                        {
                            areas[i, j] = areas[i, left];
                            counterAreas[areas[i, j]] += 1;
                            isExistArea = true;
                        }
                    }
                    else if (isExistArea && left >= 0)
                    {
                        if (currentColor == grid[i, left] && areas[i, j] != areas[i, left])
                        {
                            unionAreas[areas[i, j]] = areas[i, left];
                        }
                    }

                    if (!isExistArea)
                    {
                        areaId++;
                        areas[i, j] = areaId;

                        areasColor[areaId] = currentColor;
                        counterAreas[areaId] = 1;
                    }
                }
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(areas[i, j].ToString("000") + " ");
                }
                Console.WriteLine("");
            }

            foreach (var key in unionAreas.Keys)
            {
                counterAreas[key] += counterAreas[unionAreas[key]];
            }

            foreach (var key in unionAreas.Keys)
            {
                counterAreas.Remove(unionAreas[key]);
            }

            var max = counterAreas.FirstOrDefault(x => x.Value == counterAreas.Max(y => y.Value));
            Console.WriteLine(areasColor[max.Key] + " = " + max.Value + "(" + max.Key + ")");

        }

        static Color RandColor()
        {
            return (Color)rand.Next(0, 3);
        }

        static void FillGrid(int columns, int rows, Color[,] grid)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    grid[i, j] = RandColor();
                }
            }
        }

        static void ShowGrid(int columns, int rows, Color[,] grid)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }
    }
}
