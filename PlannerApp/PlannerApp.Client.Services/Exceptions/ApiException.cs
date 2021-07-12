using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services.Exceptions
{
    public class ApiException : Exception
    {
        public ApiErrorResponse apiErrorResponse { get; set; }

        public HttpStatusCode httpStatus { get; set; }

        public ApiException(ApiErrorResponse ErrorResponse, HttpStatusCode http)
        {
            httpStatus = http;
        }

        public ApiException(ApiErrorResponse ErrorResponse)
        {
            apiErrorResponse = ErrorResponse;
        }
    }
}
