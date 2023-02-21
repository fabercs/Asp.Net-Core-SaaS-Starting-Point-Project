using EMSApp.Core.Interfaces;

namespace EMSApp.Infrastructure.MultiTenancy
{
    public class MultiTenantContextAccessor<TTenantInfo> : IMultiTenantContextAccessor<TTenantInfo>, IMultiTenantContextAccessor
        where TTenantInfo : class, ITenantInfo, new()
    {
        internal static AsyncLocal<IMultiTenantContext<TTenantInfo>?> _asyncLocalContext = 
            new AsyncLocal<IMultiTenantContext<TTenantInfo>?>();

        public IMultiTenantContext<TTenantInfo>? MultiTenantContext
        {
            get
            {
                return _asyncLocalContext.Value;
            }

            set
            {
                _asyncLocalContext.Value = value;
            }
        }

        IMultiTenantContext? IMultiTenantContextAccessor.MultiTenantContext
        {
            get => MultiTenantContext as IMultiTenantContext;
            set => MultiTenantContext = value as IMultiTenantContext<TTenantInfo> ?? MultiTenantContext;
        }
    }
}
