using EMSApp.Infrastructure.MultiTenancy;
using EMSApp.Shared.Core;

namespace EMSApp.Infrastructure.Data;

public static class EMSServiceCollectionExtensions
{
      public static void AddMultiTenant<T>() where T : ITenantInfo, new()
      {
            
      }
}