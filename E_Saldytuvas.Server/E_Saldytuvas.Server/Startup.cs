using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_Saldytuvas.Server.Data;
using Microsoft.EntityFrameworkCore;
using E_Saldytuvas.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Swagger;

namespace E_Saldytuvas.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("e-Saldytuvas", new Info());
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
            });

            // Application services
            services.AddTransient<ICookedMealService, CookedMealService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IMeasureService, MeasureService>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                const string swaggerUrl = "/swagger/e-Saldytuvas/swagger.json";
                app.UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.DocExpansion("none");
                        c.SwaggerEndpoint(swaggerUrl, "e-Saldytuvas");
                    });
            }

            app.UseAuthentication();

            app.UseCors(builder =>
                builder.WithOrigins(
                    "http://localhost:3000"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseMvc();
        }
    }
}
