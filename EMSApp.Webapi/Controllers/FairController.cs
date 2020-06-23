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
    public class FairController : BaseController<FairController>
    {
        private readonly IFairService _fairService;
        
        public FairController(IFairService fairService)
        {
            _fairService = fairService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _fairService.GetAll();
            var fairs = Mapper.Map<List<FairListDto>>(response.Data);
            return Ok(fairs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _fairService.GetById(id);
            var fair = Mapper.Map<FairDetailDto>(response.Data);
            return Ok(fair);
        }

        [HttpGet("{id:guid}/firms", Name = "GetFairFirms")]
        public async Task<IActionResult> GetFairFirms(Guid id)
        {
            if (id != Guid.Empty)
            {
                var response = await _fairService.GetFirmsByFairId(id);
                var fairs = Mapper.Map<List<FirmListDto>>(response.Data);
                return Ok(fairs);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Validate]
        public async Task<IActionResult> Create(FairCreateDto fairCreateDto)
        {
            var firm = Mapper.Map<Fair>(fairCreateDto);
            var response = await _fairService.Create(firm);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok();
        }

        [HttpPut("{fairId:guid}")]
        [Validate]
        public async Task<IActionResult> Update(Guid fairId, FairCreateDto fairCreateDto)
        {
            if (fairId == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _fairService.GetById(fairId);
            var firm = response.Data;
            Mapper.Map(fairCreateDto, firm);

            var updResponse = await _fairService.Update(firm);
            if (!updResponse.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();
        }

        [HttpPost(template:"{fairId:guid}/firm/{firmId:guid}")]
        public async Task<IActionResult> AddFirmToFair(Guid fairId, Guid firmId)
        {
            if(fairId == Guid.Empty || firmId == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _fairService.AddFirmToFair(fairId, firmId);
            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }
            return Ok();
        }

    }
}
