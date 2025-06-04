namespace CW10.Services;

using CW10.Models;
using System.Threading.Tasks;

public interface ITripService
{
    Task<PagedResult<TripDto>> GetTripsAsync(int page, int pageSize);
}