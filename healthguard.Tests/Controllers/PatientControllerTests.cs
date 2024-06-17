using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using healthguard.Controllers;
using healthguard.Interfaces;
using healthguard.Models;
using healthguard.POST;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Tests.Controllers
{
    public class PatientControllerTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBloodTypeRepository _bloodTypeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientControllerTests()
        {
            _userManager = A.Fake<UserManager<ApplicationUser>>();
            _bloodTypeRepository = A.Fake<IBloodTypeRepository>();
            _companyRepository = A.Fake<ICompanyRepository>();
            _patientRepository = A.Fake<IPatientRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PatientController_GetPatients_ReturnOK()
        {
            // Arrange
            var controller = new PatientController(_userManager, _bloodTypeRepository, _companyRepository, _patientRepository, _mapper);

            // Act
            var result = controller.GetPatients();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PatientController_GetPatient_ReturnOK()
        {
            // Arrange
            var controller = new PatientController(_userManager, _bloodTypeRepository, _companyRepository, _patientRepository, _mapper);

            // Act
            var result = controller.GetPatient(9);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void PatientController_CreatePatient_ReturnOK()
        {
            // Arrange
            int companyId = 2;
            int bloodTypeId = 2;
            var bloodType = A.Fake<BloodType>();
            var patient = A.Fake<Patient>();
            var patientCreate = A.Fake<PatientPOST>();
            A.CallTo(() => _mapper.Map<Patient>(patientCreate)).Returns(patient);
            patient.CompanyId = companyId;

            A.CallTo(() => _bloodTypeRepository.GetBloodType(bloodTypeId)).Returns(bloodType);
            patient.BloodType = bloodType;
            
            A.CallTo(() => _patientRepository.CreatePatient(patient)).Returns(true);
            var controller = new PatientController(_userManager, _bloodTypeRepository, _companyRepository, _patientRepository, _mapper);

            // Act
            var result = _patientRepository.CreatePatient(patient);

            // Assert
            result.Should().Be(true);
        }
    }
}
