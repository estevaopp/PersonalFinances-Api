using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinances.Application.Interfaces;
using PersonalFinances.Application.ViewModel.Request.RevenueCategory;
using PersonalFinances.Application.ViewModel.Response;
using PersonalFinances.Application.ViewModel.Response.CommandResponse;
using PersonalFinances.Domain.Enums;
using PersonalFinances.Domain.Exceptions;

namespace PersonalFinances.Api.Controllers
{
    [Authorize()]
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueCategoryController : ControllerBase
    {
        private readonly IRevenueCategoryService _revenueCategoryService;

        public RevenueCategoryController(IRevenueCategoryService revenueCategoryService)
        {
            _revenueCategoryService = revenueCategoryService;
        }

        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<RevenueCategoryResponse>> GetById(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var revenueCategoryResponse = await _revenueCategoryService.GetByIdAndUserId(id, userId);

            if(revenueCategoryResponse == null)
                return NotFound();

            return Ok
            (
                new Response
                {
                    Success = true,
                    Data = revenueCategoryResponse
                }
            );
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<RevenueCategoryResponse>> Get()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var revenueCategoryResponses = await _revenueCategoryService.GetByUserId(userId);

            if(revenueCategoryResponses == null)
                return NotFound();

            return Ok
            (
                new Response
                {
                    Success = true,
                    Data = revenueCategoryResponses
                }
            );
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<RevenueCategoryResponse>> Create(CreateRevenueCategoryRequest createRevenueCategoryRequest)
        {
            if (createRevenueCategoryRequest == null)
                throw new ArgumentNullException(nameof(createRevenueCategoryRequest));
            
            if (!ModelState.IsValid)
                return BadRequest(new BusinessException("Invalid Request", nameof(CreateRevenueCategoryRequest), ErroEnum.ResourceBadRequest));

            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var revenueCategoryResponse = await _revenueCategoryService.Create(createRevenueCategoryRequest, userId);

            return Ok
            (
                new Response
                {
                    Success = true,
                    Data = revenueCategoryResponse
                }
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<RevenueCategoryResponse>> Update(UpdateRevenueCategoryRequest updateRevenueCategoryRequest, int id)
        {
            if (updateRevenueCategoryRequest == null)
                throw new ArgumentNullException(nameof(updateRevenueCategoryRequest));
            
            if (!ModelState.IsValid)
                return BadRequest(new BusinessException("Invalid Request", nameof(UpdateRevenueCategoryRequest), ErroEnum.ResourceBadRequest));

            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var revenueCategoryResponse = await _revenueCategoryService.Update(updateRevenueCategoryRequest, id, userId);

            return Ok
            (
                new Response
                {
                    Success = true,
                    Data = revenueCategoryResponse
                }
            );
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<RevenueCategoryResponse>> Delete(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var revenueCategoryResponse = await _revenueCategoryService.Delete(id, userId);

            return Ok
            (
                new Response
                {
                    Success = true,
                    Data = revenueCategoryResponse
                }
            );
        }
    }
}