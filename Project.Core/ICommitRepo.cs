namespace Project.Core;

public interface ICommitRepo {
    
    (Response Response, int CommitId) Create(CreateCommitDTO Commit);
    IReadOnlyCollection<CommitDTO> ReadAll();
    CommitDTO? Find(int CommitId);
}