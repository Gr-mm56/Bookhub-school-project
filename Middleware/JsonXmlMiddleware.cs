using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Middleware;

public class JsonXmlMiddleware
{
    private readonly RequestDelegate _next;

    public JsonXmlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.Accept.ToString().Contains("application/xml"))
        {
            await HandleXmlResponse(context);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleXmlResponse(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        await _next(context);
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body = originalBodyStream;
        if (context.Response.StatusCode == (int)HttpStatusCode.OK)
        {
            var xmlResponse = ConvertJsonToXml(responseJson);
            if (xmlResponse != responseJson)
            {
                context.Response.ContentType = "application/xml";
                var xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlResponse);
                await context.Response.Body.WriteAsync(xmlBytes, 0, xmlBytes.Length);
                return;
            }
        }

        var jsonBytes = System.Text.Encoding.UTF8.GetBytes(responseJson);
        context.Response.ContentType = "application/json";
        await context.Response.Body.WriteAsync(jsonBytes, 0, jsonBytes.Length);
    }

    private string ConvertJsonToXml(string responseJson)
    {
        var xmlDocument = JsonConvert.DeserializeXmlNode(responseJson, "root");
        return xmlDocument?.OuterXml ?? responseJson;
    }
}