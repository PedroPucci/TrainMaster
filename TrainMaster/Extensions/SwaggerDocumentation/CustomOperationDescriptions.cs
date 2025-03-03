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
                    { "pessoalProfile", () => HandlePessoalProfileOperations(operation, context) },
                    { "loginSystem", () => HandleLoginOperations(operation, context) },
                    { "educationLevel", () => HandleEducationLevelOperations(operation, context) },
                    { "professionalProfile", () => HandleProfessionalProfileOperations(operation, context) },
                    { "address", () => HandleAddressOperations(operation, context) }
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

        private void HandleUserOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new User";
                operation.Description = "This endpoint allows you to create a new User by providing the necessary details.";
                AddResponses(operation, "200", "The User was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing User";
                operation.Description = "This endpoint allows you to update an existing User by providing the necessary details.";
                AddResponses(operation, "200", "The User was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing User";
                operation.Description = "This endpoint allows you to delete an existing User by providing the ID.";
                AddResponses(operation, "200", "The User was successfully deleted.");
                AddResponses(operation, "404", "User not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                if (path.Contains("allactives"))
                {
                    operation.Summary = "Retrieve all active Users";
                    operation.Description = "This endpoint returns all Users where IsActive is true.";
                    AddResponses(operation, "200", "All active Users were successfully retrieved.");
                }
                else if (path.Contains("all"))
                {
                    operation.Summary = "Retrieve all Users";
                    operation.Description = "This endpoint allows you to retrieve details of all existing Users.";
                    AddResponses(operation, "200", "All User details were successfully retrieved.");
                }
            }
        }

        private void HandlePessoalProfileOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "POST")
            {
                operation.Summary = "Create a new Pessoal Profile";
                operation.Description = "This endpoint allows you to create a new Pessoal Profile by providing the necessary details.";
                AddResponses(operation, "200", "The Pessoal Profile was successfully created.");
            }
            else if (context.ApiDescription.HttpMethod == "PUT")
            {
                operation.Summary = "Update an existing Pessoal Profile";
                operation.Description = "This endpoint allows you to update an existing Pessoal Profile by providing the necessary details.";
                AddResponses(operation, "200", "The Pessoal Profile was successfully updated.");
            }
            else if (context.ApiDescription.HttpMethod == "DELETE")
            {
                operation.Summary = "Delete an existing Pessoal Profile";
                operation.Description = "This endpoint allows you to delete an existing Pessoal Profile by providing the ID.";
                AddResponses(operation, "200", "The Pessoal Profile was successfully deleted.");
                AddResponses(operation, "404", "The Pessoal Profile not found. Please verify the ID.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                    context.ApiDescription.RelativePath != null &&
                    context.ApiDescription.RelativePath.Contains("All"))
            {
                operation.Summary = "Retrieve all Pessoal Profiles";
                operation.Description = "This endpoint allows you to retrieve details of all existing Pessoal Profiles.";
                AddResponses(operation, "200", "All Pessoal Profiles details were successfully retrieved.");
            }
        }

        private void HandleLoginOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                if (path.Contains("login"))
                {
                    operation.Summary = "User Login";
                    operation.Description = "Authenticates a User and returns a JWT token.";
                    AddResponses(operation, "200", "User successfully logged in.");
                    AddResponses(operation, "400", "Invalid request body.");
                    AddResponses(operation, "401", "Invalid email or password.");
                }
            }
        }

        private void HandleEducationLevelOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new Education Level";
                operation.Description = "This endpoint allows you to create a new Education Level by providing the necessary details.";
                AddResponses(operation, "200", "The Education Level was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing Education Level";
                operation.Description = "This endpoint allows you to update an existing Education Level by providing the necessary details.";
                AddResponses(operation, "200", "The Education Level was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing Education Level";
                operation.Description = "This endpoint allows you to delete an existing Education Level by providing the ID.";
                AddResponses(operation, "200", "The Education Level was successfully deleted.");
                AddResponses(operation, "404", "Education Level not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                operation.Summary = "Retrieve all Education Levels";
                operation.Description = "This endpoint allows you to retrieve details of all existing Education Levels.";
                AddResponses(operation, "200", "All Education Levels details were successfully retrieved.");
            }
        }

        private void HandleProfessionalProfileOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new Professional Profile";
                operation.Description = "This endpoint allows you to create a new Professional Profile by providing the necessary details.";
                AddResponses(operation, "200", "The Professional Profile was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing Professional Profile";
                operation.Description = "This endpoint allows you to update an existing Professional Profile by providing the necessary details.";
                AddResponses(operation, "200", "The Professional Profile was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing Professional Profile";
                operation.Description = "This endpoint allows you to delete an existing Professional Profile by providing the ID.";
                AddResponses(operation, "200", "The Professional Profile was successfully deleted.");
                AddResponses(operation, "404", "Professional Profile not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                operation.Summary = "Retrieve all Professional Profiles";
                operation.Description = "This endpoint allows you to retrieve details of all existing Professional Profiles.";
                AddResponses(operation, "200", "All Professional Profiles details were successfully retrieved.");
            }
        }

        private void HandleAddressOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new Address";
                operation.Description = "This endpoint allows you to create a new Address by providing the necessary details.";
                AddResponses(operation, "200", "The Address was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing Address";
                operation.Description = "This endpoint allows you to update an existing Address by providing the necessary details.";
                AddResponses(operation, "200", "The Address was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing Address";
                operation.Description = "This endpoint allows you to delete an existing Address by providing the ID.";
                AddResponses(operation, "200", "The Address was successfully deleted.");
                AddResponses(operation, "404", "Address not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                if (path.Contains("postalCode", StringComparison.OrdinalIgnoreCase))
                {
                    operation.Summary = "Retrieve Addresses";
                    operation.Description = "This endpoint returns Addresses.";
                    AddResponses(operation, "200", "Addresses were successfully retrieved.");
                }
                else if (path.Contains("all"))
                {
                    operation.Summary = "Retrieve all Addresses";
                    operation.Description = "This endpoint allows you to retrieve details of all existing Addresses.";
                    AddResponses(operation, "200", "All Addresses details were successfully retrieved.");
                }
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
