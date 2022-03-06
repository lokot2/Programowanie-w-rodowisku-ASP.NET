using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        private readonly IRepository<MembershipType> repository;
        private readonly IMapper mapper;

        public MembershipTypesController(IRepository<MembershipType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MembershipTypeDto>> GetmembershipTypes()
        {
            var membershipTypes = repository.FindAll().ToList();
            var membershipTypeDtos = mapper.Map<IEnumerable<MembershipTypeDto>>(membershipTypes);

            return Ok(membershipTypeDtos);
        }
    }
}