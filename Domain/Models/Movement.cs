﻿using Algorithm.Logic.Domain.Enums;
using System;

namespace Algorithm.Logic.Domain.Models
{
    public class Movement
    {
        public Direction Direction { get; private set; }
        public int Steps { get; private set; }

        public Movement(string input)
        {
            Validate(input);

            Direction = (Direction) input[0];

            if (input.Length > 1)
                Steps = Convert.ToInt32(input.Substring(1, input.Length - 1));
            else
                Steps = 1;
        }

        /// <summary>
        /// Valida o movimento baseado nas regras de negócio. Vale ressaltar que foram usadas exceções a fim de manter o
        /// projeto simples, porém em uma aplicação real exceções são custosas e faria uso de uma biblioteca de validação
        /// como Flunt ou FluentValidation
        /// </summary>
        private void Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("Input", "não pode ser nulo ou vazio");

            if (!Enum.IsDefined(typeof(Direction), (int)input[0]))
                throw new ArgumentException("Direction", "direção inválida");

            if (input.Length > 1 && !int.TryParse(input.Substring(1, input.Length - 1), out int steps))
                throw new ArgumentException("Steps", "nº de passos inválido");

        }
    }
}
