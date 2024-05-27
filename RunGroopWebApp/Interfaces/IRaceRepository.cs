using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll(); // tüm club nesnelerini getirir
        // tüm kulupleri iceren bir koleksiyonu asenkron olarak dondurur
        Task<Race> GetByIdAsync(int id); //Belirtilen id'ye sahip olan Club nesnesini getirir. asenkron calısır ve tek bir club nesnesini dondurur
        Task<IEnumerable<Race>> GetAllRacesByCity(string city); //belirtilen şehre gore club neslnelerini gertirir
        bool Add(Race race); //yeni club nesnesi ekler
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
