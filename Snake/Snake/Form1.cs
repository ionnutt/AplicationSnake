﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle>Snake = new List<Circle>();
        private Circle food = new Circle();

        public Brush snakeColour { get; private set; }

        public Form1()
        {
            //Set settings to default
            new Settings();


            //Set game speed and start timer
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScore();
            gameTimer.Start();

            //Start New Game
            StartGame();
        }

        private EventHandler UpdateScore()
        {
            throw new NotImplementedException();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;
            //Set settings to default
            new Settings();

            //Ceate new player object
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }

        //Place random food game 
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }


        public  void UpdateScreen(object sender,EventArgs e)
        {
            //Check for GameOver
            if(Settings.GameOver == true)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();

                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                MovePlayer();

            }

            pbCanvas.Invalidate();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if(Settings.GameOver != false)
            {
                //Draw snake

                for(int i =0; i<Snake.Count; i++)
                {
                    //Draw head
                    if (i == 0)
                        Brush snakeColour = Brushes.Black;
                    else
                        //Draw rest for body
                        snakeColour = Brushes.Green;

                    //Draw snake
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));

                    //Draw food
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Height, Settings.Width, Settings.Height));

                }
            }
            else
            {
                string gameOver = "Game over\n Your final score is : " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for(int i =Snake.Count -1; i>0; i--)
            {
                //Move head
                if (i == 0 )
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right: Snake[i].X++; break;
                        case Direction.Left: Snake[i].X--; break;
                        case Direction.Up: Snake[i].Y--; break;
                        case Direction.Down: Snake[i].Y++; break;

                    }
                }
                else
                {
                    //Move body
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
    }
}
