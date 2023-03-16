# Double encrypting client-server application
Solution prepared as a part of recruitment process. The task was to create the client (console app) writing encrypted message to database using `SQL`-query and the server using `Entity Framework` with endpoint returning decrypted message by key. I've used `AES` encryption during saving message to database and `RSA` to encrypt message id, password (which is used for `AES` key derivation with salt) and `AES` initialization vetor to be processed by the server.

## Used libraries (beyond the obvious, e.g. `EF Core`):
- `RestSharp` for HTTP requests
- `Polly` for retrying them on fail and exiting application after last retry
- `Octokit` for GitHub REST API calls
- `System.Security.Cryptography` for RSA and AES encryption
- `System.Data.SqlClient` for direct SQL queries

## TO DO:
- Tests. Such projects always look kinda "mini-Waterfall", so I was focused on the fastest possible implementation and didn't use TDD.

## Running
To run application you need to build `Decoder.Api` and `Encoder.Client` projects and execute **.exe** files created in these project binaries (**/bin** folders). You can run multiple encoder applications at the same time. Also you can just set multiple startup projects using Visual Studio.
