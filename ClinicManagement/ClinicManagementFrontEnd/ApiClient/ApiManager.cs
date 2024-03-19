﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.ApiClient
{
    internal class ApiManager
    {
        public static string PostAsync(string inputJson, string apiMethod)
        {
            string requestResult = string.Empty;
            var client = new HttpClient();
            try
            {
                var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                client.BaseAddress = new Uri(config.GetSection("ApiLinks").GetValue<string>("BaseAddress"));
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.Timeout = TimeSpan.FromMinutes(20);
                HttpResponseMessage responses = client.PostAsync(apiMethod, new StringContent(inputJson.ToString(), Encoding.UTF8, "application/json")).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (responses.IsSuccessStatusCode)
                {
                    return responses.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    return requestResult;
                }
               
            }
            catch (Exception ex)
            {
                Classes.Logger.write("Post Async", ex.ToString());
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }
        public static string GetAsync(string apiMethod)
        {
            string requestResult = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                client.BaseAddress = new Uri(config.GetSection("ApiLinks").GetValue<string>("BaseAddress"));

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    client.Timeout = TimeSpan.FromMinutes(20);
                    // List data response.
                    HttpResponseMessage responses = client.GetAsync(apiMethod).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                    if (responses.IsSuccessStatusCode)
                    {
                        return responses.Content.ReadAsStringAsync().Result;

                    }
                    else
                    {
                        return requestResult;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Classes.Logger.write("Get Async", ex.ToString());
                return null;
            }
        }

    }
}
