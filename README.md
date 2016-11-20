# Override startup host configuration for Server listening url's and environment using environment variables

### Create environment variables for machine and user ([Using PowerShell](https://technet.microsoft.com/en-us/library/ff730964.aspx)) :

| *Name*                          | *Value*               |
| ------------------------------- |:---------------------:|
| ASPNETCORE_ENVIRONMENT          | TestEnv               |
| SERVER.URLS                     | http://localhost:5550 |
| MYVALUES_ASPNETCORE_ENVIRONMENT | MyEnvVarEnvironment   |
| MYVALUES_SERVER.URLS            | http://localhost:5400 |
</br>

>**Please click on images** to use links for navigating to code.

**Green boxes** are showing result of the path where both default configuration's for server listening port and environment are overridden by values defined in environment variables.

---
Using env. Variables without _MYVALUES alias:

![alt text](https://cdn.rawgit.com/AMatijevic/AspNetCore1.0_WorkOut/SetupServerUrlsAndDefaultEnvironment/CustomHostSetup/src/ServerUrlsAndEnvironment/Charts/Chart1.svg)

---
Using env. Variables with _MYVALUES alias:

![alt text](https://cdn.rawgit.com/AMatijevic/AspNetCore1.0_WorkOut/SetupServerUrlsAndDefaultEnvironment/CustomHostSetup/src/ServerUrlsAndEnvironment/Charts/Chart2.svg)

---
Some additional links that help me to create charts and code:

* [How to set the hosting environment in ASP.NET Core](http://andrewlock.net/how-to-set-the-hosting-environment-in-asp-net-core/)
* [How to configure urls for Kestrel, WebListener and IIS express in ASP.NET Core](http://andrewlock.net/configuring-urls-with-kestrel-iis-and-iis-express-with-asp-net-core/)
* [ASP.NET Core documentation - Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)
* [ASP.NET Core documentation - Working with Multiple Environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)
