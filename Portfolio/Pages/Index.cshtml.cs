using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class IndexModel : PageModel
    {

        private static readonly HttpClient Client = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        public Person Profile = new Person();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task FetchGithubGraplQl()
        {
            // Remember to insert api key here!
            string API_KEY = "";
            var graphQLClient = new GraphQLHttpClient("https://api.github.com/graphql", new NewtonsoftJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            var githubRequest = new GraphQL.GraphQLRequest
            {
                Query = @"
                    query GET_PROJECTS {
                        user(login: ""hougesen"") {
                            name
                            login
                            bio
                            avatarUrl
                            company
                            location
                            twitterUsername
                            pinnedItems(first: 6) {
                                nodes {
                                    ... on Repository {
                                        name
                                        description
                                        homepageUrl
                                        url
                                        languages(first: 3, orderBy: {field: SIZE, direction: DESC}) {
                                            nodes {
                                                name
                                                color
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                "
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<GithubRequest>(githubRequest);

            // Parse data 
            List<Repository> repositories = new List<Repository>();

            foreach (PinnedRepository repository in graphQLResponse.Data.user.pinnedItems.nodes)
            {
                List<RepositoryLanguage> Languages = new List<RepositoryLanguage>();

                foreach (PinnedRepositoryLanguage language in repository.languages.nodes)
                {
                    RepositoryLanguage lang = new RepositoryLanguage
                    {
                        Name = language.name,
                        Color = language.color
                    };

                    Languages.Add(lang);

                }

                Repository repo = new Repository
                {
                    Name = repository.name,
                    Description = repository.description,
                    GithubUrl = repository.url,
                    HomepageUrl = repository.homepageUrl,
                    Languages = Languages
                };

                repositories.Add(repo);
            }

            Profile = new Person
            {
                FullName = graphQLResponse.Data.user.name,
                Description = graphQLResponse.Data.user.bio,
                ProfileImage = graphQLResponse.Data.user.avatarUrl,
                Company = graphQLResponse.Data.user.company,
                Location = graphQLResponse.Data.user.location,
                GithubUserName = graphQLResponse.Data.user.login,
                TwitterUserName = graphQLResponse.Data.user.twitterUsername,
                Repositories = repositories,
            };
        }
    }
}
