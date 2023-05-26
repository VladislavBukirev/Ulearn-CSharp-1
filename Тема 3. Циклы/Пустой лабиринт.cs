namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveRight(Robot robot, int width)
        {
            for (int i = 0; i <= width - 4; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveDown(Robot robot, int height)
        {
            for (int i = 0; i <= height - 4; i++)
                robot.MoveTo(Direction.Down);
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveRight(robot, width);
            MoveDown(robot, height);
        }
    }
}