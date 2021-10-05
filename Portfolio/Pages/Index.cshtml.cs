using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using System;
using System.Net.Http;
using System.Text.Json;
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

        public void OnGet()
        {



        }

        public async Task FetchGithubUser()
        {
            string URL = "https://gcjx2rjj1j.execute-api.eu-central-1.amazonaws.com/default/fetchGithubUser";

            try
            {

                HttpResponseMessage response = await Client.GetAsync(URL);

                var contents = await response.Content.ReadAsStringAsync();



                Person deserialised = JsonSerializer.Deserialize<Person>(contents);





                Profile = new Person
                {
                    FullName = deserialised.FullName,
                    Description = deserialised.Description,
                    ProfileImage = deserialised.ProfileImage,
                    Company = deserialised.Company,
                    Location = deserialised.Location,
                    GithubUserName = deserialised.GithubUserName,
                    Repositories = deserialised.Repositories,

                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
