using System;

namespace RyanCross.BattleNetworkGame
{
    public class TileMapNotInitException : Exception
    {

        public TileMapNotInitException()
        {
        }

        public TileMapNotInitException(string message)
            : base(message)
        {
        }

        public TileMapNotInitException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

