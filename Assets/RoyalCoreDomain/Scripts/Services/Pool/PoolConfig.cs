namespace RoyalCoreDomain.Scripts.Services.Pool
{
    public sealed class PoolConfig
    {
        public int InitialAmount      = 0;    // başlangıçta üretilecek adet (Prewarm)
        public int MaxSize      = 32;  // toplam üretilip saklanabilecek en çok miktar
        public bool StrictCap   = false;// true→MaxSize üzerinde üretme (Throw), false→sınırsız üret
    }
}