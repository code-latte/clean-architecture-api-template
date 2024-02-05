# clean-architecture-api-template
Template for quickly starting a .NET Core API project following clean architecture pattern

### Project Setup Guide

Welcome to the Clean Architecture API Template repository from Code Latte! This template provides a quick start for a .NET Core API project following clean architecture principles.

#### Instructions:

1. **Rename Project Files and Solution:**
   - Rename the solution file from `CleanArchitecture.sln` to `YourProject.sln`.
   - Rename the API and classes projects folders from `CleanArchitecture.Api` to `YourProject.Api`, etc.
   - Rename the API and classes project files from `CleanArchitecture.Api\CleanArchitecture.Api.csproj` to `YourProject.Api\YourProject.Api.csproj`, etc.

2. **Update Project References in Solution:**
   - Open the solution in Visual Studio Code.
   - Perform a find and replace operation, replacing all instances of `CleanArchitecture.` with `YourProject.`.

3. **Configure AppSettings:**
   - Navigate to `YourProject.Api\appsettings.json`.
   - Update the example values in the file with the appropriate configurations for your project.

4. **Database Setup:**
   - Create tables in the database for entities `Account` and `AuthorizationToken`.

5. **Database Configuration:**
   - The project is configured to use a SQL Server database. If needed, switch to a different database adapter by installing the corresponding library and updating connection types.

6. **Run the Project:**
   - Execute the project and ensure everything is running smoothly.

#### Notes:
- For database configuration changes, update connection strings in `appsettings.json`.
- Feel free to choose a different database adapter by installing the required library and adjusting connection types.
- If you encounter any issues or have questions, refer to the documentation or contact [Your Contact Information].

Now, you are all set to kick-start your project using this clean architecture template! Happy coding with love and coffee! 🚀❤️☕

