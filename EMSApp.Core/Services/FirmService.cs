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
            try
            {
                AppRepository.Create(firm);
                await AppRepository.SaveAsync();
                return Response.Ok(firm);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<Firm>(new List<Error> { _EP.GetError("server_error") });
            }
        }

        public async Task<Response<FirmContact>> CreateFirmContact(FirmContact firmContact)
        {
            try
            {
                AppRepository.Create(firmContact);
                await AppRepository.SaveAsync();
                return Response.Ok(firmContact);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<FirmContact>(new List<Error> { _EP.GetError("server_error") });
            }
        }

        public async Task<Response<List<Firm>>> GetAllAsync()
        {
            var firms = await AppRepository.GetAllAsync<Firm>();
            return Response.Ok(firms.ToList());

        }

        public async Task<Response<Firm>> GetFirmById(Guid id)
        {
            var firm = await AppRepository.GetFirstAsync<Firm>(f=> f.Id == id, 
                includeProperties: "Fairs,Contacts");
            return Response.Ok(firm);
        }

        public async Task<Response<FirmContact>> GetFirmContactById(Guid id)
        {
            var firmContact = await AppRepository.GetFirstAsync<FirmContact>(f => f.Id == id);
            return Response.Ok(firmContact);
        }

        public async Task<Response<List<FirmContact>>> GetFirmContacts(Guid id)
        {
            var firm = await AppRepository.GetFirstAsync<Firm>(f => f.Id == id, includeProperties: "Contacts");
            return Response.Ok(firm.Contacts.ToList());
        }

        public async Task<Response<List<Fair>>> GetFirmFairs(Guid id)
        {
            var firm = await AppRepository.GetFirstAsync<Firm>(f => f.Id == id, includeProperties: "Fairs");
            return Response.Ok(firm.Fairs.ToList());
        }

        public async Task<Response<Firm>> Update(Firm firm)
        {
            try
            {
                AppRepository.Update(firm);
                await AppRepository.SaveAsync();
                return Response.Ok(firm);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<Firm>(new List<Error> { _EP.GetError("server_error") });
            }
        }

        public async Task<Response<FirmContact>> UpdateFirmContact(FirmContact firmContact)
        {
            try
            {
                AppRepository.Update(firmContact);
                await AppRepository.SaveAsync();
                return Response.Ok(firmContact);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return Response.Fail<FirmContact>(new List<Error> { _EP.GetError("server_error") });
            }
        }
    }
}
