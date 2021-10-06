using System.Collections.Generic;

namespace Portfolio.Models
{
    public class GithubRequest
    {
        public User user { get; set; }
    }

    public class User
    {
        public string name { get; set; }

        public string login { get; set; }

        public string bio { get; set; }

        public string avatarUrl { get; set; }

        public string company { get; set; }

        public string location { get; set; }

        public string twitterUsername { get; set; }

        public PinnedRepositories pinnedItems { get; set; }
    }

    public class PinnedRepositories
    {
        public List<PinnedRepository> nodes { get; set; }
    }

    public class PinnedRepository
    {
        public string name { get; set; }

        public string description { get; set; }

        public string homepageUrl { get; set; }

        public string url { get; set; }

        public PinnedRepositoryLanguages languages { get; set; }
    }

    public class PinnedRepositoryLanguages
    {
        public List<PinnedRepositoryLanguage> nodes { get; set; }
    }

    public class PinnedRepositoryLanguage
    {
        public string name { get; set; }

        public string color { get; set; }
    }

}
