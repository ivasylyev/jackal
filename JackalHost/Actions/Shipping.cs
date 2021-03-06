﻿using Jackal;

namespace JackalHost.Actions
{
    class Shipping : IGameAction
    {
        private readonly Pirate _pirate;
        private readonly Ship _ship;

        public Shipping(Pirate pirate, Ship ship)
        {
            _pirate = pirate;
            _ship = ship;
        }

        public void Act(Game game)
        {
            Position position = _pirate.Position;
            Board board = game.Board;
            Tile tile = board.Map[position.X, position.Y];
            tile.Pirates.Remove(_pirate);
            if (tile.Pirates.Count == 0)
            {
                tile.OccupationTeamId = null;
            }

            _pirate.Position = _ship.Position;
            _ship.Crew.Add(_pirate);
            var coins = _pirate.Coins;
            _ship.Coins += coins;
            _pirate.Coins = 0;

            game.Scores[_pirate.TeamId] += coins;
            game.CoinsLeft -= coins;
        }
    }
}