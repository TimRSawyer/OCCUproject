The easiest way to run this website is within the Visual Studio IDE. The exercise is a single solution, 'MVCexercise.sln', created with Visual Studio 2022 Community edition, which is freely available from Microsoft.

The current solution configuration assumes access to the localdb instance of SQL Express installed with Visual Studio. If this appproach to run the site is taken, the database can be created by Entity Framework migrations from a console in Visual Studio, and sample data will be populated on the first run of the application.

To create the database this way, open the Package Manager Console from the Visual Studio Tools menu, and select NuGet Package Manager->Package Manager Console. Then run these commands in the console: Add-Migration InitialCreate Update-Database

Alternately, if Visual Studio is not available to run the solution, the database would need to be created and the compiled website installed in IIS. If this approach to running the website is taken, the following steps must occur:

The '..\optionalSQLscripts' folder includes scripts to create the necessary SQL database and populate the data. The run order of the scripts is: MvcExercise_DBCreate.sql MvcExercise_TablesCreateAndPopulate.sql

The 'MvcExercise_DBCreate.sql' script assumes existence of "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLDEV\MSSQL\DATA" for storing the data and log files. Please alter this path, in lines 9 and 11, if another version of MS SQL Server is installed and is the desired filepath.

The solution file "appsetting.json" would also need to be edited so the the "MvcExerciseContext" connection string points to the correct SQL Server instance and database name, with valid credentials.

The compiled code has been published to the directory '..\publish'. Knowledge of setting up the hosting of a site within IIS is assumed out of scope here.
