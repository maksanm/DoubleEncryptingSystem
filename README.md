# Double encrypting client-server application
Solution prepared as a part of recruitment process. The task was to create the client (console app) writing encrypted message to database using `SQL`-query and the server using `Entity Framework` with endpoint returning decrypted message by key. I've used `AES` encryption during saving message to database and `RSA` to encrypt message id and `AES` key to be processed by the server.

## Update 15.03.2022
Implemented working solution.
### Used libraries (beyond the obvious, e.g. `EF Core`):
- `RestSharp` for HTTP requests
- `Polly` for retrying them on fail
- `System.Security.Cryptography` for RSA and AES encryption
- `System.Data.SqlClient` for direct SQL queries

### TO DO:
- ~~Extract some variables to config files~~
- Connect repo to GitHub API
