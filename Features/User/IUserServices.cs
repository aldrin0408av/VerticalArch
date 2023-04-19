using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.User
{
    public interface IUserServices
    {
        Task<IEnumerable<Users>> GetAllUsers();
        void AddUser(Users user);
        Task<Users> GetUserById(int id);
        Task<IEnumerable<Users>> GetUserByStatus(bool status);
        Task<Users> GetUserByName(string name);
        Task<Users> GetuserByUsername(string username);
    }
}
