using REST_ServiceProject.Models;
using System.Collections.Generic;

namespace REST_ServiceProject
{
    /// <summary>
    /// Interface IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Enumerable list of users
        /// </summary>
        IEnumerable<User> Users { get; }

        /// <summary>
        /// Method to add users to enumberable list
        /// </summary>
        /// <param name="user"></param>
        void Add(User user);

        /// <summary>
        /// Method to Update user in enumerable list by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Update(int userId, User user);

        /// <summary>
        /// Method to delete user from enumerable list
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Delete(int userId);
    }

    /// <summary>
    /// Class UserRepository implements interface IUserRepository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        private static int _userId = 1;

        /// <summary>
        /// Implement Property Users
        /// </summary>
        public IEnumerable<User> Users
        {
            get
            {
                return _users;
            }
        }

        /// <summary>
        /// Implement Method Add
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            user.Id = _userId++;
            user.DateAdded = DateTime.Now;
            _users.Add(user);
        }

        /// <summary>
        /// Implement Method Update
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        public bool Update(int userId, User updatedUser)
        {
            var user = _users.FirstOrDefault(t => t.Id == userId);

            if (user != null)
            {
                user.Email = updatedUser.Email;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Implement Method Delete
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Delete(int userId)
        {
            var userDeleted = _users.RemoveAll(t => t.Id == userId);
            return userDeleted > 0;
        }
    }
}
