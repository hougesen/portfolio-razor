# portfolio-razor

Replica of my [portfolio](https://github.com/hougesen/portfolio) made using C# Razor.

This website automatically fetches data from my [Github](https://github.com/hougesen) using the Github GraphQL API.

Feel free to use it as a template if you wish.

## Fetching Github user information

To fetch Github user information you have to create a Github personal access token (only public permisssions needed).

I currently have the function to fetch my user information seperated into an [AWS Lambda function](./aws-lambda-fetch-github-user/), so I don't have to expose my Github access token. If you don't care about that or wish to call the api from frontend/backend, then the following GraphQL query can be used:

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
