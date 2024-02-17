using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Text;

namespace Management.API.AppSetting
{
    public class ApplicationConfiguration
    {
        public static void RegisterGlobalException(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(
                 options =>
                 {
                     options.Run(
                     async context =>
                     {
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "application/json";
                         IExceptionHandlerFeature ex = context.Features.Get<IExceptionHandlerFeature>();
                         if (ex != null)
                         {
                             string err = ex.Error.Message;
                             string jsonResult = JsonConvert.SerializeObject(new { StatusCode = (int)HttpStatusCode.InternalServerError, Status = false, Message = ex.Error.Message }, new JsonSerializerSettings
                             {
                                 ContractResolver = new CamelCasePropertyNamesContractResolver()
                             });
                             await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(jsonResult), 0, jsonResult.Length).ConfigureAwait(false);
                         }
                     });
                 });
        }
    }
}
