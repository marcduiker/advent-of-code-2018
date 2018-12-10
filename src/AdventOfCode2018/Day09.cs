using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day09
    {
        public Player09 CalculateHighScore()
        {
            var game = new Game09(468, 71010*100);
            var bestPlayer = game.Play();

            return bestPlayer;
        }

        public Player09 CalculateScoreForPlayer387()
        {
            var playerId = 387;
            
            var game = new Game09(468, 71010 * 100);
            game.Play();

            return game.Players[playerId];
        }
    }

    public class Game09
    {
        private readonly int nrOfPlayers;
        private readonly int nrOfMarbles;
        public readonly List<Player09> Players;
        private readonly List<int> marbles;

        public Game09(int nrOfPlayers, int nrOfMarbles)
        {
            this.nrOfPlayers = nrOfPlayers;
            this.nrOfMarbles = nrOfMarbles;
            Players = new List<Player09>(nrOfPlayers);
            for (int p = 0; p < nrOfPlayers; p++)
            {
                Players.Add(new Player09(p));
            }
            marbles = new List<int>();
            
        }
        public Player09 Play()
        {
            int playerIndex = 0;
            int currentIndex = 0;
            int nextIndex = 0;
            Player09 player = null;
            for (int i = 0; i < nrOfMarbles; i++)
            {

                if (i > 0)
                {
                    player = Players[playerIndex];
                }

                if (i <= 1)
                {
                    marbles.Add(i);
                    currentIndex = marbles.IndexOf(i);
                }
                else if (i % 23 == 0)
                {
                    player.Score += i;
                    // remove 7th i CCW
                    nextIndex = currentIndex - 7;
                    if (nextIndex < 0)
                    {
                        nextIndex = marbles.Count + nextIndex;
                    }

                    player.Score += marbles[nextIndex];
                    marbles.RemoveAt(nextIndex);
                    currentIndex = nextIndex;
                }
                else
                {
                    // move 2 CW
                    nextIndex = currentIndex + 2;
                    if (nextIndex == marbles.Count)
                    {
                        marbles.Add(i);
                    }
                    else if (nextIndex > marbles.Count)
                    {
                        nextIndex = nextIndex - marbles.Count;
                        marbles.Insert(nextIndex, i);
                    }
                    else
                    {
                        marbles.Insert(nextIndex, i);
                    }
                    currentIndex = marbles.IndexOf(i);
                }

                if (playerIndex == nrOfPlayers-1)
                {
                    playerIndex = 0;
                }
                else
                {
                    playerIndex++;
                }
            }

            return Players.OrderByDescending(p => p.Score).FirstOrDefault();
        }
    }

    public class Player09
    {

        public Player09(int id)
        {
            Id = id;
        }
        public int Id { get; }

        public int Score { get; set; }

        public override string ToString()
        {
            return $"Player {Id}, Score {Score}";
        }
    }
}
