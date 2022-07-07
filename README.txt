MoviesAPI

To be able to utilize the API, one must first get an API key from TheMoviesDB API: https://developers.themoviedb.org/3/getting-started/authentication

Then, a user-secret must be created, associating the index "TheMoviesDB:APIKey" with the value of the API key
To make this in a terminal utilizing the dotnet command, would be:

> dotnet user-secrets set "TheMoviesDB:APIKey" {API Key}