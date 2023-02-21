using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void Move(Robot robot, Direction direction, double amountSteps)
        {
            for (int i = 0; i < amountSteps; i++)
                robot.MoveTo(direction);
        }

        public static void MoveSegment(Robot robot, Direction longStep, Direction shortStep, double amountSteps)
        {
            Move(robot, longStep, amountSteps);
            if(!robot.Finished)
                Move(robot, shortStep, 1);
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            var amountSteps = Math.Max(width - 2, height - 2) / Math.Min(width - 2, height - 2);
            while (!robot.Finished)
            {
                if (height > width)
                    MoveSegment(robot, Direction.Down, Direction.Right, amountSteps);
                else
                    MoveSegment(robot, Direction.Right, Direction.Down, amountSteps);
            }
        }
    }
}