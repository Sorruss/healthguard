﻿using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/Measurements")]
    [ApiController]
    public class MeasurementController : Controller
    {
        private readonly IMedicalDeviceTypeRepository _medicalDeviceTypeRepository;
        private readonly IMedicalDeviceRepository _medicalDeviceRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public MeasurementController(
            IMedicalDeviceTypeRepository medicalDeviceTypeRepository,
            IMedicalDeviceRepository medicalDeviceRepository,
            IMeasurementRepository measurementRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _medicalDeviceTypeRepository = medicalDeviceTypeRepository;
            _medicalDeviceRepository = medicalDeviceRepository;
            _measurementRepository = measurementRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Measurement>))]
        public IActionResult GetMeasurements()
        {
            var measurements = _mapper.Map<List<MeasurementDto>>(_measurementRepository.GetMeasurements());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(measurements);
        }

        [HttpGet("{measurId}")]
        [ProducesResponseType(200, Type = typeof(Measurement))]
        [ProducesResponseType(400)]
        public IActionResult GetMeasurement(int measurId)
        {
            if (!_measurementRepository.MeasurementExists(measurId))
                return NotFound();

            var measurement = _mapper.Map<MeasurementDto>(_measurementRepository.GetMeasurement(measurId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(measurement);
        }

        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Measurement>))]
        [ProducesResponseType(400)]
        public IActionResult GetMeasurementsByPatient(int patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var measurements = _mapper.Map<List<MeasurementDto>>(_measurementRepository.GetMeasurementsByPatient(patientId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(measurements);
        }

        [HttpGet("patient/{patientId}/mdevicetype/{mdevicetypeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Measurement>))]
        [ProducesResponseType(400)]
        public IActionResult GetMeasurementsByPatientAndType(int patientId, int mdevicetypeId)
        {
            if (!_patientRepository.PatientExists(patientId) || 
                !_medicalDeviceTypeRepository.MedicalDeviceTypeExists(mdevicetypeId))
                return NotFound();

            var measurements = _mapper.Map<List<MeasurementDto>>(
                _measurementRepository.GetMeasurementsByPatientAndType(patientId, mdevicetypeId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(measurements);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMeasurement(
            [FromQuery] int mdeviceId,
            [FromQuery] int patientId,
            [FromBody] MeasurementDto measurementCreate)
        {
            if (measurementCreate == null)
                return BadRequest(ModelState);

            var measurement = _measurementRepository.GetMeasurements()
                .Where(e => e.MeasurementDate == measurementCreate.MeasurementDate)
                .FirstOrDefault();

            if (measurement != null)
            {
                ModelState.AddModelError("", "Measurement with this date already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var measurementMap = _mapper.Map<Measurement>(measurementCreate);
            measurementMap.MedicalDevice = _medicalDeviceRepository.GetMedicalDevice(mdeviceId);
            measurementMap.Patient = _patientRepository.GetPatient(patientId);

            if (!_measurementRepository.CreateMeasurement(measurementMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{measurId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMeasurement(int measurId, [FromBody] MeasurementDto measurementUpdate)
        {
            if (measurementUpdate == null || measurementUpdate.MeasurementId != measurId)
                return BadRequest(ModelState);

            if (!_measurementRepository.MeasurementExists(measurId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var measurementMap = _mapper.Map<Measurement>(measurementUpdate);
            if (!_measurementRepository.UpdateMeasurement(measurementMap))
            {
                ModelState.AddModelError("", "Somthing went wrong updating measurement");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{measurId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMeasurement(int measurId)
        {
            if (!_measurementRepository.MeasurementExists(measurId))
                return NotFound();

            var measurementToDelete = _measurementRepository.GetMeasurement(measurId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_measurementRepository.DeleteMeasurement(measurementToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting measurement");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}