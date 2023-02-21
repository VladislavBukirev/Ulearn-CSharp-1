namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void Move(Robot robot, Direction direction, int amountSteps)
        {
            for (var i = 0; i < amountSteps; i++)
                robot.MoveTo(direction);
        }

        public static void FinalMove(Robot robot, int amountSteps)
        {
            Move(robot, Direction.Right, amountSteps - 3);
            Move(robot, Direction.Down, 2);
            Move(robot, Direction.Left, amountSteps - 3);
            if (!robot.Finished)
                Move(robot, Direction.Down, 2);
        }


        public static void MoveOut(Robot robot, int width, int height)
        {
            for (var i = 0; i < height / 4; i++)
                FinalMove(robot, width);
        }
    }
}