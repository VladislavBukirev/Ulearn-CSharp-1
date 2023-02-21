using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public CreatureCommand Act(int x, int y)
        {
            var playerCoordinates = GetPlayerCoordinates();
            if (playerCoordinates[0] == -1)
                return new CreatureCommand();
            var playerX = playerCoordinates[0];
            var playerY = playerCoordinates[1];
            var movingCoordinates = GetMoving(x, y, playerX, playerY);
            var moveX = movingCoordinates[0];
            var moveY = movingCoordinates[1];

            if (x + moveX != playerX || y + moveY != playerY - 1)
                return CheckMonsterInsideOrCantMove(x, y, moveX, moveY)
                    ? new CreatureCommand()
                    : new CreatureCommand { DeltaX = moveX, DeltaY = moveY };
            Game.Map[x + moveX, y + moveY] = null;
            return new CreatureCommand();
        }

        private static int[] GetMoving(int x, int y, int playerX, int playerY)
        {
            var moveX = 0;
            var moveY = 0;
            if (x == playerX)
            {
                moveY = y > playerY ? -1 : 1;
            }
            else if (y == playerY)
            {
                moveX = x > playerX ? -1 : 1;
            }
            else
            {
                moveX = x > playerX ? -1 : 1;
            }

            return new[] { moveX, moveY };
        }

        private static bool CheckMonsterInsideOrCantMove(int x, int y, int moveX, int moveY)
        {
            var movementX = x + moveX;
            var movementY = y + moveY;
            var newPosition = Game.Map[x + moveX, y + moveY];
            return movementX >= Game.MapWidth || movementX < 0 || movementY >= Game.MapHeight || movementY < 0
                   || newPosition is Sack || newPosition is Monster || newPosition is Terrain;
        }

        private static int[] GetPlayerCoordinates()
        {
            for (var i = 0; i < Game.MapWidth; i++)
            {
                for (var j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] is Player)
                        return new[] { i, j };
                }
            }

            return new[] { -1, -1 };
        }
    }

    public class Sack : ICreature
    {
        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        private int counterFalling;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null)
                {
                    counterFalling++;
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                }

                if (counterFalling > 0 && (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster))
                {
                    counterFalling++;
                    Game.Map[x, y + 1] = null;
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                }
            }

            if (counterFalling > 1)
            {
                counterFalling = 0;
                return new CreatureCommand { TransformTo = new Gold() };
            }

            counterFalling = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var moveX = 0;
            var moveY = 0;
            switch (Game.KeyPressed)
            {
                case (Keys.Left):
                    moveX = -1;
                    break;
                case (Keys.Right):
                    moveX = 1;
                    break;
                case (Keys.Up):
                    moveY = -1;
                    break;
                case (Keys.Down):
                    moveY = 1;
                    break;
            }

            if (x + moveX >= Game.MapWidth || x + moveX < 0 || y + moveY >= Game.MapHeight
                || y + moveY < 0 || Game.Map[x + moveX, y + moveY] is Sack)
            {
                return new CreatureCommand();
            }

            return new CreatureCommand { DeltaX = moveX, DeltaY = moveY };
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }
    }
}