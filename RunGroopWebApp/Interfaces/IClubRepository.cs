using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    //Bu arayüz, veri erişim katmanında (data access layer) belirli bir yapı ve tutarlılık sağlamak için kullanılır.
    /// /Arayüzler, uygulamanın diğer bölümleri tarafından bağımlılıklardan kaçınarak esneklik sağlar ve bu sayede farklı veri erişim yöntemleri kolayca uygulanabilir. Bu arayüzü uygulayan bir sınıf, bu yöntemlerin her birini tanımlamak ve uygulamak zorundadır.

    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll(); // tüm club nesnelerini getirir
        // tüm kulupleri iceren bir koleksiyonu asenkron olarak dondurur
        Task<Club> GetByIdAsync(int id); //Belirtilen id'ye sahip olan Club nesnesini getirir. asenkron calısır ve tek bir club nesnesini dondurur
        Task<Club> GetByIdAsyncNoTracking(int id); //Belirtilen id'ye sahip olan Club nesnesini getirir. asenkron calısır ve tek bir club nesnesini dondurur
        Task<IEnumerable<Club>> GetClubByCity(string city); //belirtilen şehre gore club neslnelerini gertirir
        bool Add(Club club); //yeni club nesnesi ekler
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
