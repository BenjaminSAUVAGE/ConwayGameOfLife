// <copyright file="Cell.cs" company="BS">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConwayGameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The cell of the game of life.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        public Cell()
        {
            this.Gene = new Genes(Color.Red, true);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cell is alive or not.
        /// </summary>
        public bool Alive { get; set; }

        /// <summary>
        /// Gets or Sets the array who contains all cell.
        /// </summary>
        public Cell[,] Cells { get; set; }

        public Genes Gene { get; set; }

        private Random random = new Random();

        /// <summary>
        /// The next generation of cells.
        /// </summary>
        /// <param name="cells">The array who contain the cell.</param>
        public void NextGen()
        {
            // Copy the 2D array without reference.
            Cell[,] inter = new Cell[this.Cells.GetUpperBound(0) + 1, this.Cells.GetUpperBound(1) + 1];
            for (int x = 0; x <= this.Cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= this.Cells.GetUpperBound(1); y++)
                {
                    inter[x, y] = new Cell
                    {
                        Alive = this.Cells[x, y].Alive,
                    };
                }
            }

            for (int x = 1; x < this.Cells.GetUpperBound(0); x++)
            {
                for (int y = 1; y < this.Cells.GetUpperBound(1); y++)
                {
                    this.Cells[x, y].Alive = this.BeAlive(x, y, inter);
                    Genes.GiveGene(this.Cells[x, y], inter, x, y);
                    if (this.random.Next(101) < 5 && this.Cells[x, y].Alive)
                    {
                        Genes.Mutation(this.Cells[x, y]);
                    }
                }
            }
        }

        /// <summary>
        /// Random the cell to beAlive.
        /// </summary>
        public void Random()
        {
            for (int x = 0; x <= this.Cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= this.Cells.GetUpperBound(1); y++)
                {
                    if ((x != 0 && x != this.Cells.GetUpperBound(0))
                        && y != 0 && y != this.Cells.GetUpperBound(1))
                    {
                        this.Cells[x, y].Alive = this.random.Next(101) > 50;
                    }
                }
            }
        }

        /// <summary>
        /// Draw a rectangle if the cell is alive.
        /// </summary>
        /// <param name="g">The graphics.</param>
        public void DrawAll(Graphics g)
        {
            Brush brush;
            for (int x = 1; x < this.Cells.GetUpperBound(0); x++)
            {
                for (int y = 1; y < this.Cells.GetUpperBound(1); y++)
                {
                    if (this.Cells[x, y].Alive)
                    {
                        brush = new SolidBrush(this.Cells[x, y].Gene.Color);
                    }
                    else
                    {
                        brush = Brushes.White;
                    }

                    g.FillRectangle(brush, x * Program.SQUARESIZE, y * Program.SQUARESIZE, Program.SQUARESIZE, Program.SQUARESIZE);
                }
            }
        }

        /// <summary>
        /// Setup cell.
        /// </summary>
        /// <param name="width">The width of the 2D array.</param>
        /// <param name="height">The "heigth" of the 2D array.</param>
        public void Setup(int width, int height)
        {
            this.Cells = new Cell[width, height];
            for (int x = 0; x <= this.Cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= this.Cells.GetUpperBound(1); y++)
                {
                    this.Cells[x, y] = new Cell();
                }
            }
        }

        /// <summary>
        /// Test if the cell in parameter can be alive or not.
        /// </summary>
        /// <param name="x">The position x of the cell in the 2D array.</param>
        /// <param name="y">The position y of the cell in the 2D array.</param>
        /// <returns>True if the cell can be alive, else false.</returns>
        private bool BeAlive(int x, int y, Cell[,] cells)
        {
            int cellsAlive = 0;

            // Test all the cells around the position x,y.
            for (int width = -1; width <= 1; width++)
            {
                for (int height = -1; height <= 1; height++)
                {
                    cellsAlive += cells[x + width, y + height].Alive ? 1 : 0;
                }
            }

            // Remove the cell itself if she is alive.
            cellsAlive -= cells[x, y].Alive ? 1 : 0;

            if (cells[x, y].Alive && (cellsAlive > 3 || cellsAlive < 2))
            {
                return false;
            }

            if (!cells[x, y].Alive && cellsAlive == 3)
            {
                return true;
            }

            return cells[x, y].Alive;
        }
    }
}
