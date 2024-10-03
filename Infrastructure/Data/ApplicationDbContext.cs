using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options) , IApplicationDbContext
    {

    }
}
