﻿using System.Text.RegularExpressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions
{
    public static class StringExtension
    {
        public static string? ToSnakeCase(this string? str)
        {
            Regex pattern =
                new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");

            return str == null
                ? null
                : string
                    .Join("_", pattern.Matches(str).Cast<Match>().Select(m => m.Value))
                    .ToLower();
        }
    }
}
