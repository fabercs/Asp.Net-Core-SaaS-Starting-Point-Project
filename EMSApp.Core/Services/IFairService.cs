using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IFairService
    {
        Task<List<Fair>> GetAll();
    }

    public class FairService : IFairService
    {
        private readonly IAppRepository _appRepository;

        public FairService(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task<List<Fair>> GetAll()
        {
            var fairs = await _appRepository.GetAllAsync<Fair>();
            return fairs.ToList();
        }
    }
}
