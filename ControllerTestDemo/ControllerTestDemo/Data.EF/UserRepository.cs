
using System.Collections.Generic;
using System.Linq;
using ControllerTestDemo.Domain;

namespace ControllerTestDemo.Data.EF
{
    public class UserRepository : IUserRepository
    {
        private GCUToursCeContext context = new GCUToursCeContext();

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User GetByUsername(string target)
        {
            return context.Users.SingleOrDefault(u => u.username == target);
        }

        public void Save(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}