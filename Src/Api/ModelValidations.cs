﻿using Microsoft.AspNetCore.Mvc;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Api
{
    public static class ModelValidations
    {
        internal static void ConfigureModelValidations(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
