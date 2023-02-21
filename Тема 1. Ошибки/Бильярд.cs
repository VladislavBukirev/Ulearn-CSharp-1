using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <param name="directionRadians">Угол направления движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns>Возвращает искомый угол</returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return 2 * wallInclinationRadians - directionRadians;
        }
    }
}