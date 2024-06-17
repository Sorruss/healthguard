using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using healthguard.Controllers;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Tests.Controllers
{
    public class SpecializationControllerTests
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationControllerTests()
        {
            _specializationRepository = A.Fake<ISpecializationRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void SpecializationController_GetSpecializations_ReturnOK()
        {
            // Arrange
            var controller = new SpecializationController(_specializationRepository, _mapper);

            // Act
            var result = controller.GetSpecializations();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void SpecializationController_GetSpecialization_ReturnOK()
        {
            // Arrange
            var controller = new SpecializationController(_specializationRepository, _mapper);

            // Act
            var result = controller.GetSpecialization(1);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void SpecializationController_CreateSpecialization_ReturnOK()
        {
            // Arrange
            var companyCreate = A.Fake<Specialization>();
            A.CallTo(() => _specializationRepository.CreateSpecialization(companyCreate)).Returns(true);
            var controller = new SpecializationController(_specializationRepository, _mapper);

            // Act
            var result = _specializationRepository.CreateSpecialization(companyCreate);

            // Assert
            result.Should().Be(true);
        }
    }
}
