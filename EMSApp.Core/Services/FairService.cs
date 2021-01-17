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
            var fairs = await AppRepository.GetAllAsync<Fair>();
            return Response.Ok(fairs.ToList());
            
        }
        public async Task<Response<Fair>> GetById(Guid id)
        {
            var fair = await AppRepository.GetFirstAsync<Fair>(f => f.Id == id, includeProperties:"Firms");
            return Response.Ok(fair);
        }
        public async Task<Response<Fair>> Create(Fair fair)
        {
            try
            {
                AppRepository.Create(fair);
                await AppRepository.SaveAsync();
                return Response.Ok(fair);

            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<Fair>(new List<Error> { _EP.GetError("server_error") });
            }
            
        }
        public async Task<Response<Fair>> Update(Fair fair)
        {
            try
            {
                AppRepository.Update(fair);
                await AppRepository.SaveAsync();
                return Response.Ok(fair);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<Fair>(new List<Error> { _EP.GetError("server_error") });
            }
        }
        public async Task<Response<List<Firm>>> GetFirmsByFairId(Guid id)
        {
            var firms = await AppRepository.GetAsync<Firm>(f => f.Fairs.Any(fa => fa.Id == id)
                , includeProperties: "Fairs");
            return Response.Ok(firms.ToList());
        }
        public async Task<Response<bool>> AddFirmToFair(Guid fairId, Guid firmId)
        {
            var fair = await AppRepository.GetFirstAsync<Fair>(x => x.Id == fairId);
            var firm = await AppRepository.GetFirstAsync<Firm>(x => x.Id == firmId);
            fair.Firms.Add(firm);

            await AppRepository.SaveAsync();
            return Response.Ok(true);
        }
    }
}
