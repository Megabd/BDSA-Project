using Project;
namespace Project.Core;

public interface IUserRepoRepo {
    
    //(Response Response, int UserRepoId) Create(CreateUserRepoDTO UserRepo);
    IReadOnlyCollection<UserRepoDTO> ReadAll();
    UserRepoDTO? Find(int UserRepoId);

    Response Update(UpdateUserRepoDTO UserRepo);
}