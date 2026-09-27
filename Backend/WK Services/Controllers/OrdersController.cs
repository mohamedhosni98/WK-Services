using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WK_Services.Application.Dtos;
using WK_Services.Domain.Entities;
using WK_Services.Infrastructure.Presistence.Context;
using WK_Services.Infrastructure.Services;

namespace WK_Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderNumberGenerator _generator;

        public OrdersController(ApplicationDbContext context, OrderNumberGenerator generator)
        {
            _context = context;
            _generator = generator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var clientId = int.Parse(User.FindFirst("clientId")!.Value);
            var contactId = int.Parse(User.FindFirst("contactId")!.Value);

            var client = await _context.Clients.FindAsync(clientId);
            var orderNumber = await _generator.GenerateAsync(clientId, client!.Name);

            var order = new Order
            {
                OrderNumber = orderNumber,
                OrderType = dto.OrderType,
                ServiceId = dto.ServiceId,
                ClientId = clientId,
                CreatedByContactId = contactId,
                Quantity = dto.Quantity,
                RequestedDeliveryDate = dto.RequestedDeliveryDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"order number {orderNumber} saved successfully", orderNumber });
        }

        // serach by order number

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetByOrderNumber(string orderNumber)
        {
            var clientId = int.Parse(User.FindFirst("clientId")!.Value);

            var order = await _context.Orders
                .Include(o => o.Service)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber && o.ClientId == clientId);

            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }
    }
}
