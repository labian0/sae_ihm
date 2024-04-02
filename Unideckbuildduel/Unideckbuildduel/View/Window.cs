using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unideckbuildduel.Logic;
using Unideckbuildduel.Logic.GameData;
using Unideckbuildduel.View;

namespace Unideckbuildduel.View
{
    /// <summary>
    /// A class for the main window for gameplay. One single instance.
    /// </summary>
    public partial class Window : Form
    {
        private readonly List<CardView> cardViews;
        private readonly List<List<BuildingView>> buildingViews;
        private Point playerOneCardStart;
        private Point playerTwoCardStart;
        private Point playerOneBuildingStart;
        private Point playerTwoBuildingStart;
        private Point playerOneBuildingCurrent;
        private Point playerTwoBuildingCurrent;
        private int playerDrawOnceNum = -1;
        private string playerDrawOnceCardType = "";
        /// <summary>
        /// A reference to the single instance of this class
        /// </summary>
        public static Window GetWindow { get; } = new Window();
        private Window()
        {
            InitializeComponent();
            ViewSettings.Rightmost = outputListBox.Left;
            cardViews = new List<CardView>();
            buildingViews = new List<List<BuildingView>>
            {
                new List<BuildingView>(),
                new List<BuildingView>()
            };
            playerOneCardStart = new Point(10, 10);
            playerTwoCardStart = new Point(10, 500);
            playerOneBuildingStart = new Point(25, 190);
            playerTwoBuildingStart = new Point(25, 370);
            playerOneBuildingCurrent=playerOneBuildingStart;
            playerTwoBuildingCurrent=playerTwoBuildingStart;
            UpdateStack();

        }
        /// <summary>
        /// Method called by the controler whenever some text should be displayed
        /// </summary>
        /// <param name="s"></param>
        public void WriteLine(string s)
        {
            List<string> strs = s.Split('\n').ToList();
            strs.ForEach(str => outputListBox.Items.Add(str));
            if (outputListBox.Items.Count > 0)
            {
                outputListBox.SelectedIndex = outputListBox.Items.Count - 1;
            }
            outputListBox.Refresh();
        }
        /// <summary>
        /// Method called to display a new building
        /// </summary>
        /// <param name="playerNumber">The number of the player</param>
        /// <param name="card">The card to base the building on</param>
        public void AddNewBuilding(int playerNumber, Card card)
        {
            Point point = playerNumber == 0 ? playerOneBuildingCurrent : playerTwoBuildingCurrent;
            Point start = playerNumber == 0 ? playerOneBuildingStart : playerTwoBuildingStart;

            buildingViews[playerNumber].Add(BuildingView.MakeBuildingOrNull(card.CardType, point));
            point.Offset(ViewSettings.BuildSize.Width, 0);
            point.Offset(ViewSettings.Margin.Width, 0);
            if (point.X + ViewSettings.BuildSize.Width + ViewSettings.Margin.Width >= ViewSettings.Rightmost)
            {
                point.X = start.X;
                point.Y = point.Y + ViewSettings.BuildSize.Height + ViewSettings.Margin.Height;
            }

            if (playerNumber == 0)
            {
                playerOneBuildingCurrent = point;
            }
            else
            {
                playerTwoBuildingCurrent = point;
            }
            Refresh();
        }

        /// <summary>
        /// Method called whenever the cards should be displayed
        /// </summary>
        /// <param name="playerNumber">The number of the player</param>
        /// <param name="cards">The cards to display for the player</param>
        public void CardsForPlayer(int playerNumber, List<Card> cards)
        {
            cardViews.Clear();
            Point point = playerNumber == 0 ? playerOneCardStart : playerTwoCardStart;
            int i = 0;
            foreach (Card c in cards)
            {
                cardViews.Add(new CardView(c, point, i++));
                point.Offset(ViewSettings.CardSize.Width, 0);
                point.Offset(ViewSettings.Margin.Width, 0);
            }
            Refresh();
        }
        #region Event handling
        private void Window_Paint(object sender, PaintEventArgs e)
        {
            foreach(CardView cv in cardViews) { cv.Draw(e.Graphics); }
            foreach (List<BuildingView> lbv in buildingViews)
            {
                foreach (BuildingView bv in lbv) { bv.Draw(e.Graphics); }
            }
            playerOneScoreLabel.Text = Controller.GetControler.NumberOfTurns;
            playerTwoScoreLabel.Text = Controller.GetControler.PlayerOneScore;
            turnLabel.Text = Controller.GetControler.PlayerTwoScore;
        }

        private void NextTurnButton_Click(object sender, EventArgs e)
        {
            if (Game.GetGame.GameStatus == GameStatus.Playing) Game.GetGame.PlayPhaseEnded();
            else Controller.GetControler.EndTurn();
        }


        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        public void DisableTurnButton()
        {
            nextTurnButton.Enabled = false;
            placeAllButton.Enabled = false;
            restartButton.Visible = true;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            nextTurnButton.Enabled = true;
            placeAllButton.Enabled = true;
            restartButton.Visible  = false;
            Controller.GetControler.StartEverything();
        }

        private void Window_MouseClick(object sender, MouseEventArgs e)
        {
                for (int i = 0; i < cardViews.Count; i++)
                {
                    if (cardViews[i].Rect.Contains(e.Location))
                    {
                        if (Game.GetGame.GameStatus == GameStatus.Playing)
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                Game.GetGame.PlayCard(Game.GetGame.CurrentPlayer, cardViews[i].CardNum);
                            }
                            else if (e.Button == MouseButtons.Right && cardViews[i].card.CardType.Effect == Effect.CanExchange)
                            {
                                Game.GetGame.DiscardCard(Game.GetGame.CurrentPlayer, cardViews[i].CardNum);
                                Game.GetGame.DrawOneCard(Game.GetGame.CurrentPlayer);
                            }
                        }
                        else if (Game.GetGame.GameStatus == GameStatus.Discarding)
                        { 
                            Game.GetGame.DiscardCard(Game.GetGame.CurrentPlayer, cardViews[i].CardNum);
                            break;
                        }
                    }
                }
            UpdateStack();
        }

        private void placeAllButton_Click(object sender, EventArgs e)
        {
            Controller.GetControler.PlaceAllCards();
        }

        private void UpdateStack()
        {
            deckLabel.Text = "Pioche : " + Game.GetGame.GetCommonDeckLenght;
            discardLabel.Text = "Defausse : " + Game.GetGame.GetDiscardLenght;
        }
        public void UpdateNextTurnButtonLabel()
        {
            if(Game.GetGame.GameStatus == GameStatus.Playing)
            {
                nextTurnButton.Text = "Discard";
            }
            else
            {
                nextTurnButton.Text = "Next Turn";
            }
        }

        public void InitDrawOncePerTurnButton(int numPlayer, string cardTypeName)
        {
            Player player = Game.GetGame.players[numPlayer];
            playerDrawOnceCardType = cardTypeName;
            playerDrawOnceNum = numPlayer;
            DrawOncePerTurnButton.Text = "Piocher pour une carte de type " + cardTypeName;
            DrawOncePerTurnButton.Visible = true;
        }

        private void DrawOncePerTurnButton_Click(object sender, EventArgs e)
        {
            if (playerDrawOnceNum != -1) Game.GetGame.DrawOncePerTurn(playerDrawOnceNum,playerDrawOnceCardType);

            DrawOncePerTurnButton.Visible = false;
            DrawOncePerTurnButton.Text = "";
            playerDrawOnceNum = -1;
            playerDrawOnceCardType = "";
            Refresh();
        }
    }
}
