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
        private List<Kreis> Snake = new List<Kreis>();
        private Kreis food = new Kreis();

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
            // Neues Label erstellen und zur PictureBox hinzufügen
            Label caption = new Label();
            caption.Text = "Ich erzielte: " + score + " Punkte und mein Highscore ist: " + highScore;
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.Black;
            caption.AutoSize = false;
            caption.Width = picSpielfeld.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleLeft;
            picSpielfeld.Controls.Add(caption);

            // Erstellt eine neue SaveFileDialog Box
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Snake Game Snapshot";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picSpielfeld.Width);
                int height = Convert.ToInt32(picSpielfeld.Height);
                Bitmap bmp = new Bitmap(width, height);
                picSpielfeld.DrawToBitmap(bmp, new Rectangle(0, 0, width, Height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picSpielfeld.Controls.Remove(caption);
            }
        }
        // Beschreibt das GameTimerEvent bei "Ablauf" der Zeit.
        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
            {
                Einstellungen.directions = "left";
            }
            if (goRight)
            {
                Einstellungen.directions = "right";
            }
            if (goUp)
            {
                Einstellungen.directions = "up";
            }
            if (goDown)
            {
                Einstellungen.directions = "down";

            }
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Einstellungen.directions)
                    {
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }

                    if (Snake[i].x < 0)
                    {
                        GameOver();
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        GameOver();
                    }
                    if (Snake[i].y < 0)
                    {
                        GameOver();
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        GameOver();
                    }

                    //  Körper = Snake[j]   Kopf = Snake[i]

                    if (Snake[i].x == food.x && Snake[i].y == food.y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            GameOver();
                        }

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
                    Einstellungen.Width, Einstellungen.Height
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
            lblScore.Text = "Score: ";
            score = 0;
            Kreis head = new Kreis{x = 10, y = 5};
            Snake.Add(head); // Der Kopf wird als Teil der Schlange zur Liste hinzugefügt

            for (int i=0; i<2; i++)
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
            score += 1;

            lblScore.Text = "Score: " + score;

            Kreis body = new Kreis
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };

            Snake.Add(body);
            food = new Kreis { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
        }
        // Beschreibt das Ende des Spieles.
        private void GameOver()
        {
            GameTimer.Stop();
            btnStart.Enabled = true;
            btnScreen.Enabled = true;

            if (score > highScore)
            {
                highScore = score;

                lblHighscore.Text = "Highscore: " + Environment.NewLine + highScore;
                lblHighscore.ForeColor = Color.Maroon;
                lblHighscore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
    }
}