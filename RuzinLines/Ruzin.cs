using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuzinLines
{
    class Ruzin
    {
        private Board _board;

        public Ruzin(Board board)
        {
            _board = board;
        }


        private void CreateNewBalls()
        {
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int newNumber = rnd.Next(0, _board.EmptyCells.Count-1);
                Ball newBall = new Ball(_board.RandomColor(), new Point(_board.EmptyCells[newNumber].StartPoint.X + 10, _board.EmptyCells[newNumber].StartPoint.Y + 10));
                _board.EmptyCells[newNumber].Ball = newBall;
                _board.EmptyCells[newNumber].IsEmpty = false;
                _board.Balls.Add(newBall);
                _board.EmptyCells.RemoveAt(newNumber);
            }
        }

        public void DrawFirst(Graphics gr)
        {
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    EmptyCell cell = new EmptyCell(new Point(i * 50, j * 50), gr);
                    _board.Cells.Add(cell);
                    _board.EmptyCells.Add(cell);
                    gr.FillRectangle(new SolidBrush(Color.Black), i * 50, j * 50, 50, 50);
                    gr.DrawRectangle(new Pen(Color.White), i * 50, j * 50, 50, 50);
                }
                
            }
            CreateNewBalls();

            for (int i = 0; i < _board.Balls.Count; i++)
            {
                Ball ball = _board.Balls[i];
                RectangleF rect = new RectangleF(ball.BallPoint.X, ball.BallPoint.Y, 30, 30);

                gr.FillEllipse(new SolidBrush(_board.Balls[i].BallColor), rect);
                gr.DrawEllipse(Pens.White, rect);
            }
        }

        public void Draw(Graphics gr)
        {
            int cellCounter = 0;
            

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    gr.FillRectangle(new SolidBrush(_board.Cells[cellCounter].CellColor), i * 50, j * 50, 50, 50);
                    gr.DrawRectangle(new Pen(Color.White), i * 50, j * 50, 50, 50);
                    cellCounter++;
                }
            }

            for (int i = 0; i < _board.Balls.Count; i++)
            {
                Ball ball = _board.Balls[i];
                RectangleF rect = new RectangleF(ball.BallPoint.X, ball.BallPoint.Y, 30, 30);

                gr.FillEllipse(new SolidBrush(_board.Balls[i].BallColor), rect);
                gr.DrawEllipse(Pens.White, rect);
            }
        }
    }
}
