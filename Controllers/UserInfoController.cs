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
    public class UserInfoController : ControllerBase
    {
        public Exception Error = new Exception();
        private readonly BasicSystemDbContext _context;

        public UserInfoController(BasicSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoTasks
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfos()
        {
            return await _context.UserInfo.OrderBy(x => x.role).ToListAsync();
        }

        [HttpGet("userList/{role}"), Authorize]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfosByRole(string role)
        {
            return await _context.UserInfo.Where(x => x.role == role).ToListAsync();
        }

        [HttpPost]
        public async Task<bool> PostUserInfo(UserInfo userInfo)
        {
            try
            {
                userInfo.ID = Guid.NewGuid();
                _context.UserInfo.Add(userInfo);
                if (userInfo.role == "Regular")
                {
                    TblUserAccess TblUserAccess = new TblUserAccess();
                    TblUserAccess.Menu = "Items"; // would have used another table to integrate it but unable for shortest time
                    TblUserAccess.Read = true;
                    TblUserAccess.Write = true;
                    TblUserAccess.Delete = true;
                    TblUserAccess.user_id = userInfo.User_id;
                    _context.TblUserAccess.Add(TblUserAccess);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }

        [HttpPut]
        public async Task<bool> UpdateUserInfo (UserInfo userInfo)
        {
            try
            {
                if (userInfo.ID != null)
                {
                    var user = _context.UserInfo.FirstOrDefault(x => x.ID == userInfo.ID);
                    if (user != null)
                    {
                        user.Name = userInfo.Name;
                        user.role = userInfo.role;
                        await _context.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }

        [HttpGet("{Id}"), Authorize]
        public UserInfo GetUserInfoById(string Id)
        {
            try
            {
                return _context.UserInfo.FirstOrDefault(x => x.ID == Guid.Parse(Id));
            }
            catch (Exception ex) { Error = ex; return null; }
            
        }

        [HttpDelete("{Id}"), Authorize]
        public bool DeleteUser(string Id)
        {
            try
            {
                var user = _context.UserInfo.FirstOrDefault(x => x.ID == Guid.Parse(Id));
                var perm = _context.TblUserAccess.FirstOrDefault(x => x.user_id == user.User_id);
                //
                _context.TblUserAccess.Remove(perm);
                _context.UserInfo.Remove(user);
                //
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { Error = ex; return false; }
        }


    }
}
