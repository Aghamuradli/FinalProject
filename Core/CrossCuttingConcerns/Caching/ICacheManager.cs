using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key); //bele de yazmaq olar amma tip donusumu etmek lazimdir
        void Add(string key, object value , int duration); 
        bool IsAdd(string key); //kesde varmi?
        void Remove(string key); //Kes'den ucurmaq
        void RemoveByPattern(string pattern); //Paramtrik olanda Pattern veririk ki Key'de hansisa sozun olmasina gore remove etsin
    }
}
