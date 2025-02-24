using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrainMaster.Extensions.SwaggerDocumentation
{
    public class CustomOperationDescriptions : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.ApiDescription?.HttpMethod is null || context.ApiDescription.RelativePath is null)
                return;

            var routeHandlers = new Dictionary<string, Action>
                {
                    { "user", () => HandleUserOperations(operation, context) },
                    { "pessoalProfile", () => HandlePessoalProfileOperations(operation, context) }
                };

            foreach (var routeHandler in routeHandlers)
            {
                if (context.ApiDescription.RelativePath.Contains(routeHandler.Key))
                {
                    routeHandler.Value.Invoke();
                    break;
                }
            }
        }

        //private void HandleUserOperations(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    if (context.ApiDescription.HttpMethod == "POST")
        //    {
        //        operation.Summary = "Create a new user";
        //        operation.Description = "This endpoint allows you to create a new user by providing the necessary details.";
        //        AddResponses(operation, "200", "The user was successfully created.");
        //    }
        //    else if (context.ApiDescription.HttpMethod == "PUT")
        //    {
        //        operation.Summary = "Update an existing user";
        //        operation.Description = "This endpoint allows you to update an existing user by providing the necessary details.";
        //        AddResponses(operation, "200", "The user was successfully updated.");
        //    }
        //    else if (context.ApiDescription.HttpMethod == "DELETE")
        //    {
        //        operation.Summary = "Delete an existing user";
        //        operation.Description = "This endpoint allows you to delete an existing user by providing the ID.";
        //        AddResponses(operation, "200", "The user was successfully deleted.");
        //        AddResponses(operation, "404", "user not found. Please verify the ID.");
        //    }
        //    else if (context.ApiDescription.HttpMethod == "GET" &&
        //            context.ApiDescription.RelativePath != null &&
        //            context.ApiDescription.RelativePath.Contains("All"))
        //    {
        //        operation.Summary = "Retrieve all users";
        //        operation.Description = "This endpoint allows you to retrieve details of all existing users.";
        //        AddResponses(operation, "200", "All user details were successfully retrieved.");
        //    }
        //    else if (context.ApiDescription.HttpMethod == "GET" &&
        //            context.ApiDescription.RelativePath != null &&
        //            context.ApiDescription.RelativePath.Contains("AllActives"))
        //    {
        //        operation.Summary = "Retrieve all users actives";
        //        operation.Description = "This endpoint allows you to retrieve details of all existing users.";
        //        AddResponses(operation, "200", "All user details were successfully retrieved.");
        //    }
        //}

        private void HandleUserOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty; // 🔥 Ignora case-sensitive

            if (method == "POST")
            {
                operation.Summary = "Create a new user";
                operation.Description = "This endpoint allows you to create a new user by providing the necessary details.";
                AddResponses(operation, "200", "The user was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing user";
                operation.Description = "This endpoint allows you to update an existing user by providing the necessary details.";
                AddResponses(operation, "200", "The user was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing user";
                operation.Description = "This endpoint allows you to delete an existing user by providing the ID.";
                AddResponses(operation, "200", "The user was successfully deleted.");
                AddResponses(operation, "404", "User not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                if (path.Contains("allactives"))
                {
                    operation.Summary = "Retrieve all active users";
                    operation.Description = "This endpoint returns all users where IsActive is true.";
                    AddResponses(operation, "200", "All active users were successfully retrieved.");
                }
                else if (path.Contains("all"))
                {
                    operation.Summary = "Retrieve all users";
                    operation.Description = "This endpoint allows you to retrieve details of all existing users.";
                    AddResponses(operation, "200", "All user details were successfully retrieved.");
                }
            }
        }


        private void HandlePessoalProfileOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "POST")
            {
                operation.Summary = "Create a new pessoal profile";
                operation.Description = "This endpoint allows you to create a new pessoal profile by providing the necessary details.";
                AddResponses(operation, "200", "The pessoal profile was successfully created.");
            }
            else if (context.ApiDescription.HttpMethod == "PUT")
            {
                operation.Summary = "Update an existing pessoal profile";
                operation.Description = "This endpoint allows you to update an existing pessoal profile by providing the necessary details.";
                AddResponses(operation, "200", "The pessoal profile was successfully updated.");
            }
            else if (context.ApiDescription.HttpMethod == "DELETE")
            {
                operation.Summary = "Delete an existing pessoal profile";
                operation.Description = "This endpoint allows you to delete an existing pessoal profile by providing the ID.";
                AddResponses(operation, "200", "The pessoal profile was successfully deleted.");
                AddResponses(operation, "404", "pessoal profile not found. Please verify the ID.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                    context.ApiDescription.RelativePath != null &&
                    context.ApiDescription.RelativePath.Contains("All"))
            {
                operation.Summary = "Retrieve all pessoal profiles";
                operation.Description = "This endpoint allows you to retrieve details of all existing pessoal profiles.";
                AddResponses(operation, "200", "All pessoal profiles details were successfully retrieved.");
            }
        }

        private void AddResponses(OpenApiOperation operation, string statusCode, string description)
        {
            if (!operation.Responses.ContainsKey(statusCode))
            {
                operation.Responses.Add(statusCode, new OpenApiResponse { Description = description });
            }
        }
    }
}
