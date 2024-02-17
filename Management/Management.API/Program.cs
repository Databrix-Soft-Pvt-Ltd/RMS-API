using Management.API;
using Management.API.AppSetting;
using Management.Model.RMSEntity;
using Management.Services.ClientMasterDomain;
using Management.Services.CourierMasterDomain;
using Management.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
using TwoWayCommunication.Core.Repository;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Domain.Authentication;
using TwoWayCommunication.Model.DBModels;
using static Management.Services.User.UserDomain;

var builder = WebApplication.CreateBuilder(args);
//Arvind
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string value = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<RMS_2024Context>(options => options.UseSqlServer(value));
builder.Services.AddDbContext<TwoWayCommunicationDbContext>(options => options.UseSqlServer(value));


#region Project Interfaces........................ 

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationDomain, AuthenticationDomain>();
builder.Services.AddScoped<IExceptionHandling, ExceptionHandling>();
builder.Services.AddScoped<IRoleDomain, RoleDomain>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IBranchMasterDomain, BranchMasterDomain>();
builder.Services.AddScoped<IClientMasterDomain, ClientMasterDomain>();
builder.Services.AddScoped<ICourierMasterDomain, CourierMasterDomain>();
builder.Services.AddScoped<IValidateTokenExtension, ValidateTokenExtension>();

#endregion-------------------------------------


//builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = builder.Configuration["Jwt:Issuer"],
                  ValidAudience = builder.Configuration["Jwt:Issuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
              };
          });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
ApplicationConfiguration.RegisterGlobalException(app);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "Management API");
});
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
