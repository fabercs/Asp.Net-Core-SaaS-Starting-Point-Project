using System.Threading;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    
    public class CurrentTenantContextAccessor : ICurrentTenantContextAccessor
    {
        private static AsyncLocal<TenantHolder> _currentTenantContext = new AsyncLocal<TenantHolder>();
        public TenantContext CurrentTenant { 
            get {
                return _currentTenantContext.Value?.Context;
            } 
            set {
                var holder = _currentTenantContext.Value;
                if (holder != null)
                {
                    holder.Context = null;
                }
                if(value != null)
                {
                    _currentTenantContext.Value = new TenantHolder { Context = value };
                }
            }  
        }

        private class TenantHolder
        {
            public TenantContext Context;
        }
    }
}
