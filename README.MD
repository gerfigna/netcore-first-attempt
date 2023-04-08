## Tests


> `$> dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura`

> `$> reportgenerator -reports:"tests/coverage.cobertura.xml" -targetdir:"coveragereport"`


## Try it

> `$> curl --location 'https://localhost:7156/Trial?plaintiff=KN&defendant=NV' `

> `$> dotnet run --project cli KKK NVV`

