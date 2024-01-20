using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using healthguard.POST;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/CompanyManagers")]
    [ApiController]
    public class CompanyManagerController : Controller
    {
        private readonly ICompanyManagerRepository _сompanyManagerRepository;
        private readonly ICompanyRepository _сompanyRepository;
        private readonly IMapper _mapper;

        public CompanyManagerController(
            ICompanyManagerRepository сompanyManagerRepository,
            ICompanyRepository сompanyRepository,
            IMapper mapper)
        {
            _сompanyManagerRepository = сompanyManagerRepository;
            _сompanyRepository = сompanyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompanyManager>))]
        public IActionResult GetCompanyManagers()
        {
            var сompanyManagers = _mapper.Map<List<CompanyManagerDto>>(_сompanyManagerRepository.GetCompanyManagers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(сompanyManagers);
        }

        [HttpGet("{managerId}")]
        [ProducesResponseType(200, Type = typeof(CompanyManager))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyManager(int managerId)
        {
            if (!_сompanyManagerRepository.CompanyManagerExists(managerId))
                return NotFound();

            var сompanyManager = _mapper.Map<CompanyManagerDto>(_сompanyManagerRepository.GetCompanyManager(managerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(сompanyManager);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompanyManager(
            [FromQuery] int companyId, 
            [FromBody] CompanyManagerPOST companyManagerCreate)
        {
            if (companyManagerCreate == null)
                return BadRequest(ModelState);

            var companyManager = _сompanyManagerRepository.GetCompanyManagers()
                .Where(e => e.Email == companyManagerCreate.Email).FirstOrDefault();

            if (companyManager != null)
            {
                ModelState.AddModelError("", "Company Manager with this email already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyManagerMap = _mapper.Map<CompanyManager>(companyManagerCreate);
            companyManagerMap.Company = _сompanyRepository.GetCompany(companyId);

            if (!_сompanyManagerRepository.CreateCompanyManager(companyManagerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{managerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompanyManager(
            int managerId, 
            [FromBody] CompanyManagerPOST companyManagerUpdate)
        {
            if (companyManagerUpdate == null || companyManagerUpdate.CompanyManagerId != managerId)
                return BadRequest(ModelState);

            if (!_сompanyManagerRepository.CompanyManagerExists(managerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var companyManagerMap = _mapper.Map<CompanyManager>(companyManagerUpdate);
            if (!_сompanyManagerRepository.UpdateCompanyManager(companyManagerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating company manager");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{managerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompanyManager(int managerId)
        {
            if (!_сompanyManagerRepository.CompanyManagerExists(managerId))
                return NotFound();

            var companyManagerToDelete = _сompanyManagerRepository.GetCompanyManager(managerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_сompanyManagerRepository.DeleteCompanyManager(companyManagerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting company manager");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
