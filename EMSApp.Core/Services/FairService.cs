using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IFairService
    {
        Task<Response<List<Fair>>> GetAll();
        Task<Response<Fair>> GetById(Guid id);
        Task<Response<Fair>> Create(Fair fair);
        Task<Response<Fair>> Update(Fair fair);
        Task<Response<List<Firm>>> GetFirmsByFairId(Guid id);
        Task<Response<bool>> AddFirmToFair(Guid fairId, Guid firmId);
    }

    public class FairService : BaseService, IFairService
    {
        public FairService(IServiceProvider provider) : base(provider){}

        public async Task<Response<List<Fair>>> GetAll()
        {
            var response = new Response<List<Fair>>();
            var fairs = await AppRepository.GetAllAsync<Fair>();
            response.Data = fairs.ToList();
            response.Success = true;
            return response;
        }
        public async Task<Response<Fair>> GetById(Guid id)
        {
            var response = new Response<Fair>();
            var fair = await AppRepository.GetFirstAsync<Fair>(f => f.Id == id, includeProperties:"FairFirm.Firm");
            response.Data = fair;
            response.Success = true;
            return response;
        }
        public async Task<Response<Fair>> Create(Fair fair)
        {
            var response = new Response<Fair> { Success = true };
            try
            {
                AppRepository.Create(fair);
                await AppRepository.SaveAsync();
                response.Data = fair;

            }catch(Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }
            return response;
            
        }
        public async Task<Response<Fair>> Update(Fair fair)
        {
            var response = new Response<Fair> { Success = true };
            try
            {
                AppRepository.Update(fair);
                await AppRepository.SaveAsync();
                response.Data = fair;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }
            return response;
        }
        public async Task<Response<List<Firm>>> GetFirmsByFairId(Guid id)
        {
            var response = new Response<List<Firm>> { Success = true };
            var firms = await AppRepository.GetAsync<Firm>(f => f.FairFirm.Any(ff => ff.FairId == id)
                , includeProperties: "FairFirm");
            response.Data = firms.ToList();
            return response;
        }
        public async Task<Response<bool>> AddFirmToFair(Guid fairId, Guid firmId)
        {
            var response = new Response<bool> { Success = true, Data = true };
            AppRepository.Create(new FairFirm { FairId = fairId, FirmId = firmId });
            await AppRepository.SaveAsync();
            return response;
        }
    }
}
