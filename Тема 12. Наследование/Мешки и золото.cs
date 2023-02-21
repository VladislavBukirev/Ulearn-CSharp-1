using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
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
                    return new CreatureCommand{DeltaX = 0, DeltaY = 1};
                }
                if (counterFalling > 0 && Game.Map[x, y + 1] is Player)
                {
                    counterFalling++;
                    Game.Map[x, y + 1] = null;
                    return new CreatureCommand{DeltaX = 0, DeltaY = 1};
                }
            }

            if (counterFalling > 1)
            {
                counterFalling = 0;
                return new CreatureCommand{TransformTo = new Gold() };
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

            return new CreatureCommand{ DeltaX = moveX, DeltaY = moveY };
        }
        
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }
    }
}