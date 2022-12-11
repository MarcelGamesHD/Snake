using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Für den JPG - Kompressor
using System.Drawing.Imaging;


namespace SnakeProjekt
{
    // Kommentar von Marcel
    public partial class frmSnake : Form
    {
        // Eine Liste namens "Snake" wird erstellt
        private List<Kreis> Snake = new List<Kreis>();
        private Kreis food = new Kreis();

        // Maximale Width und Height, welche erreichbar sind
        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rand = new Random();

        bool goLeft, goRight, goUp, goDown;
        public frmSnake()
        {
            InitializeComponent();

            new Einstellungen();
        }

        // Wir überprüfen, welche Taste gedrückt wird, und wir bewegen die Schlange
        // in diese Richtung.
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Einstellungen.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Einstellungen.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Einstellungen.directions != "down") 
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Einstellungen.directions != "up")
            {
                goDown = true;
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

        }
        // Beim drücken des Start-Buttons wird die Funktion von "RestartGame()"
        // aufgerufen.
        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }
        // Ein Screenshot wird aufgenommen, und so verarbeitet,
        // dass er abgespeichert werden kann.
        private void TakeScreenshot(object sender, EventArgs e)
        {

        }
        // Beschreibt das GameTimerEvent bei "Ablauf" der Zeit.
        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
            {
                Einstellungen.directions = "Links";
            }
            if (goRight)
            {
                Einstellungen.directions = "Rechts";
            }
            if (goUp)
            {
                Einstellungen.directions = "Oben";
            }
            if (goDown)
            {
                Einstellungen.directions = "Unten";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Einstellungen.directions)
                    {
                        case "Links":
                            Snake[i].x--;
                            break;
                        case "Rechts":
                            Snake[i].x++;
                            break;
                        case "Oben":
                            Snake[i].y++;
                            break;
                        case "Unten":
                            Snake[i].y--;
                            break;
                    }

                    if (Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }
                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }
            picSpielfeld.Invalidate();
        }
        // Beschreibt das neuladen der Spielfeldes, wenn das GameTimerEvent
        // ausgelöst wird.
        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics Spielfeld = e.Graphics;
            Brush SnakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    SnakeColour = Brushes.DarkGreen;
                }
                else
                {
                    SnakeColour = Brushes.ForestGreen;
                }
                Spielfeld.FillEllipse(SnakeColour, new Rectangle
                    (
                    Snake[i].x * Einstellungen.Width,
                    Snake[i].y * Einstellungen.Height,
                    Einstellungen.Width,Einstellungen.Height
                    ));
            }
            Spielfeld.FillEllipse(Brushes.DarkRed, new Rectangle
                    (
                    food.x * Einstellungen.Width,
                    food.y * Einstellungen.Height,
                    Einstellungen.Width, Einstellungen.Height
                    ));
        }
        // Beschreibt den Start/Neustart des Spieles.
        private void RestartGame()
        {
            maxWidth = picSpielfeld.Width / Einstellungen.Width;
            maxHeight = picSpielfeld.Height / Einstellungen.Height;

            Snake.Clear();
            btnStart.Enabled = false;
            btnScreen.Enabled = false;
            lblScore.Text = "Score: " + score;

            Kreis head = new Kreis{x = 10, y = 5};
            Snake.Add(head); // Der Kopf wird als Teil der Schlange zur Liste hinzugefügt

            for (int i=0; i<10; i++)
            {
                Kreis body = new Kreis();
                Snake.Add(body);
            }

            food = new Kreis { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
            GameTimer.Start();
        }
        // Beschreibt das essen des Essen.
        private void EatFood()
        {

        }
        // Beschreibt das Ende des Spieles.
        private void GameOver()
        {

        }
    }
}