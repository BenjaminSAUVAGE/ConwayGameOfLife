// <copyright file="Program.cs" company="BS">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConwayGameOfLife
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The class who contain the main execution of the pogram.
    /// </summary>
    public class Program : Form
    {
        public const int SQUARESIZE = 20;

        private readonly Cell cells = new Cell();
        private Color c = Color.Red;
        private bool recessif = false;
       

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        public Program()
        {
            this.cells.Setup(this.Width / SQUARESIZE, this.Height / SQUARESIZE);
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(this.Menu);
            this.SizeChanged += new EventHandler(this.ChangeSize);
            this.MouseClick += new MouseEventHandler(this.ChangeState);
            this.Paint += new PaintEventHandler(this.CellDrawAll_Handler);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            Application.Run(new Program());
        }

        private void CellDrawAll_Handler(object sender, PaintEventArgs e)
        {
            this.SuspendLayout();
            this.cells.DrawAll(e.Graphics);
            this.ResumeLayout();
        }

        private new void Menu(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cells.NextGen();
            }

            if (e.KeyCode == Keys.C)
            {
                this.cells.Setup(this.Width / SQUARESIZE, this.Height / SQUARESIZE);
            }

            if (e.KeyCode == Keys.R)
            {
                this.cells.Random();
            }

            if (e.KeyCode == Keys.P)
            {
                this.c = Color.Blue;
            }

            this.Refresh();
        }

        private void ChangeState(object sender, MouseEventArgs e)
        {
            int y = e.Y / SQUARESIZE;
            int x = e.X / SQUARESIZE;
            this.cells.Cells[x, y].Alive = !this.cells.Cells[x, y].Alive;
            this.cells.Cells[x, y].Gene.Color = this.c;
            this.cells.Cells[x, y].Gene.Recessif = this.recessif;
            Rectangle rc = new Rectangle(x * 20, y * 20, SQUARESIZE, SQUARESIZE);
            this.Invalidate(rc);
            this.Update();
        }

        private void ChangeSize(object sender, EventArgs e)
        {
            this.cells.Setup(this.Width / SQUARESIZE, this.Height / SQUARESIZE);
            this.Refresh();
        }
    }
}
