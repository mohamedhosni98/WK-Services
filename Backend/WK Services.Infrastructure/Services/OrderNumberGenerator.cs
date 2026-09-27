using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK_Services.Infrastructure.Presistence.Context;

namespace WK_Services.Infrastructure.Services
{
    public class OrderNumberGenerator
    {
        private readonly ApplicationDbContext _context;

        public OrderNumberGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(int clientId, string clientName)
        {
            var today = DateTime.UtcNow.Date;

            var todayOrdersCount = await _context.Orders
                .Where(o => o.ClientId == clientId && o.CreatedAt.Date == today)
                .CountAsync();

            var serial = todayOrdersCount + 1;
            var datePart = today.ToString("ddMMyyyy");

            return $"{clientName}_{datePart}_{serial}";
        }
    }
}
