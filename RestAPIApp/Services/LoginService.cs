using RestAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Services
{
    public class LoginService
    {
        RestAPIContext db;
        public LoginService(RestAPIContext db)
        {
            this.db = db;
        }
        public User UserLogin(Login lg)
        {
            var response = new Response();
            try
            {
                var user = db.Users.FirstOrDefault(x => x.Email.Equals(lg.UserName) && x.Password.Equals(lg.Password));

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Username / password not matched";
                    //return response;
                }
                response.IsSuccess = true;
                response.Message = "User successfully logged in";
                //response.Result = user;
                return user;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return null;
            }            
            //return response;
        }
    }
}
