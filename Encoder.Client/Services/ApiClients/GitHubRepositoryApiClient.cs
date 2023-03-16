using Octokit;
using System;
using System.Threading.Tasks;

namespace Encryptor.Client.Services.ApiClients
{
    internal class GitHubRepositoryApiClient : ApiClientBase
    {
        private readonly Repository _repository;

        public GitHubRepositoryApiClient(string owner, string repository)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("EncoderClientGitHubAPI"));
            _repository = githubClient.Repository.Get(owner, repository).Result;
        }

        public string GetRepositoryDescription() => _repository.Description;

        public DateTime GetRepositoryCreationDate() => _repository.CreatedAt.LocalDateTime;

        public string GetRepositoryUrl() => _repository.Url;

        public string GetRepositoryMainLanguage() => _repository.Language;
    }
}
