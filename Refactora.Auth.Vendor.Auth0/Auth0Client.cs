﻿using Newtonsoft.Json;
using Refactora.Auth.Management;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refactora.Auth.Vendor.Auth0
{
	public class Auth0Client : IAuthClient
	{
		private readonly string _clientId;
		private readonly string _clientSecret;
		private readonly string _clientAudience;

		public Auth0Client(string host, string clientId, string clientSecret, string clientAudience)
		{
			Host = !string.IsNullOrEmpty(host) ? host : throw new ArgumentNullException("host");
			_clientId = !string.IsNullOrEmpty(clientId) ? clientId : throw new ArgumentNullException("clientId");
			_clientSecret = !string.IsNullOrEmpty(clientSecret) ? clientSecret : throw new ArgumentNullException("clientSecret");
			_clientAudience = clientAudience;
		}

		public string Host { get; private set; }

		public async Task<string> GetToken()
		{
			using (var client = new HttpClient())
			{
				var url = string.Format("https://{0}", Host);
				client.BaseAddress = new Uri(url);

				var response = await client.PostAsync("/oauth/token", new StringContent(JsonConvert.SerializeObject(new
				{
					grant_type = "client_credentials",
					client_id = _clientId,
					client_secret = _clientSecret,
					audience = _clientAudience
				}), Encoding.UTF8, "application/json"));

				var responseString = await response.Content.ReadAsStringAsync();
				var tokenResponse = JsonConvert.DeserializeObject<Auth0Token>(responseString);

				if (string.IsNullOrEmpty(tokenResponse?.access_token))
				{
					throw new Exception(responseString);
				}

				return tokenResponse.access_token;
			}
		}

		public class Auth0Token
		{
			public string access_token { get; set; }
		}
	}
}
