using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models;

namespace DBLayer
{
    public class FlyCompanyDBInstance
    {
        readonly FlyCompanyDBContext dbContext;

        public FlyCompanyDBInstance()
        {
            FlyCompanyDBContextBuilder builder = new ();
            dbContext = builder.CreateDbContext([]);
        }
        public Salt? GetSalt(string _login)
        {
            return dbContext.Salts.Where(s => s.Login == _login).FirstOrDefault();
        }

        public void AddSalt(Salt _salt)
        {
            dbContext.Salts.Add(new()
            {
                Login = _salt.Login,
                SaltString = _salt.SaltString,
            });
            dbContext.SaveChanges();
        }
        // new code here

        // nothing special
    }
}
