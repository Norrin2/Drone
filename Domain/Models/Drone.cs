
using Algorithm.Logic.Domain.Enums;

namespace Algorithm.Logic.Domain.Models
{
    public class Drone
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Drone(int positionX = 0, int positionY = 0)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        /// <summary>
        /// Movimenta o Drone para um posição X e Y específicas
        /// </summary>
        /// <param name="x">Nova posição X do Drone</param>
        /// <param name="y">Nova posição Y do Drone</param>
        public void MoveToPositionXY(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        /// <summary>
        /// Movimenta o Drone em passo em uma determinada direção
        /// </summary>
        /// <param name="direction">Direção da movimentação do drone</param>
        /// <param name="steps">Número de passos a movimentar</param>
        public void MoveInDirection(Direction direction, int steps)
        {
            if (direction == Direction.N)
                PositionY += steps;

            if (direction == Direction.S)
                PositionY -= steps;

            if (direction == Direction.L)
                PositionX += steps;

            if (direction == Direction.O)
                PositionX -= steps;
        }
    }
}
