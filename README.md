# SmileDirectClubChallenge for Isaac Zimmerman

## Technologies

Api is written using .net core 2 in C#
Standard 3-tiered architecture: Controllers, Business Layer, Services
Third-party libaries: AutoMapper, Newtonsoft, Moq

## Migrating from the SpaceXApi to an internal database

All the code involving the SpaceXApi with the exception of the app setting configuration and dependency injection is contained in the SmileDirectClub.Services projects.
To swap out the api for a database, implement a database-backed ILaunchPadService and remap the dependency injection in Startup.ConfigureServices()

## A few other notes

Non-success responses from the api are done via an exception flow and global exception handling - see comments in code
SpaceXApi uris are configured in app settings
Logging is just done to the output console
There is some unit test coverage, but it is in no way complete coverage
