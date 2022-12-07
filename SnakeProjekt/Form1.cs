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

        }
        // Beschreibt das neuladen der Spielfeldes, wenn das GameTimerEvent
        // ausgelöst wird.
        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {

        }
        // Beschreibt den Start/Neustart des Spieles.
        private void RestartGame()
        {

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