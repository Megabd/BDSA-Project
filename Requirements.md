# Functional Requirements

1. The user shall be able to specify the path of a repository.
2. The system shall be able to extract information (commits, author names, author dates) from the repository from the given path.
3. The user shall be able to specify what information is displayed through command-line switches.
4. The system shall, after having been given a repository, analyze and store this repository and its commits in a database.
5. If the repository already exists in the database, the system shall be able to analyze and update the entry in the database.
    - In case the repository is reanalyzed, and nothing has changed, it should not update the entry.
6. The system shall, based on the users input, be able to get the relevant information from the database and display it in a relevant format.
    - Given the commit frequency command-line switch, the system shall display the frequency of commits for each date.
    - Given the commit author command-line switch, the system shall, for each author, display the frequency of commits for each date.

# Non-functional Requirements

1. The project should include a test-suite.
    - The individual tests should be unit-tests.
    - The test-suite should test the persistance of the program.
    - Upon pushing to main, a Github Actions workflow should ensure that the program passes the test-suite.
2. The project should include documentation covering:
    - Functional and non-functional requirements
    - Use-cases
3. The process of development should be agile.
    - The process of development should be test-driven.
