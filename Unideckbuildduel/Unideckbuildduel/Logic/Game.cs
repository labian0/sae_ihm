﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Unideckbuildduel.Logic.GameData;
using Unideckbuildduel.View;

namespace Unideckbuildduel.Logic
{
    /// <summary>
    /// A class used for the game's logic. One single instance at a time.
    /// </summary>
    public class Game
    {
        private Stack<Card> commonDeck;
        private Stack<Card> discard;
        public List<Player> players;
        public Dictionary<Player, List<Card>> cards;
        private Dictionary<Player, List<Card>> buildings;
        public Dictionary<Player, string> playerDrawOnceCardType;

        /// <summary>
        /// A reference to the single instance of this class
        /// </summary>
        public static Game GetGame { get; } = new Game();

        public int GetCommonDeckLenght { get { return commonDeck != null ? commonDeck.Count : 0; } }

        public int GetDiscardLenght { get { return discard != null ? discard.Count : 0; } }
        /// <summary>
        /// Turn number (from 0)
        /// </summary>
        public int Turn { get; set; }
        /// <summary>
        /// The current phase
        /// </summary>
        public GameStatus GameStatus {get; private set;}
        /// <summary>
        /// The current player (0-1)
        /// </summary>
        public int CurrentPlayer { get; private set; }
        private Game() {}
        /// <summary>
        /// Method used to launch a new game (at startup or after)
        /// </summary>
        public void NewGame(string playerOneName, string playerTwoName)
        {
            commonDeck = LoadData.GenStack();
            discard = new Stack<Card>();
            players = new List<Player> { new Player { Name = playerOneName }, new Player { Name = playerTwoName } };
            cards = new Dictionary<Player, List<Card>>();
            buildings = new Dictionary<Player, List<Card>>();
            playerDrawOnceCardType = new Dictionary<Player, string>();
            foreach (Player p in players)
            {
                p.Number = players.IndexOf(p);
                cards.Add(p, new List<Card>());
                buildings.Add(p, new List<Card>());
                playerDrawOnceCardType.Add(p, null);
            }
            GameStatus = GameStatus.TurnStart;
            CurrentPlayer = 0;
            Turn = 0;
        }
        /// <summary>
        /// Method called to end the discard phase
        /// </summary>
        public void DiscardPhaseEnded()
        {
            GameStatus = GameStatus.Ended;
        }
        /// <summary>
        /// Try to play a specific card.
        /// </summary>
        /// <param name="playerNum">The number of the player</param>
        /// <param name="cardNum">The number of the card</param>
        /// <returns>msg: a string containing a message, ok: true iff the card could be played</returns>
        public (string msg, bool ok) PlayCard(int playerNum, int cardNum)
        {
            Card card = cards[players[playerNum]][cardNum];
            return PlayCard(playerNum, card);
        }
        private (string msg, bool ok) PlayCard(int playerNum, Card card)
        {
            if (card == null) { return ("Card playing error", false); }
            if (card.CardType.Effect == Effect.OneMoreCard) { players[playerNum].HandSize = 6; }
            if (card.CardType.Effect == Effect.DrawOncePerTurn)
            {
                Window.GetWindow.InitDrawOncePerTurnButton(playerNum, card.CardType.EffectCard.Name);
            }
            switch (card.CardType.Kind)
            {
                case Kind.Building:
                    var reqBs = card.CardType.RequiredBuildings;
                    var reqRs = card.CardType.RequiredRessources;
                    List<Card> resourcesToRemove = new List<Card>(); ;
                    if (reqBs != null && reqBs.Count > 0)
                    {
                        bool reqBok = true;
                        foreach (CardType b in reqBs.Keys)
                        {
                            int presB = NumberOfCardsPresent(buildings[players[playerNum]], b);
                            if (presB < reqBs[b])
                            {
                                reqBok = false;
                            }
                        }
                        if (!reqBok)
                        {
                            return ("Not enough required buildings", false);
                        }
                        bool reqRok = true;
                        if (reqRs != null && reqRs.Count > 0)
                        {
                            foreach (CardType r in reqRs.Keys)
                            {
                                int presR = 0;
                                foreach (Card c in cards[players[playerNum]])
                                {
                                    if (c.CardType.Equals(r) && !resourcesToRemove.Contains(c))
                                    {
                                        presR++;
                                        resourcesToRemove.Add(c);
                                    }
                                }
                                if (presR < reqRs[r])
                                {
                                    reqRok = false;
                                }
                            }
                        }
                        if (!reqRok)
                        {
                            return ("Not enough required resources", false);
                        }
                    }
                    buildings[players[playerNum]].Add(card);
                    cards[players[playerNum]].Remove(card);
                    foreach(Card r in resourcesToRemove)
                    {
                        cards[players[playerNum]].Remove(r);
                    }
                    players[playerNum].Points += card.CardType.Points;
                    Controller.GetControler.NewBuilding(playerNum, card);
                    Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                    return (null, true);

                case Kind.Action:
                    cards[players[playerNum]].Remove(card);
                    discard.Push(card);
                    Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                    return (null, true);

                default:
                    return ("Card type not handled yet", false);
            }
        }
        private static int NumberOfCardsPresent(List<Card> cards, CardType type)
        {
            if (cards == null || cards.Count == 0 || type == null)
            {
                return 0;
            }
            int res = 0;
            foreach (Card card in cards)
            {
                if (card.CardType.Equals(type))
                {
                    res++;
                }
            }
            return res;
        }
        /// <summary>
        /// Method called when the game should advance; sometimes recursive, sometimes not.
        /// </summary>
        public void Play()
        {
            switch (GameStatus)
            {
                case GameStatus.TurnStart:
                    GameStatus = GameStatus.Drawing;
                    Play();
                    break;
                case GameStatus.Drawing:
                    Controller.GetControler.DrawStart(CurrentPlayer);
                    break;
                case GameStatus.Playing:
                    Controller.GetControler.DisplayHand(CurrentPlayer, cards[players[CurrentPlayer]]);
                    Controller.GetControler.PlayPhaseStart(CurrentPlayer);
                    break;
                case GameStatus.Discarding:
                    Controller.GetControler.DiscardStart(CurrentPlayer);
                    break;
                case GameStatus.Ended:
                    CurrentPlayer = (CurrentPlayer + 1) % players.Count;
                    if (CurrentPlayer == 0)
                    {
                        Turn++;
                    }
                    Controller.GetControler.TurnEnded(CurrentPlayer, Turn);
                    GameStatus = GameStatus.TurnStart;
                    Play();
                    break;
            }
        }
        public void DrawOncePerTurn(int numPlayer, string cardTypeName)
        {
            Player player = players[numPlayer];
            Card card = null;
            Stack<Card> newDeck = new Stack<Card>();
            while (commonDeck.Count != 0)
            {
                Card topCard = commonDeck.Pop();
                if(card == null && topCard.CardType.Name == cardTypeName) card = topCard;
                else newDeck.Push(topCard);
            }
            commonDeck = newDeck;
            if (card == null) return;
            if (cards[player].Count < player.HandSize)
            {
                cards[player].Add(card);
            }
        }
        /// <summary>
        /// Method called to discard a specific card.
        /// </summary>
        /// <param name="playerNum">The number of the player</param>
        /// <param name="cardNum">The number of the card</param>
        /// <returns>True iff the card was actually discarded</returns>
        public bool DiscardCard(int playerNum, int cardNum)
        {
            Card card = cards[players[playerNum]][cardNum];
            Window.GetWindow.Refresh();
            return DiscardCard(playerNum, card);
        }
        private bool DiscardCard(int playerNum, Card card)
        {
            if (card == null) { return false; }
            discard.Push(card);
            bool ok = cards[players[playerNum]].Remove(card);
            Window.GetWindow.CardsForPlayer(playerNum, cards[players[playerNum]]);
            return ok;
        }
        /// <summary>
        /// Draw one card for a player from the deck
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>A reference to the drawn card, also added to the player's hand</returns>
        public Card DrawOneCard(int num)
        {
            if (PlayerHandSize(num)<=PlayerCardCount(num)) { return null; }
            if (commonDeck.Count <= 0)
            {
                commonDeck = ShuffleStack(discard);// Deck reload!
                discard = new Stack<Card>();
            }
            Card c = null;
            if (commonDeck.Count > 0)
            {
                c = commonDeck.Pop();
                cards[players[num]].Add(c);
            }
            return c;
        }
        /// <summary>
        /// Method called to end the draw phase
        /// </summary>
        public void DrawPhaseEnded()
        {
            GameStatus = GameStatus.Playing;
            Window.GetWindow.UpdateNextTurnButtonLabel();
        }

