sequenceDiagram
    participant F as Frontend
    participant B as Backend
    participant D as Database
    participant G as Github
    F ->>+ B: Get forks: /forks/<user>/<repository>
    B ->>+ G: Get forks
    G ->>- B: Forks: json-format
    B ->>- F: Forks amount: int
    
    F ->>+ B: CommitFrequency: /frequency/<user>/<repository>
    B ->>+ G: Clone: <user>/<repository>
    G ->>- B: Repository clone

    loop For every commit
        B ->> D: Create Commit
    end 

    B ->>+ D: Update Repository Table
    D ->>- B: Response

    B ->>- F: CommitFrequency: json-format

    F ->>+ B: CommitAuthor: /author/<user>/<repository>
    B ->>+ G: Clone: <user>/<repository>
    G ->>- B: Repository clone

    loop For every author
        loop For every author commit
            B ->> D: Create Author Commit
        end
    end 

    B ->>+ D: Update Repository Table
    D ->>- B: Response

    B ->>- F: CommitAuthor: json-format
