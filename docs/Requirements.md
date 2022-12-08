# Functional Requirements

1. The system shall be able to extract information (commits, author names, author dates) from the repository from the given path.
2. The system shall, after having been given a repository, analyze and store this repository and its commits in a database.
3. If the repository already exists in the database, the system shall be able to analyze and update the entry in the database.
    - In case the repository is reanalyzed, and nothing has changed, it should not update the entry.
4. The system shall be able to get the relevant information from the database.
    - It shall be able to get information on commit frequency.
    - It shall be a le to get information on commits by authors.
5. The system shall be able to run as a web-application.
    - The system shall be able to recognize and respond only to requests from authorized users.
    - The user shall be able to authenticate themselves before making any requests.
    - The user shall be able to request certain information about a certain repository (with a GET-request), by supplying the Github repository identifier.
    - The system shall be able to clone a repository to a local temporary folder.
    - The system shall be able to update local repositories.
6. The user shall be able to interact with the REST API through a frontend.
    - The frontend shall be able to make GET-requests to the API.
    - The frontend shall be able to display the JSON response in relevant diagrams.
    - The user shall be able to authenticate themselves by providing credentials.
7. The system shall be able to find the forks for some specific repository, by querying the Github API.

# Non-functional Requirements

1. The project should include a test-suite.
    - The individual tests should both be unit-tests and integration tests.
    - The test-suite should test the persistance of the program.
    - Upon pushing to main, a Github Actions workflow should ensure that the program passes the test-suite.
2. The project should include documentation covering:
    - Functional and non-functional requirements
    - Use-cases
3. The process of development should be agile.
    - The process of development should be test-driven.
4. All communication betwween the frontend and the backend should be secure and encrypted.
