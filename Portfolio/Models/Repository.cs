namespace Portfolio.Models
{
    public class Repository
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string GithubUrl { get; set; }

        public string HomepageUrl { get; set; }

        public RepositoryLanguage[] Languages { get; set; }
    }

    public class RepositoryLanguage
    {
        public string Name { get; set; }

        public string Color { get; set; }
    }
}