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

    public interface IFirmService
    {
        Task<Response<Firm>> Create(Firm firm);
        Task<Response<Firm>> Update(Firm firm);
        Task<Response<List<Firm>>> GetAllAsync();
        Task<Response<Firm>> GetFirmById(Guid id);
        Task<Response<List<Fair>>> GetFirmFairs(Guid id);
        Task<Response<List<FirmContact>>> GetFirmContacts(Guid id);
        Task<Response<FirmContact>> CreateFirmContact(FirmContact firmContact);
        Task<Response<FirmContact>> UpdateFirmContact(FirmContact firmContact);
        Task<Response<FirmContact>> GetFirmContactById(Guid id);
    }
    public class FirmService : BaseService, IFirmService
    {
        public FirmService(IServiceProvider serviceProvider) : base(serviceProvider){}

        public async Task<Response<Firm>> Create(Firm firm)
        {
            var response = new Response<Firm> { Success = true };
            try
            {
                AppRepository.Create(firm);
                await AppRepository.SaveAsync();
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }
            
            response.Data = firm;
            return response;
        }

        public async Task<Response<FirmContact>> CreateFirmContact(FirmContact firmContact)
        {
            var response = new Response<FirmContact> { Success = true };
            try
            {
                AppRepository.Create(firmContact);
                await AppRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }

            response.Data = firmContact;
            return response;
        }

        public async Task<Response<List<Firm>>> GetAllAsync()
        {
            var response = new Response<List<Firm>> { Success = true };
            var firms = await AppRepository.GetAllAsync<Firm>();
            response.Data = firms.ToList();
            return response;

        }

        public async Task<Response<Firm>> GetFirmById(Guid id)
        {
            var response = new Response<Firm> { Success = true };
            var firm = await AppRepository.GetFirstAsync<Firm>(f=> f.Id == id, 
                includeProperties: "FairFirm.Fair,Contacts");
            response.Data = firm;
            return response;
        }

        public async Task<Response<FirmContact>> GetFirmContactById(Guid id)
        {
            var response = new Response<FirmContact> { Success = true };
            var firmContact = await AppRepository.GetFirstAsync<FirmContact>(f => f.Id == id);
            response.Data = firmContact;
            return response;
        }

        public async Task<Response<List<FirmContact>>> GetFirmContacts(Guid id)
        {
            var response = new Response<List<FirmContact>> { Success = true };
            var firm = await AppRepository.GetFirstAsync<Firm>(f => f.Id == id, includeProperties: "Contacts");
            response.Data = firm.Contacts.ToList();
            return response; 
        }

        public async Task<Response<List<Fair>>> GetFirmFairs(Guid id)
        {
            var response = new Response<List<Fair>> { Success = true };
            var firm = await AppRepository.GetFirstAsync<Firm>(f => f.Id == id, includeProperties: "FairFirm.Fair");
            response.Data = firm.FairFirm.Select(x=> x.Fair).ToList();
            return response;
        }

        public async Task<Response<Firm>> Update(Firm firm)
        {
            var response = new Response<Firm> { Success = true };
            try
            {
                AppRepository.Update(firm);
                await AppRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }

            response.Data = firm;
            return response;
        }

        public async Task<Response<FirmContact>> UpdateFirmContact(FirmContact firmContact)
        {
            var response = new Response<FirmContact> { Success = true };
            try
            {
                AppRepository.Update(firmContact);
                await AppRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(new Error { Description = ex.Message });
                Logger.LogError(ex, ex.Message);
            }

            response.Data = firmContact;
            return response;
        }
    }
}
