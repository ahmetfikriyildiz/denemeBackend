using System.Text;
using Microsoft.IO;

namespace denemeBackend.Middleware;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private readonly RecyclableMemoryStreamManager _streamManager;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _streamManager = new RecyclableMemoryStreamManager();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the request
        var request = await LogRequest(context.Request);
        _logger.LogInformation("HTTP {RequestMethod} {RequestPath} {RequestBody}", 
            context.Request.Method, 
            context.Request.Path, 
            request);

        // Copy the original response body stream
        var originalBodyStream = context.Response.Body;
        using var responseBody = _streamManager.GetStream();
        context.Response.Body = responseBody;

        // Continue down the middleware pipeline
        await _next(context);

        // Log the response
        var response = await LogResponse(context.Response);
        _logger.LogInformation("HTTP {StatusCode} {ResponseBody}", 
            context.Response.StatusCode, 
            response);

        // Copy the response back to the original stream
        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task<string> LogRequest(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(
            request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);

        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private async Task<string> LogResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return text;
    }
} 