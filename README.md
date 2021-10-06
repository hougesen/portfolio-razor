# portfolio-razor

Replica of my [portfolio](https://github.com/hougesen/portfolio) made using C# Razor.

This website automatically fetches data from my [Github](https://github.com/hougesen) using the Github GraphQL API.

Feel free to use it as a template if you wish.

## Fetching Github user information

To fetch Github user information you have to create a Github personal access token (only public permisssions needed). 

```js
const query = gql`
    query GET_PROJECTS {
        user(login: `${YOUR_GITHUB_HANDLE}`) {
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
`;
```
