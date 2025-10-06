using Application.Contracts;
using Application.Contracts.DTOs;
using Domain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgramsController : ControllerBase
    {
        private readonly AppIdentityDbContext _db;
        public ProgramsController(AppIdentityDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _db.Programs
                .Select(p => new ProgramDto(p.Id, p.Name, p.Description ,p.StartDate, p.EndDate, p.IsActive, 0))
                .ToListAsync();
            return Ok(list);
        }
    }
}
