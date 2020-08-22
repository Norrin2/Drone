
using Algorithm.Logic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Algorithm.Logic.Domain.Models
{
    public class Drone
    {
        private const int MAX_NUMBER_STEPS = 2147483647;
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
        private void MoveToPositionXY(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        /// <summary>
        /// Movimenta o Drone em passo em uma determinada direção
        /// </summary>
        /// <param name="direction">Direção da movimentação do drone</param>
        /// <param name="steps">Número de passos a movimentar</param>
        private void MoveInDirection(Direction direction, int steps)
        {
            if (direction == Direction.North)
                PositionY += steps;

            if (direction == Direction.South)
                PositionY -= steps;

            if (direction == Direction.East)
                PositionX += steps;

            if (direction == Direction.West)
                PositionX -= steps;
        }


        public void ProcessInput(string input)
        {
            if (ValidateInput(input))
            {
                var directionsWithCancels = Regex.Split(input, @"([NLOS\d]*X+)");

                List<string> directionsToBeProcessed = new List<string>();

                foreach (var direction in directionsWithCancels)
                {
                    var cancels = direction.Count(c => c == 'X');
                    var directions = Regex.Replace(direction, "X", "");

                    List<string> commands = Regex.Split(directions, @"([NSLO]\d*)")
                            .Where(c => !string.IsNullOrEmpty(c))
                            .ToList();

                    commands.RemoveRange(commands.Count() - cancels, cancels);

                    directionsToBeProcessed.AddRange(commands);
                }

                directionsToBeProcessed.ForEach(d =>
                                       MoveInDirection((Direction)d[0], d.Length > 1 ? Convert.ToInt32(d.Substring(1, d.Length - 1)) : 1));
            }
            else
            {
                MoveToPositionXY(999, 999);
            }
        }


        private bool ValidateInput(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return false;

            if (Regex.IsMatch(input, @"(2147483647)(?!X)")) return false;

            return Regex.IsMatch(input, @"^(X*([NSLO]+\d*)+X*)*$");
        }

        public string GetCurrentPosition()
        {
            return $"({PositionX}, {PositionY})";
        }
    }
}
