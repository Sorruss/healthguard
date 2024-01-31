using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/Administrators")]
    [ApiController]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IMapper _mapper;

        public AdministratorController(IAdministratorRepository administratorRepository, IMapper mapper)
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Administrator>))]
        public IActionResult GetAdministrators()
        {
            var administrators = _mapper.Map<List<AdministratorDto>>(_administratorRepository.GetAdministrators());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(administrators);
        }

        [HttpGet("{adminId}")]
        [ProducesResponseType(200, Type = typeof(Administrator))]
        [ProducesResponseType(400)]
        public IActionResult GetAdministrator(int adminId)
        {
            if (!_administratorRepository.AdministratorExists(adminId))
                return NotFound();

            var administrator = _administratorRepository.GetAdministrator(adminId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(administrator);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAdministrator([FromBody] AdministratorDto administratorCreate)
        {
            if (administratorCreate == null)
                return BadRequest(ModelState);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var administratorMap = _mapper.Map<Administrator>(administratorCreate);
            if (!_administratorRepository.CreateAdministrator(administratorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{adminId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAdministrator(int adminId, [FromBody] Administrator administratorUpdate)
        {
            if (administratorUpdate == null || administratorUpdate.AdministratorId != adminId)
                return BadRequest(ModelState);

            if (!_administratorRepository.AdministratorExists(adminId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var administratorMap = _mapper.Map<Administrator>(administratorUpdate);
            if (!_administratorRepository.UpdateAdministrator(administratorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating administrator");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{adminId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAdministrator(int adminId)
        {
            if (!_administratorRepository.AdministratorExists(adminId))
                return NotFound();

            var administratorToDelete = _administratorRepository.GetAdministrator(adminId);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_administratorRepository.DeleteAdministrator(administratorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting administrator");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
