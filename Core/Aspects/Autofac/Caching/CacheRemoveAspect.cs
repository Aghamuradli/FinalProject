using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception // Ne Vaxt Calisir? Data bozuldugu zaman(Data elave olunsa,Data Guncellense,Data Silinse)
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) //Ne vaxt edir? :OnSuccess Olanda  Niye? :Belke Add() Xeta verecek yeni Product elave olunmayacaq.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
