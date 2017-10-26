namespace pubg_database.Services.Cache
{
    public interface ICacheProvider
    {
        void Add(string key, object value);
        void Add(string key, object value, int cacheSecond);
        object Get(string key);
        T Get<T>(string key) where T : class;
        void Remove(string key);
    }
}