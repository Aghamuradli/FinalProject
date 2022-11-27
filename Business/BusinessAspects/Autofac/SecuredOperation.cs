using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        //JWT Ucun
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles) //rollari qaytarir
        {
            _roles = roles.Split(','); //attribute'oldugu ucun vergulle ayrilmalidir
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //Aspect oldugu ucun enjecte etmek olmur,ve bu eger biz geecekde winformla da islesek istifade ede bileceyimiz ortam yaradir.

        }

        protected override void OnBefore(IInvocation invocation) //Methodun evvelinde calisdir
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //claim'in rollarini tap 
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) //Rollari gez eger ilgili rol varsa return et. 
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}