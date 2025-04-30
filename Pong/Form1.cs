using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(10, 170, 10, 60);
        Rectangle player2 = new Rectangle(580, 170, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = -6;
        int ballYSpeed = 6;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < 400 - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            //move player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < 400 - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            //check if ball hits top wall or the bottom wall
            if(ball.Y < 0 || ball.Y > 400 - ball.Height)
            {
                ballYSpeed = ballYSpeed * -1;
            }

            //check if ball hits either player.
            //If it does change the direction
            //and place the ball in front of the player hit
            if (player1.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                ball.X = player1.X + ball.Width;
            }
            else if (player2.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                ball.X = player2.X - ball.Width;
            }

            //check if player missses the ball
            if (ball.X < 0)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 170;
                player2.Y = 170;
            }
            else if (ball.X > 600 - ball.Width)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 170;
                player2.Y = 170;
            }


            Refresh(); // runs the Paint method
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
        }
    }
}
