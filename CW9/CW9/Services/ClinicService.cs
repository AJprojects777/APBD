using CW9.Data;
using CW9.Models;
using CW9.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CW9.Services;

public interface IClinicService
{
    public Task<GetPatientFullDataDto> GetPatientFullDataAsync(int patientId);
}

public class ClinicService(ClinicDbContext clinicDbContext) : IClinicService
{
    public async Task<GetPatientFullDataDto> GetPatientFullDataAsync(int patientId)
    {
        Patient? patient = await clinicDbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
        {
            // return NotFound(); -> need to rise exception which will be handled inside controller with proper return value
        }

        var prescriptions = await clinicDbContext.Prescriptions
            .Where(p => p.IdPatient == patientId)
            .ToListAsync();

        foreach (Prescription p in prescriptions)
        {
            // var prescriptionDoctors = await clinicDbContext.Doctors
            //     .Where(d => d.IdDoctor == p.)
        }

        return null; // tmp solution
    }
}