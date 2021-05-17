using BasicSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(BasicSystemDbContext context, IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpGet("{User_id}/{pass}")]
        public Login Login(string User_id, string pass)
        {
            Login User = new Login();
            string query = "select ID,User_id,Name,pass,role from user_info where user_id = '" + User_id + "' and pass = '" + pass + "'";

            string sqlDataSource = _configuration.GetConnectionString("BasicSystemCon");
            SqlDataReader rdr;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand mycommand = new SqlCommand(query, con))
                {
                    rdr = mycommand.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr["ID"] != DBNull.Value)
                            User.ID = Guid.Parse(rdr["ID"].ToString());

                        if (rdr["User_id"] != DBNull.Value)
                            User.User_id = rdr["User_id"].ToString();

                        if (rdr["Name"] != DBNull.Value)
                            User.Name = rdr["Name"].ToString();

                        if (rdr["role"] != DBNull.Value)
                            User.role = rdr["role"].ToString();
                    }
                    rdr.Close();
                    con.Close();
                }
            }

            if (User.User_id != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                User.token = tokenString;
            }

            List<TblUserAccess> TblUserAccessList = new List<TblUserAccess>(); 
            string query1 = "select * from tblUserAccess where user_id = '" + User_id + "'";
            SqlDataReader rdr1;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand mycommand = new SqlCommand(query1, con))
                {
                    rdr1 = mycommand.ExecuteReader();
                    while (rdr1.Read())
                    {
                        TblUserAccess TblUserAccess = new TblUserAccess();
                        if (rdr1["Id"] != DBNull.Value)
                            TblUserAccess.Id = Convert.ToInt32(rdr1["Id"].ToString());

                        if (rdr1["user_id"] != DBNull.Value)
                            TblUserAccess.user_id = rdr1["user_id"].ToString();

                        if (rdr1["Menu"] != DBNull.Value)
                            TblUserAccess.Menu = rdr1["Menu"].ToString();

                        if (rdr1["Read"] != DBNull.Value)
                            TblUserAccess.Read = Convert.ToBoolean(rdr1["Read"].ToString());

                        if (rdr1["Write"] != DBNull.Value)
                            TblUserAccess.Write = Convert.ToBoolean(rdr1["Write"].ToString());

                        if (rdr1["Delete"] != DBNull.Value)
                            TblUserAccess.Delete = Convert.ToBoolean(rdr1["Delete"].ToString());

                        TblUserAccessList.Add(TblUserAccess);
                    }
                    rdr1.Close();
                    con.Close();
                }
            }
            User.TblUserAccess = TblUserAccessList;

            return User;
        }
    }
}
