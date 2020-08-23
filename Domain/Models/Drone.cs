
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
        /// <param name="movement">Direção da movimentação do drone e passos</param>
        private void Move(Movement movement)
        {
            if (movement == null)
                throw new ArgumentNullException("Movimento nulo");

            switch (movement.Direction)
            {
                case Direction.North:
                    PositionY += movement.Steps;
                    break;
                case Direction.South:
                    PositionY -= movement.Steps;
                    break;
                case Direction.East:
                    PositionX += movement.Steps;
                    break;
                case Direction.West:
                    PositionX -= movement.Steps;
                    break;
                default:
                    throw new ArgumentException("Direção inválida");
            }
        }

        /// <summary>
        /// Processa os inputs e movimenta o drone baseado na string enviada
        /// </summary>
        /// <param name="input">String no padrão "N1N2S3S4L5L6O7O8X"</param>
        public void ProcessInput(string input)
        {
            if (ValidateInput(input))
            {
                //Separa os inputs por cancelamento
                var movmentsWithCancels = Regex.Split(input, @"([NLOS\d]*X+)");

                List<string> movementsToBeProcessed = new List<string>();

                foreach (var movement in movmentsWithCancels)
                {
                    //Conta o número de comandos de cancelamento e os remove
                    var cancels = movement.Count(cancel => cancel == 'X');
                    var movementsWithoutCancels = Regex.Replace(movement, "X", "");

                    //Separa cada movimento dos inputs
                    List<string> movementInputs = Regex.Split(movementsWithoutCancels, @"([NSLO]\d*)")
                                                 .Where(command => !string.IsNullOrEmpty(command))
                                                 .ToList();

                    //Remove movimentos cancelados
                    movementInputs.RemoveRange(movementInputs.Count() - cancels, cancels);

                    movementsToBeProcessed.AddRange(movementInputs);
                }

                //Executa os movimentos
                try
                {
                    movementsToBeProcessed.ForEach(movementInput => Move(new Movement(movementInput)));
                } catch
                {
                    MoveToPositionXY(999, 999);
                }
            }
            else
            {
                MoveToPositionXY(999, 999);
            }
        }


        private bool ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            // Retorna inválido caso haja um comando com número de passos maior que o permitido
            if (Regex.IsMatch(input, @"(" + MAX_NUMBER_STEPS.ToString() + ")(?!X)")) return false;

            return Regex.IsMatch(input, @"^(X*([NSLO]+\d*)+X*)*$");
        }

        /// <summary>
        /// Devolve a posição atual do drone formatada em (X, Y)
        /// </summary>
        public string GetCurrentPosition()
        {
            return $"({PositionX}, {PositionY})";
        }
    }
}
