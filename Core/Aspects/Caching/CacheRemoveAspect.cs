using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.InterCeptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern= pattern;
            _cacheManager=ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
		/*
		 kod parçası, bir metodun başarılı bir şekilde çalıştırılmasının ardından, o metoda bağlı olarak cache'ten belirli öğeleri silecek bir yöntemi tanımlıyor. Bu sayede, veriler güncellendiği zaman cache'in güncel kalmasını sağlayabilirsiniz.
		 */
	}
}
