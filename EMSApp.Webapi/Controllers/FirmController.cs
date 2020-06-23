using EMSApp.Core.DTO;
using EMSApp.Core.Entities;
using EMSApp.Core.Services;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [TenantRequired]
    [Authorize]
    public class FirmController : BaseController<FirmController>
    {
        private readonly IFirmService _firmService;
        
        public FirmController(IFirmService firmService)
        {
            _firmService = firmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _firmService.GetAllAsync();
            var firms = Mapper.Map<List<FirmListDto>>(response.Data);
            return Ok(firms);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _firmService.GetFirmById(id);
            var firmDetailDto = Mapper.Map<FirmDetailDto>(response.Data);
            return Ok(firmDetailDto);
        }

        [HttpGet(template:"{id:guid}/contacts",Name = "GetFirmContacts")]
        public async Task<IActionResult> GetFirmContacts(Guid id)
        {
            if(id != Guid.Empty)
            {
                var response = await _firmService.GetFirmContacts(id);
                var contacts = Mapper.Map<List<FirmContactListDto>>(response.Data);
                return Ok(contacts);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet(template:"{id:guid}/fairs",Name = "GetFirmFairs")]
        public async Task<IActionResult> GetFirmFairs(Guid id)
        {
            if(id != Guid.Empty)
            {
                var response = await _firmService.GetFirmFairs(id);
                var fairs = Mapper.Map<List<FairListDto>>(response.Data);
                return Ok(fairs);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Validate]
        public async Task<IActionResult> Create(FirmCreateDto firmCreateDto)
        {
            var firm = Mapper.Map<Firm>(firmCreateDto);
            var response = await _firmService.Create(firm);
            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();
        }

        [HttpPost(template:"{id:guid}/contact")]
        [Validate]
        public async Task<IActionResult> CreateFirmContact(Guid id, FirmContactCreateDto firmContactCreateDto)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }
            var firmContact = Mapper.Map<FirmContact>(firmContactCreateDto);
            firmContact.FirmId = id;
            var response = await _firmService.CreateFirmContact(firmContact);
            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();

        }

        [HttpPut(template: "{firmId:guid}")]
        [Validate]
        public async Task<IActionResult> Update(Guid firmId, FirmCreateDto firmCreateDto)
        {
            if (firmId == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _firmService.GetFirmById(firmId);
            var firm = response.Data;
            Mapper.Map(firmCreateDto, firm);

            var updResponse = await _firmService.Update(firm);
            if (!updResponse.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();
        }

        [HttpPut(template: "{firmId:guid}/contact/{contactId:guid}")]
        [Validate]
        public async Task<IActionResult> UpdateFirmContact(Guid firmId, Guid contactId, 
            FirmContactCreateDto firmContactCreateDto)
        {
            if (contactId == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _firmService.GetFirmContactById(contactId);
            var firmContact = response.Data;
            Mapper.Map(firmContactCreateDto, firmContact);

            var updResponse = await _firmService.UpdateFirmContact(firmContact);
            if (!updResponse.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();
        }
    }
}