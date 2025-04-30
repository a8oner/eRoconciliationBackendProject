
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

//eReconciliationDbdeneme Database !!!!!!!   fpoject

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			var builder = WebApplication.CreateBuilder(args);
			//Aþaðýdaki iki satýrda AutofacBusinessModule'ý WebAPI'ye tanýttýk
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//1
            builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule())); //2 Autofac ayarý burda tanýmlandý!!!!!!

			// Add services to the container.
			/*
             CONFIGURATÝON VE TokenOptions ayarlarýný  buraya ekliyoruz!!!!

            builder.Services.AddDbContext<ContextDb>(options => //veri taban? ba?lant?s?
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
             */
			IConfiguration configuration = builder.Configuration;//3 genel olarak proje ayarlarý

			builder.Services.AddControllers();//Program.cs'de varolan yapýydi

			builder.Services.AddCors(options =>//4 (güvenliði saðlamak için)
			{
                options.AddPolicy("AllowOrigin",
                builder => builder.WithOrigins("https://localhost:7174"));
            });
            var tokenOptions =configuration.GetSection("TokenOptions").Get<TokenOptions>();//5

            //6
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience=true,
                    ValidateLifetime =false,//true olursa expiration süresi dolduðunda token'i düþer.
                    ValidIssuer=tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
  };
            });

            builder.Services.AddDependencyResolvers(new ICoreModule[]//6
            {
                new CoreModule(),
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
			//CONFIGURATÝON VE TokenOptions ayarlarýnýn devamý::: Cors ekleyeceðiz
			app.UseCors(builder => builder.WithOrigins("https://localhost:7174").AllowAnyHeader());//AllowAnyHeader ile bu adresten gelen tüm isteklier karþýla
			app.UseHttpsRedirection();
		
            //UseAuthentication giriþ iþlemi yapar aþaðýdaki ise giriþ iþlemini yaptýktan sonra yetkilendirmeden sorumlu
			app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}