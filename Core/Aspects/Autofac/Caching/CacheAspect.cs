using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception 
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) //Eger biz kes'e vaxt vremezsek o 60 deq Kes'de duracaq sonra silinecek.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //eger basqa meselen; Redis isletsek sadece o papkni yaziriq
        }

        public override void Intercept(IInvocation invocation) //MethodInterception'daki Intercept'i override edrik ki o kod bloku ise dussun
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //method adini tapiriq  = nameSpace'ni al(Business.Concrete.IProductService).Metod adi -> qisasi bie key yaziriq ==>> NorthWind.Business.Concrete.IProductService.GetAll
            var arguments = invocation.Arguments.ToList(); //Metodun parametresi varsa onu List et
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //Metodun parametrelerini tek-tek gez eger varse elave et ==>>NorthWind.Business.Concrete.IProductService.GetAll(1 eger olmasa null)
            if (_cacheManager.IsAdd(key)) //Yaddasabaxir ki bu adda key varmi?
            {
                invocation.ReturnValue = _cacheManager.Get(key); //Eger varsa HEC CALISDIRMADAN birbasa Kes'den al.
                return;
            }
            invocation.Proceed(); //yoxsa bazadan al ve Kes'e elave et.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //elave edirik :)
        }
    }
}
