using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace TimeManager.Container
{
    public class LogInterceptor : IInterceptor
    {
        private readonly ILoggerFactory loggerFactory;

        public LogInterceptor(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null) throw new ArgumentNullException("loggerFactory");
            this.loggerFactory = loggerFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            var logger = loggerFactory.Create(invocation.TargetType);
            
            try
            {
                logger.Info(invocation.Method.Name);
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                logger.Error(invocation.Method.Name, ex);
                throw;
            }
        }
    }
}
