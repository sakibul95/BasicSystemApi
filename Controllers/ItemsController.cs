using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public Exception Error = new Exception();
        private readonly BasicSystemDbContext _context;

        public ItemsController(BasicSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoTasks
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<TblItem>>> GetItems()
        {
            return await _context.TblItem.OrderBy(x => x.code).ToListAsync();
        }

        [HttpPost]
        public async Task<bool> SaveItemInfo(TblItem TblItem)
        {
            try
            {
                _context.TblItem.Add(TblItem);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }

        [HttpPut]
        public async Task<bool> UpdateItemInfo(TblItem TblItem)
        {
            try
            {
                var item = _context.TblItem.FirstOrDefault(x => x.Id == TblItem.Id);
                if (item != null)
                {
                    item.name = TblItem.name;
                    item.code = TblItem.code;
                    item.defaultCost = TblItem.defaultCost;
                    item.defaultPrice = TblItem.defaultPrice;
                    item.descriptions = TblItem.descriptions;
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }

        [HttpGet("{Id}"), Authorize]
        public TblItem GetItemInfoById(string Id)
        {
            try
            {
                return _context.TblItem.FirstOrDefault(x => x.Id == int.Parse(Id));
            }
            catch (Exception ex) { Error = ex; return null; }

        }

        [HttpDelete("{Id}"), Authorize]
        public bool DeleteItem(string Id)
        {
            try
            {
                var user = _context.TblItem.FirstOrDefault(x => x.Id == int.Parse(Id));
                _context.TblItem.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }
    }
}
