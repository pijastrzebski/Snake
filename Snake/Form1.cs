using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            

            InitializeComponent();


            //ustawienie ustawien domyslnych
            new Settings();

            //ustaw predkosc gry i licznik startu
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //zacznij nowa gre
            StartGame();
        }
        private void StartGame()
        {
            
            lblGameOver.Visible = false;
            kroliczek.Visible = false;

            new Settings();

            //stworz nowy obiekt gracza
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

            

        }

        //randomowe zarcie dla weza
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            // czy game over sprawdz
            if (Settings.GameOver == true)
            {
                //sprawdz czy nacisniety zostal enter
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
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                //ustaw kolor weza
                Brush snakeColour;

                //rysuj weza
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColour = Brushes.Yellow; //rysuj glowe
                    else
                        snakeColour = Brushes.Green;  //reszta ciala

                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height));

                    // rysuj zarcie
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height, Settings.Width, Settings.Height));
                }
            }
            else
            {
                SnakeTheme();

                string gameOver = "Koniec gry!\n Twoj wynik to: " + Settings.Score + "\nWcisnij 'Enter' by sprobowac raz jeszcze!";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
                kroliczek.Visible = true;
                
            }
        }

        private void MovePlayer()
        {
            
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                // rusz glowa
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;


                    }

                    // max X i Y Pos
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    // wykryj kolizje z granicami planszy
                    if (Snake[i].X < 0 ||
                        Snake[i].Y < 0 || 
                        Snake[i].X >= maxXPos ||
                        Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }
                    // wykryj kolizje z cialem
                    for (int j=1; j<Snake.Count; j++)
                    {
                        if(Snake[i].X == Snake[j].X &&
                           Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    // kolizja z jedzeniem
                    if(Snake[0].X == food.X &&
                       Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    // rusz cialem weza
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Eat()
        {
            // dodanie kolka do ciala weza
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);

            // aktualizacja punktow
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();

        }
        private void Die()
        {
            Settings.GameOver = true;


        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }

        //      dzwieki nie dzialaja
        private void SnakeTheme()
        {
            var player3 = new WMPLib.WindowsMediaPlayer();
            player3.URL = @"C:\Users\Piotr\Desktop\moje gry\GOT.midi";
            
            player3.controls.play();
        }

    }
}
