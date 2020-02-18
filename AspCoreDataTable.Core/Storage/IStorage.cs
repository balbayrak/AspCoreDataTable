using AspCoreDependency.Core.Abstract;
using System;

namespace AspCoreDataTable.Core.Storage
{
    public interface IStorage : ISingletonType
    {
        T GetObject<T>(string key);

        /// <summary>
        /// Memory cache için timespan null olarak bırakıldığında default olarak 1 gün set edilir. Remote cache için ayrı olara startup dosyasından configure edilebilir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        bool SetObject<T>(string key, T obj, DateTime? expires = null, bool? sameSiteStrict = null);

        bool Remove(string key);

        bool ExpireEntryIn(string key, TimeSpan timeSpan);

        void RemoveAll();
    }
}
