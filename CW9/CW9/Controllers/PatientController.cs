using CW9.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IClinicService clinicService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatientData(int patientId)
    {
        return Ok(await clinicService.GetPatientFullDataAsync(patientId));
    }
}