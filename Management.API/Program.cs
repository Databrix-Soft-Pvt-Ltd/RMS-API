using Management.API.AppSetting;
using Management.API.Miscellaneous;
using Management.Core.AutoMapperConfi;
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwoWayCommunication.Model.Enums;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//Arvind
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string value = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<RMS_2024Context>(options => options.UseSqlServer(value));
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
//services cors
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});



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
builder.Services.AddScoped<IRetrivel_RequestDomain, Retrivel_RequestDomain>();

builder.Services.AddScoped<IRefilling_RequestDomain, Refilling_RequestDomain>();
builder.Services.AddScoped<IReportsDomain, ReportsDomain>();
builder.Services.AddScoped<IBranchInwardDomain, BranchInwardDomain>();
builder.Services.AddScoped<ICheckListMasterDomain, CheckListMasterDomain>();
#endregion-------------------------------------



builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
 
builder.Services.AddHttpContextAccessor();

 
builder.Services.AddScoped<GlobalUserID>();

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


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



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
    app.UseCors(devCorsPolicy);
}

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");
ApplicationConfiguration.RegisterGlobalException(app);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/New_RetrievaAPI/swagger/v1/swagger.json", "API v1");
    app.UseCors(devCorsPolicy);
});
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
