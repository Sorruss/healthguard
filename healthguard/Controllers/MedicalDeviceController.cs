﻿using AutoMapper;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/MedicalDevices")]
    [ApiController]
    public class MedicalDeviceController : Controller
    {
        private readonly IMedicalDeviceTypeRepository _medicalDeviceTypeRepository;
        private readonly IMedicalDeviceRepository _medicalDeviceRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public MedicalDeviceController(
            IMedicalDeviceTypeRepository medicalDeviceTypeRepository,
            IMedicalDeviceRepository medicalDeviceRepository,
            IManufacturerRepository manufacturerRepository,
            IMapper mapper)
        {
            _medicalDeviceTypeRepository = medicalDeviceTypeRepository;
            _medicalDeviceRepository = medicalDeviceRepository;
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalDevice>))]
        public IActionResult GetMedicalDevices()
        {
            var medicalDevices = _mapper.Map<List<MedicalDeviceDto>>(_medicalDeviceRepository.GetMedicalDevices());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(medicalDevices);
        }

        [HttpGet("{mdeviceId}")]
        [ProducesResponseType(200, Type = typeof(MedicalDevice))]
        [ProducesResponseType(400)]
        public IActionResult GetMedicalDevice(int mdeviceId)
        {
            if (!_medicalDeviceRepository.MedicalDeviceExists(mdeviceId))
                return NotFound();

            var medicalDevice = _mapper.Map<MedicalDeviceDto>(_medicalDeviceRepository.GetMedicalDevice(mdeviceId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(medicalDevice);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMedicalDevice(
            [FromQuery] int manfId,
            [FromQuery] int mdevicetypeId,
            [FromBody] MedicalDeviceDto medicalDeviceCreate)
        {
            if (medicalDeviceCreate == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var medicalDeviceMap = _mapper.Map<MedicalDevice>(medicalDeviceCreate);
            medicalDeviceMap.MedicalDeviceType = _medicalDeviceTypeRepository.GetMedicalDeviceType(mdevicetypeId);
            medicalDeviceMap.Manufacturer = _manufacturerRepository.GetManufacturer(manfId);

            if (!_medicalDeviceRepository.CreateMedicalDevice(medicalDeviceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{mdeviceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMedicalDevice(int mdeviceId, [FromBody] MedicalDeviceDto medicalDeviceUpdate)
        {
            if (medicalDeviceUpdate == null || medicalDeviceUpdate.MedicalDeviceId != mdeviceId)
                return BadRequest(ModelState);

            if (!_medicalDeviceRepository.MedicalDeviceExists(mdeviceId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var medicalDeviceMap = _mapper.Map<MedicalDevice>(medicalDeviceUpdate);
            if (!_medicalDeviceRepository.UpdateMedicalDevice(medicalDeviceMap))
            {
                ModelState.AddModelError("", "Something went wrong updating medical device");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mdeviceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMedicalDevice(int mdeviceId)
        {
            if (!_medicalDeviceRepository.MedicalDeviceExists(mdeviceId))
                return NotFound();

            var medicalDeviceToDelete = _medicalDeviceRepository.GetMedicalDevice(mdeviceId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_medicalDeviceRepository.DeleteMedicalDevice(medicalDeviceToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting medical device");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}