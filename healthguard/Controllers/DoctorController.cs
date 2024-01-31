using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using healthguard.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorController(
            ISpecializationRepository specializationRepository,
            IDoctorRepository doctorRepository,
            IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doctor>))]
        public IActionResult GetDoctors()
        {
            var doctors = _mapper.Map<List<DoctorDto>>(_doctorRepository.GetDoctors());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doctors);
        }

        [HttpGet("{doctorId}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public IActionResult GetDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var doctor = _doctorRepository.GetDoctor(doctorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doctor);
        }

        [HttpGet("mrecords/{doctorId}")]
        [Authorize(Roles = "Administrator,Doctor")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalRecord>))]
        [ProducesResponseType(400)]
        public IActionResult GetMedicalRecordsByDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var medicalRecords = _mapper.Map<List<MedicalRecordDto>>(_doctorRepository.GetMedicalRecordsByDoctor(doctorId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(medicalRecords);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDoctor(
            [FromQuery] int spezId,
            [FromBody] DoctorDto doctorCreate)
        {
            if (doctorCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctorMap = _mapper.Map<Doctor>(doctorCreate);
            doctorMap.Specialization = _specializationRepository.GetSpecialization(spezId);

            if (!_doctorRepository.CreateDoctor(doctorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{doctorId}")]
        [Authorize(Roles = "Administrator,Doctor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctor(int doctorId, [FromBody] DoctorPOST doctorUpdate)
        {
            if (doctorUpdate == null || doctorUpdate.DoctorId != doctorId)
                return BadRequest(ModelState);

            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var doctor = new Doctor
            {
                DoctorId = doctorUpdate.DoctorId,
                Specialization = _specializationRepository.GetSpecialization(doctorUpdate.SpecializationId),
                ApplicationUser = doctorUpdate.ApplicationUser
            };

            if (!_doctorRepository.UpdateDoctor(doctor))
            {
                ModelState.AddModelError("", "Something went wrong updating doctor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{doctorId}")]
        [Authorize(Roles = "Administrator,Doctor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var doctorManagerToDelete = _doctorRepository.GetDoctor(doctorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_doctorRepository.DeleteDoctor(doctorManagerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting doctor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
