using CW10.Models;
using CW10.Services;
using Microsoft.EntityFrameworkCore;

public class TripService : ITripService
{
    private readonly MasterContext _context;

    public TripService(MasterContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TripDto>> GetTripsAsync(int page, int pageSize)
    {
        var baseQuery = _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries);

        var totalCount = await baseQuery.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var trips = await baseQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            }).ToListAsync();

        return new PagedResult<TripDto>
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = totalPages,
            Trips = trips
        };
    }
}