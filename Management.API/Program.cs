using Management.API.AppSetting;
using Management.API.Miscellaneous;
using Management.Model.RMSEntity;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Text;
using TwoWayCommunication.Core.Repository;
using TwoWayCommunication.Core.UnitOfWork; 
using static Management.Services.Masters.UserDomain;

var builder = WebApplication.CreateBuilder(args);
//Arvind
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string value = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<RMS_2024Context>(options => options.UseSqlServer(value)); 


#region Project Interfaces........................ 

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationDomain, AuthenticationDomain>();
builder.Services.AddScoped<IExceptionHandling, ExceptionHandling>();
builder.Services.AddScoped<IRoleDomain, RoleDomain>();
builder.Services.AddScoped<IRoleMapDomain, RoleMappingDomain>();
builder.Services.AddScoped<IBranchMappingDomain, BranchMappingDomin>();
builder.Services.AddScoped<IClientMappingDomain, ClientMappingDomain>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IBranchMasterDomain, BranchMasterDomain>();
builder.Services.AddScoped<IClientMasterDomain, ClientMasterDomain>();
builder.Services.AddScoped<ICourierMasterDomain, CourierMasterDomain>();
builder.Services.AddScoped<IProjectMasterDomain, ProjectMasterDomain>();
builder.Services.AddScoped<IPickListDomain, PickListMasterDomain>();
builder.Services.AddScoped<IMenuMasterDomain, MenuMasterDomain>();
builder.Services.AddScoped<ITemplateMasterDomain, TemplateMasterDomain>();
builder.Services.AddScoped<IValidateTokenExtension, ValidateTokenExtension>();
builder.Services.AddScoped<IDumpUploadDomain, DumpUploadDomain>();


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
                  //   RequireExpirationTime
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
              };
          });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

// Configure the HTTP request pipeline.
 
app.UseCors("CorsPolicy");
ApplicationConfiguration.RegisterGlobalException(app);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name");
});
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
