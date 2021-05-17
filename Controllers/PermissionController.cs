using BasicSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        public Exception Error = new Exception();
        private readonly BasicSystemDbContext _context;

        public PermissionController(BasicSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoTasks
        [HttpGet("{user_id}"), Authorize]
        public async Task<ActionResult<IEnumerable<TblUserAccess>>> getPermissionLists(string user_id)
        {
            return await _context.TblUserAccess.Where(x => x.user_id == user_id).ToListAsync();
        }

        [HttpPut]
        public async Task<bool> UpdatePermission(TblUserAccess tblUserAccess)
        {
            try
            {
                var existing = _context.TblUserAccess.FirstOrDefault(x => x.Id == tblUserAccess.Id);
                if (existing != null)
                {
                    existing.Read = tblUserAccess.Read;
                    existing.Write = tblUserAccess.Write;
                    existing.Delete = tblUserAccess.Delete;
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }


    }
}
