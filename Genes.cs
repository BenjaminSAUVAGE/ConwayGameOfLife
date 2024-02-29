// <copyright file="genes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConwayGameOfLife
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Do the gestion of genes.
    /// </summary>
    public class Genes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Genes"/> class.
        /// </summary>
        /// <param name="color">The color of the genes.</param>
        /// <param name="recessif">If is recessif or not.</param>
        public Genes(Color color, bool recessif)
        {
            this.Color = color;
            this.Recessif = recessif;
        }

        /// <summary>
        /// Gets or Sets the color of the genes.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gene is recessif or not.
        /// </summary>
        public bool Recessif { get; set; }

        /// <summary>
        /// Give the futur gene of the cell.
        /// </summary>
        /// <param name="cell">The cell who will receive the gene.</param>
        /// <param name="cells">The 2D array.</param>
        /// <param name="x">The coordinate x of the cell.</param>
        /// <param name="y">The coordiante y of the cell.</param>
        public static void GiveGene(Cell cell, Cell[,] cells, int x, int y)
        {
            Genes gene = cell.Gene;
            Dictionary<Color, int> h = new Dictionary<Color, int>();
            h.Add(gene.Color, 0);

            // Search the number of genes in the parents of the cell and take it.
            // Test all the cells around the position x,y.
            for (int width = -1; width <= 1; width++)
            {
                for (int height = -1; height <= 1; height++)
                {
                    Cell cellTest = cells[x + width, y + height];
                    if (cellTest.Alive)
                    {
                        if (!h.ContainsKey(cellTest.Gene.Color))
                        {
                            h.Add(cellTest.Gene.Color, 1);
                        }
                        else
                        {
                            h[cellTest.Gene.Color] += 1;
                        }

                        if (h[cellTest.Gene.Color] < h[gene.Color] || (!cellTest.Gene.Recessif && gene.Recessif))
                        {
                            gene = cellTest.Gene;
                        }
                    }
                }
            }

            cell.Gene = gene;
        }

        /// <summary>
        /// Mutate the cell.
        /// </summary>
        /// <param name="cell">The cell mutated.</param>
        public static void Mutation(Cell cell)
        {
            Random r = new Random();
            cell.Gene.Color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
        }
    }

}
