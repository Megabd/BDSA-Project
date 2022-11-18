
namespace Project.Core;

public interface IComFrequencyResultRepo {
    
    (Response Response, int ComFreResId) Create(CreateComFrequencyResultDTO ComFreRes);
    IReadOnlyCollection<ComFrequencyResultDTO> GetComFrequencyResults (int RepositoryId);

    Response Update(UpdateComFrequencyResultDTO ComAuthRes);
}
