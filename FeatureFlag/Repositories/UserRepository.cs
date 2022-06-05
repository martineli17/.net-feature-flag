using _Flagsmith.Models;
using System.Collections.Generic;

namespace _Flagsmith.Repositories
{
    public static class UserRepository
    {
        private static List<User> _users = new();

        public static void Add(User user) => _users.Add(user);
        public static IEnumerable<User> Get() => _users;
    }
}
