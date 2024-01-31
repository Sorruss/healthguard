using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using healthguard.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IBloodTypeRepository _bloodTypeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientController(
            IBloodTypeRepository bloodTypeRepository,
            ICompanyRepository companyRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _bloodTypeRepository = bloodTypeRepository;
            _companyRepository = companyRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        public IActionResult GetPatients()
        {
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patients);
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patient = _patientRepository.GetPatient(patientId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patient);
        }

        [HttpGet("notifications/{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Notification>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotificationsByPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var notifications = _mapper.Map<List<NotificationDto>>(_patientRepository.GetNotificationsByPatient(patientId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notifications);
        }

        [HttpGet("measurements/{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Measurement>))]
        [ProducesResponseType(400)]
        public IActionResult GetMeasurementsByPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var measurements = _mapper.Map<List<MeasurementDto>>(_patientRepository.GetMeasurementsByPatient(patientId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(measurements);
        }

        [HttpGet("mrecords/{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalRecord>))]
        [ProducesResponseType(400)]
        public IActionResult GetMedicalRecordsByPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var medicalRecords = _mapper.Map<List<MedicalRecordDto>>(_patientRepository.GetMedicalRecordsByPatient(patientId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(medicalRecords);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient(
            [FromQuery] int companyId,
            [FromQuery] int bloodTypeId,
            [FromBody] PatientPOST patientCreate)
        {
            if (patientCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientMap = _mapper.Map<Patient>(patientCreate);
            patientMap.Company = _companyRepository.GetCompany(companyId);
            patientMap.BloodType = _bloodTypeRepository.GetBloodType(bloodTypeId);

            if (!_patientRepository.CreatePatient(patientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{patientId}")]
        [Authorize(Roles = "Administrator,Patient")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePatient(int patientId, [FromBody] PatientUPDATE patientUpdate)
        {
            if (patientUpdate == null || patientUpdate.PatientId != patientId)
                return BadRequest(ModelState);

            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var patientMap = _mapper.Map<Patient>(patientUpdate);
            if (!_patientRepository.UpdatePatient(patientMap))
            {
                ModelState.AddModelError("", "Something went wrong updating patient");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{patientId}")]
        [Authorize(Roles = "Administrator,Patient")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patientToDelete = _patientRepository.GetPatient(patientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_patientRepository.DeletePatient(patientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting patient");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
