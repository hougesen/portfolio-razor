const axios = require('axios');

exports.handler = async function () {
    const githubInfo = await axios({
        url: 'https://api.github.com/graphql',
        method: 'POST',
        headers: {
            // Remember to set this in the aws control panel
            authorization: `Bearer ${process.env.PERSONAL_ACCESS_TOKEN}`,
        },
        data: {
            query: `
                query GET_PROJECTS {
                    user(login: "Hougesen") {
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
        `,
        },
    })
        .then((res) => {
            return res.data.data.user;
        })
        .catch((err) => {
            console.log('error fetching repositories', err);
            return null;
        });

    if (githubInfo) {
        return {
            FullName: githubInfo.name,
            Description: githubInfo.bio,
            ProfileImage: githubInfo.avatarUrl,
            Company: githubInfo.company,
            Location: githubInfo.location,
            GithubUserName: githubInfo.login,
            TwitterUserName: githubInfo.twitterUsername,
            Repositories: githubInfo.pinnedItems.nodes.map((r) => ({
                Name: r.name,
                Description: r.description,
                GithubUrl: r.url,
                HomepageUrl: r.homepageUrl,
                Languages: r.languages.nodes.map((l) => ({
                    Name: l.name,
                    Color: l.color,
                })),
            })),
        };
    }

    return null;
};
