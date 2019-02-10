using eimprovement.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eimprovement.WebApplication.Utils
{
    public static class ApiMethod
    {
        /// <summary>
        /// Represents the content of a request.
        /// </summary>
        public class Request
        {
            public string BaseUrl { get; set; }
            public string ActionUrl { get; set; }
            public RequestHeaders Headers { get; set; }
            public string RequestBody { get; set; }
            public string Params { get; set; }
            public HttpAttributes Type { get; set; }


            public Request()
            {
                //Assign API base url for the main endpoint.
                BaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
                Headers = new RequestHeaders();
            }
        }

        /// <summary>
        /// Represents the headers of the request.
        /// </summary>
        public class RequestHeaders
        {
            public string Content_Type { get; set; }
            public AuthorizationHeader Authorization { get; set; }

            public RequestHeaders()
            {
                Authorization = new AuthorizationHeader();

                //Assign request content type for requests.
                Content_Type = ConfigurationManager.AppSettings["RequestContentType"];
            }
        }

        /// <summary>
        /// Represents the Authorization Request Header
        /// </summary>
        public class AuthorizationHeader
        {
            public string Key { get; set; }

            public AuthorizationHeader()
            {
                //Assign API key at constructor so every request would use the same one.
                Key = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"];
            }
        }

        /// <summary>
        /// Generic request used for get and delete request. 
        /// </summary>
        /// <param name="request">Request previously created.</param>
        /// <returns>ApiResponse model with information about the request.</returns>
        public static async Task<ApiResponse> SendGetDeleteRequest(Request request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //Clear default headers
                    client.DefaultRequestHeaders.Clear();

                    //Add Authorization Header
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", request.Headers.Authorization.Key);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.Headers.Content_Type));

                    HttpResponseMessage response = new HttpResponseMessage();

                    //Make the HTTP request
                    if (request.Type == HttpAttributes.Get)
                    {
                        response = await client.GetAsync(request.BaseUrl + request.ActionUrl + request.Params);
                    }
                    else if (request.Type == HttpAttributes.Delete)         
                    {
                        response = await client.DeleteAsync(request.BaseUrl + request.ActionUrl + request.Params);
                    }

                    // -> Response 

                    //Checking the response is successful or not which is sent using HttpClient  
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.OK,
                                Message = response.RequestMessage.ToString(),
                                Content = response.Content.ReadAsStringAsync().Result
                            };
                        case HttpStatusCode.NotFound:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.NotFound,
                                Message = response.RequestMessage.ToString()
                            };
                        default:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = response.RequestMessage.ToString()
                            };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Generic request used for post and put request. 
        /// </summary>
        /// <param name="request">Request previously created.</param>
        /// <returns>ApiResponse model with information about the request.</returns>
        public static async Task<ApiResponse> SendPostPutRequest(Request request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(request.BaseUrl);

                    //Clear default headers
                    client.DefaultRequestHeaders.Clear();

                    //Define the Content-Type to be accepted
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.Headers.Content_Type));

                    //Add Authorization Header
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", request.Headers.Authorization.Key);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.Headers.Content_Type));

                    // Construct an HttpContent from the parameters
                    HttpContent httpContent = new StringContent(request.RequestBody, Encoding.UTF8, request.Headers.Content_Type);

                    HttpResponseMessage response = new HttpResponseMessage();

                    //Make the HTTP request
                    if (request.Type == HttpAttributes.Post)
                    {
                        response = await client.PostAsync(request.BaseUrl + request.ActionUrl, httpContent);
                    }
                    else if (request.Type == HttpAttributes.Put)
                    {
                        response = await client.PutAsync(request.BaseUrl + request.ActionUrl, httpContent);
                    }

                    // -> Response 

                    //Checking the response is successful or not which is sent using HttpClient  
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.OK,
                                Message = response.RequestMessage.ToString(),
                                Content = response.Content.ReadAsStringAsync().Result
                            };
                        case HttpStatusCode.NotFound:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.NotFound,
                                Message = response.RequestMessage.ToString()
                            };
                        case HttpStatusCode.MethodNotAllowed:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.MethodNotAllowed,
                                Message = response.RequestMessage.ToString()
                            };
                        default:
                            return new ApiResponse()
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = response.RequestMessage.ToString()
                            };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}