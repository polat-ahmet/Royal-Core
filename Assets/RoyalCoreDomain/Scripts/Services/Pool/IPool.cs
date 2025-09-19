using System.Threading;
using System.Threading.Tasks;

namespace RoyalCoreDomain.Scripts.Services.Pool
{
    public interface IPool<T>
    {
        T Rent();           // Havuzdan al (yoksa üret)
        void Return(T item); // Geri ver
        int Count { get; }  // Stacked (bekleyen) miktar
    }
}