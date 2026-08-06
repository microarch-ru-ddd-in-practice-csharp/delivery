using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Adapters.PostgeSQL;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region имплементация интерфейса IOrderRepository
    public async Task AddAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task<Order> GetAnyCreatedOrderAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Status.Name == OrderStatus.Created.Name, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAssignedOrdersAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.Where(x => x.Status.Name == OrderStatus.Assigned.Name).ToListAsync(cancellationToken);
    }

    public async Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
    }

    
    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
    }

    

    #endregion
}
