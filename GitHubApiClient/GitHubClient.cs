﻿using GitHubApiClient.Interfaces;
using GitHubApiClient.Models;
using Refit;
using System.Text;

namespace GitHubApiClient
{
	public class GitHubClient
	{
		private readonly string _userName;
		private readonly string _token;
		private readonly IGitHubClient _api;

		public GitHubClient(string userName, string token, HttpClient httpClient)
		{
			_userName = userName;
			_token = token;
			_api = RestService.For<IGitHubClient>(httpClient, GetSettings());
		}

		public GitHubClient(string userName, string token)
		{
			_userName = userName;
			_token = token;
			_api = RestService.For<IGitHubClient>("https://api.github.com", GetSettings());
		}

		private RefitSettings GetSettings() => new RefitSettings()
		{			
			AuthorizationHeaderValueGetter = async () =>
			{
				var bytes = Encoding.UTF8.GetBytes($"{_userName}:{_token}");
				var encoded = Convert.ToBase64String(bytes);
				return await Task.FromResult(encoded);
			}
		};

		public async Task<IReadOnlyCollection<Issue>> GetIssuesAsync(string repositoryName, int page = 1) => await _api.GetIssuesAsync(_userName, repositoryName, page);		
	}
}