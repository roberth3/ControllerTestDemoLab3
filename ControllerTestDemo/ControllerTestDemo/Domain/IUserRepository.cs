using System.Collections.Generic;

namespace ControllerTestDemo.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByUsername(string target);
        void Save(User user);
    }
}
