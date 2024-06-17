using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using healthguard.Controllers;
using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private readonly ICompanyManagerRepository _companyManagerRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyControllerTests()
        {
            _companyManagerRepository = A.Fake<ICompanyManagerRepository>();
            _patientRepository = A.Fake<IPatientRepository>();
            _companyRepository = A.Fake<ICompanyRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void CompanyController_GetCompanies_ReturnOK()
        {
            // Arrange
            var controller = new CompanyController(_companyManagerRepository, _patientRepository, _companyRepository, _mapper);

            // Act
            var result = controller.GetCompanies();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CompanyController_GetCompany_ReturnOK()
        {
            // Arrange
            var controller = new CompanyController(_companyManagerRepository, _patientRepository, _companyRepository, _mapper);

            // Act
            var result = controller.GetCompany(2);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void CompanyController_CreatePatient_ReturnOK()
        {
            // Arrange
            var company = A.Fake<Company>();
            var companyCreate = A.Fake<CompanyDto>();
            A.CallTo(() => _mapper.Map<Company>(companyCreate)).Returns(company);
            A.CallTo(() => _companyRepository.CreateCompany(company)).Returns(true);
            var controller = new CompanyController(_companyManagerRepository, _patientRepository, _companyRepository, _mapper);

            // Act
            var result = _companyRepository.CreateCompany(company);

            // Assert
            result.Should().Be(true);
        }
    }
}