        public void PlayPhaseEnded()
        {
            GameStatus = GameStatus.Discarding;
            Window.GetWindow.UpdateNextTurnButtonLabel();
        }

        public bool isPlayable(int currentPlayer, int cardNum)
        {
            if (cards[players[currentPlayer]][cardNum].CardType.RequiredRessources != null)
            {
                return cards[players[currentPlayer]][cardNum].CardType.RequiredRessources.Count == 0;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Read-only access to the players' names
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's name</returns>
        public string PlayerName(int num) => players[num].Name;
        /// <summary>
        /// Read-only access to the players' scores
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's score</returns>
        public int PlayerScore(int num) => players[num].Points;
        /// <summary>
        /// Read-only access to the players' handsizes
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's hand's size</returns>
        public int PlayerHandSize(int num) => players[num].HandSize;
        /// <summary>
        /// Read-only access to the players' number of cards
        /// </summary>
        /// <param name="num">The number of the player</param>
        /// <returns>The player's number of cards</returns>
        public int PlayerCardCount(int num) => cards[players[num]].Count;

        public Stack<Card> ShuffleStack(Stack<Card> stack)
        {
            Random random = new Random();
            List<Card> list = stack.ToList();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int r = random.Next(i + 1);
                (list[i], list[r]) = (list[r], list[i]);
            }
            Stack<Card> newStack = new Stack<Card>();
            foreach (Card card in list)
            {
                newStack.Push(card);
            }
            return newStack;
        }

    }
}
