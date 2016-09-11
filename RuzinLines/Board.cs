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
    class Board
    {
        public List<EmptyCell> Cells = new List<EmptyCell>();
        public List<Ball> Balls = new List<Ball>();
        Ball choosenBall;
        EmptyCell choosenCell;
        bool theBallIsChoosen;

        private Color RandomColor()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(4);
            switch (randomNum)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                default:
                    return Color.Red;
            }
        }

        private bool RuleChecking(EmptyCell finalCell)
        {
            if (choosenCell.StartPoint.X == finalCell.StartPoint.X)
            {
                if (choosenCell.StartPoint.Y < finalCell.StartPoint.Y)
                {
                    for (int i = (choosenCell.StartPoint.Y + 50); i <= finalCell.StartPoint.Y; i += 50)
                    {
                        for (int j = 0; j < Cells.Count; j++)
                        {
                            if (Cells[j].StartPoint == new Point(choosenCell.StartPoint.X, i))
                            {
                                if (!Cells[j].IsEmpty)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    for (int i = (choosenCell.StartPoint.Y - 50); i >= finalCell.StartPoint.Y; i -= 50)
                    {
                        for (int j = 0; j < Cells.Count; j++)
                        {
                            if (Cells[j].StartPoint == new Point(choosenCell.StartPoint.X , i))
                            {
                                if (!Cells[j].IsEmpty)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            else if (choosenCell.StartPoint.Y == finalCell.StartPoint.Y)
            {
                if (choosenCell.StartPoint.X < finalCell.StartPoint.X)
                {
                    for (int i = (choosenCell.StartPoint.X + 50); i <= finalCell.StartPoint.X; i += 50)
                    {
                        for (int j = 0; j < Cells.Count; j++)
                        {
                            if (Cells[j].StartPoint == new Point(i, choosenCell.StartPoint.Y))
                            {
                                if (!Cells[j].IsEmpty)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;

                }
                else
                {
                    for (int i = (choosenCell.StartPoint.X - 50); i >= finalCell.StartPoint.X; i -= 50)
                    {
                        for (int j = 0; j < Cells.Count; j++)
                        {
                            if (Cells[j].StartPoint == new Point(i, choosenCell.StartPoint.Y))
                            {
                                if (!Cells[j].IsEmpty)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                }

            }
            else return false;
        }

        public void checkClick(Point mousePos)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                if (MouseInBounds(mousePos, Cells[i].StartPoint))
                {
                    if (Cells[i].IsEmpty)
                    {
                        if (theBallIsChoosen)
                        {
                            if (RuleChecking(Cells[i]))
                            {
                                choosenBall.BallPoint = new Point(Cells[i].StartPoint.X + 10, Cells[i].StartPoint.Y + 10);
                                Cells[i].Ball = choosenBall;
                                choosenCell.DeleteCircle();
                                choosenCell.CellColor = Color.Black;
                                choosenCell = null;
                                choosenBall = null;
                                Cells[i].DrawCircle();
                                theBallIsChoosen = false;
                            }
                            else
                            {
                                choosenCell.CellColor = Color.Black;
                                choosenBall = null;
                                choosenCell = null;
                                theBallIsChoosen = false;
                            }
                        }
                        else {
                            Ball newBall = new Ball(RandomColor(), new Point(Cells[i].StartPoint.X + 10, Cells[i].StartPoint.Y + 10));
                            Cells[i].Ball = newBall;
                            Balls.Add(newBall);
                            Cells[i].DrawCircle();
                        }
                        break;
                    }
                    else
                    {
                        if (!theBallIsChoosen)
                        {
                            Ball clickedBall = Cells[i].Ball;
                            for (int j = 0; j < Balls.Count; j++)
                            {
                                if ((Balls[j].BallPoint.X == clickedBall.BallPoint.X) && (Balls[j].BallPoint.Y == clickedBall.BallPoint.Y))
                                {
                                    choosenBall = Balls[j];
                                    choosenCell = Cells[i];
                                    theBallIsChoosen = true;
                                    Cells[i].CellColor = Color.DarkGray;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private bool MouseInBounds(Point mousePos, Point cellPos)
        {
            return (mousePos.X >= cellPos.X) && (mousePos.Y >= cellPos.Y) && (mousePos.X <= cellPos.X + 50) && (mousePos.Y <= cellPos.Y + 50);
        }
    }
}
