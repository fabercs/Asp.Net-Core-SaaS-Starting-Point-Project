using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IFairService
    {
        Task<List<Fair>> GetAll();
        Task<Fair> GetById(Guid id);
    }

    public class FairService : BaseService, IFairService
    {
        private readonly IAppRepository _appRepository;

        public FairService(IAppRepository appRepository,
            IServiceProvider provider) : base(provider)
        {
            _appRepository = appRepository;
        }
        public async Task<List<Fair>> GetAll()
        {
            var fairs = await _appRepository.GetAllAsync<Fair>();
            return fairs.ToList();
        }

        public async Task<Fair> GetById(Guid id)
        {
            var fair = await _appRepository.GetByIdAsync<Fair>(id);
            return fair;
        }
    }
}
