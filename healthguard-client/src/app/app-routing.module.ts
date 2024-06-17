import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './ui/login/login.component';
import { RegisterComponent } from './ui/register/register.component';
import { HomeComponent } from './ui/home/home.component';
import { NotificationsComponent } from './ui/notifications/notifications.component';
import { MedicalRecordsComponent } from './ui/medical-records/medical-records.component';
import { MeasurementsComponent } from './ui/measurements/measurements.component';
import { authorizedGuard } from './guards/authorized.guard';
import { PatientProfileComponent } from './ui/patient-profile/patient-profile.component';
import { allowedToGuard } from './guards/allowed-to.guard';
import { DoctorProfileComponent } from './ui/doctor-profile/doctor-profile.component';
import { ManagerProfileComponent } from './ui/manager-profile/manager-profile.component';
import { DmedicalRecordsComponent } from './ui/dmedical-records/dmedical-records.component';
import { AddDmedicalRecordComponent } from './ui/dmedical-records/add-dmedical-record/add-dmedical-record.component';
import { EditDmedicalRecordComponent } from './ui/dmedical-records/edit-dmedical-record/edit-dmedical-record.component';
import { DnotificationsComponent } from './ui/dnotifications/dnotifications.component';
import { EditDnotificationComponent } from './ui/dnotifications/edit-dnotification/edit-dnotification.component';
import { AddDnotificationComponent } from './ui/dnotifications/add-dnotification/add-dnotification.component';
import { DpatientsComponent } from './ui/dpatients/dpatients.component';
import { MpatientsComponent } from './ui/mpatients/mpatients.component';
import { AdminPanelComponent } from './ui/admin-panel/admin-panel.component';
import { isAdminGuard } from './guards/is-admin.guard';
import { CreatePatientComponent } from './ui/admin-panel/patients/create-patient/create-patient.component';
import { PutPatientComponent } from './ui/admin-panel/patients/put-patient/put-patient.component';
import { CreateSpecializationComponent } from './ui/admin-panel/specializations/create-specialization/create-specialization.component';
import { PutSpecializationComponent } from './ui/admin-panel/specializations/put-specialization/put-specialization.component';
import { CreateNotificationComponent } from './ui/admin-panel/notifications/create-notification/create-notification.component';
import { PutNotificationComponent } from './ui/admin-panel/notifications/put-notification/put-notification.component';
import { CreateMedicalRecordComponent } from './ui/admin-panel/medical-records/create-medical-record/create-medical-record.component';
import { PutMedicalRecordComponent } from './ui/admin-panel/medical-records/put-medical-record/put-medical-record.component';
import { CreateMedicalDeviceTypeComponent } from './ui/admin-panel/medical-device-types/create-medical-device-type/create-medical-device-type.component';
import { PutMedicalDeviceTypeComponent } from './ui/admin-panel/medical-device-types/put-medical-device-type/put-medical-device-type.component';
import { CreateMedicalDeviceComponent } from './ui/admin-panel/medical-devices/create-medical-device/create-medical-device.component';
import { PutMedicalDeviceComponent } from './ui/admin-panel/medical-devices/put-medical-device/put-medical-device.component';
import { CreateManufacturerComponent } from './ui/admin-panel/manufacturers/create-manufacturer/create-manufacturer.component';
import { PutManufacturerComponent } from './ui/admin-panel/manufacturers/put-manufacturer/put-manufacturer.component';
import { CreateDoctorComponent } from './ui/admin-panel/doctors/create-doctor/create-doctor.component';
import { PutDoctorComponent } from './ui/admin-panel/doctors/put-doctor/put-doctor.component';
import { CreateCompanyComponent } from './ui/admin-panel/companies/create-company/create-company.component';
import { CreateCompanyManagerComponent } from './ui/admin-panel/company-managers/create-company-manager/create-company-manager.component';
import { PutCompanyManagerComponent } from './ui/admin-panel/company-managers/put-company-manager/put-company-manager.component';
import { PutCompanyComponent } from './ui/admin-panel/companies/put-company/put-company.component';
import { CreateAdministratorComponent } from './ui/admin-panel/administrators/create-administrator/create-administrator.component';
import { PutAdministratorComponent } from './ui/admin-panel/administrators/put-administrator/put-administrator.component';
import { CreateMeasurementComponent } from './ui/admin-panel/measurements/create-measurement/create-measurement.component';

const routes: Routes = [
  { path: "home", component: HomeComponent, canActivate: [allowedToGuard] },
  { path: "", redirectTo: "/home", pathMatch: "full" },
  { path: "login", component: LoginComponent, canActivate: [allowedToGuard] },
  { path: "register", component: RegisterComponent, canActivate: [allowedToGuard] },

  { path: "notifications", component: NotificationsComponent },
  { path: "medical-records", component: MedicalRecordsComponent },
  { path: "dmedical-records", component: DmedicalRecordsComponent },
  { path: "measurements", component: MeasurementsComponent },
  { path: "pprofile/:patientId", component: PatientProfileComponent },
  { path: "dprofile/:doctorId", component: DoctorProfileComponent },
  { path: "mprofile/:managerId", component: ManagerProfileComponent },
  { path: "add-mrecord", component: AddDmedicalRecordComponent },
  { path: "edit-record/:medicalRecordId", component: EditDmedicalRecordComponent },
  { path: "dnotifications", component: DnotificationsComponent },
  { path: "edit-dnotification/:notificationId", component: EditDnotificationComponent },
  { path: "add-dnotification", component: AddDnotificationComponent },
  { path: "dpatients", component: DpatientsComponent },
  { path: "mpatients", component: MpatientsComponent },

  { path: "admin-panel", component: AdminPanelComponent },
  { path: "create-patient", component: CreatePatientComponent },
  { path: "put-patient/:patientId", component: PutPatientComponent },
  { path: "create-specialization", component: CreateSpecializationComponent },
  { path: "put-specialization/:spezId", component: PutSpecializationComponent },
  { path: "create-notification", component: CreateNotificationComponent },
  { path: "put-notification/:notificationId", component: PutNotificationComponent },
  { path: "create-medical-record", component: CreateMedicalRecordComponent },
  { path: "put-medical-record/:medicalRecordId", component: PutMedicalRecordComponent },
  { path: "create-medical-device-type", component: CreateMedicalDeviceTypeComponent },
  { path: "put-medical-device-type/:mDeviceTypeId", component: PutMedicalDeviceTypeComponent },
  { path: "create-medical-device", component: CreateMedicalDeviceComponent },
  { path: "put-medical-device/:mDeviceId", component: PutMedicalDeviceComponent },
  { path: "create-manufacturer", component: CreateManufacturerComponent },
  { path: "put-manufacturer/:manfId", component: PutManufacturerComponent },
  { path: "create-doctor", component: CreateDoctorComponent },
  { path: "put-doctor/:doctorId", component: PutDoctorComponent },
  { path: "create-company-manager", component: CreateCompanyManagerComponent },
  { path: "put-company-manager/:managerId", component: PutCompanyManagerComponent },
  { path: "create-company", component: CreateCompanyComponent },
  { path: "put-company/:companyId", component: PutCompanyComponent },
  { path: "create-administrator", component: CreateAdministratorComponent },
  { path: "put-administrator/:administratorId", component: PutAdministratorComponent },
  { path: "create-measurement", component: CreateMeasurementComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
