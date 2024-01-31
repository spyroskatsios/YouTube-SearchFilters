using System.Net;
using EntityFramework.Exceptions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Customers.Common;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var message = "Something went wrong.";
        
        // if (exception is DbUpdateException)
        // {
        //     message = GetSqlMessage((exception.GetBaseException() as SqlException)!);
        // }
        
        if (exception is DbUpdateException)
        {
            message = GetSqlMessage(exception);
        }
        
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(message);
    }
    
    private static string GetSqlMessage(Exception exception)
    {
        return exception switch
        {
            MaxLengthExceededException _=> "The maximum length of the field has been exceeded.",
            UniqueConstraintException _ => "A record with the same key already exists.",
            _ => "Something went wrong."
        };
    }

    // private static string GetSqlMessage(SqlException sqlException)
    // {
    //     return sqlException.Number switch
    //     {
    //         2601 => "A record with the same key already exists.",
    //         547 => "The record cannot be deleted because it is referenced by other records.",
    //         _ => "Something went wrong."
    //     };
    // }
}