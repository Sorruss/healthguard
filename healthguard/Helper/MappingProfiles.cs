﻿using AutoMapper;
using healthguard.Dto;
using healthguard.Models;
using healthguard.POST;

namespace healthguard.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Administrator, AdministratorDto>();
            CreateMap<AdministratorPOST, Administrator>();

            CreateMap<Patient, PatientDto>();
            CreateMap<PatientPOST, Patient>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<CompanyManager, CompanyManagerDto>();
            CreateMap<CompanyManagerPOST, CompanyManager>();

            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorPOST, Doctor>();

            CreateMap<Measurement, MeasurementDto>();
            CreateMap<MeasurementDto, Measurement>();

            CreateMap<MedicalDevice, MedicalDeviceDto>();
            CreateMap<MedicalDeviceDto, MedicalDevice>();

            CreateMap<MedicalRecord, MedicalRecordDto>();
            CreateMap<MedicalRecordDto, MedicalRecord>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();

            CreateMap<MedicalDeviceType, MedicalDeviceTypeDto>();
            CreateMap<MedicalDeviceTypeDto, MedicalDeviceType>();
        }
    }
}
