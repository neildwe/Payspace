using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment
{
    public static partial class MvcExtensions
    {
        public static IMvcBuilder AddCalculator(this IMvcBuilder sender, IConfiguration Configuration, IServiceCollection services)
        {
            sender.Services.AddDbContextPool<Payspace.Assesment.Repository.CalculatorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), option => option.EnableRetryOnFailure()));
            sender.AddApplicationPart(typeof(Controller.CalculatorController).Assembly);
            return sender;
        }
    }
}
