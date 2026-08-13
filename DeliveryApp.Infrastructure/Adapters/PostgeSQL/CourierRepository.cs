using DeliveryApp.Core.Application.UseCases.Queries.GetAllCouriers;
using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Adapters.PostgeSQL;

public class CourierRepository : ICourierRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CourierRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region имплементация интерфейса ICourierRepository

    public async Task AddAsync(Courier courier)
    {
        ArgumentNullException.ThrowIfNull (courier);
        await _dbContext.Couriers.AddAsync(courier);
    }

    public async Task UpdateAsync(Courier courier)
    {
        ArgumentNullException.ThrowIfNull(courier);
        _dbContext.Couriers.Update(courier);
    }

    
    public async Task<Courier> GetByIdAsync(Guid courierId, CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .Include(x => x.Assignments)
            .SingleOrDefaultAsync(x => x.Id == courierId,cancellationToken);
    }

    public async Task<IEnumerable<Courier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .Include(x => x.Assignments)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetAllCouriersQueryDto>> GetAllCouriersDtosAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .Select(c => new GetAllCouriersQueryDto
            {
                CourierId = c.Id,
                Name = c.Name,
                Location = c.Location
            })
            .ToListAsync(cancellationToken);

    }

    #endregion
}
