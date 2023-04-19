using Microsoft.EntityFrameworkCore;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.User;

namespace VerticalSliceArch.Features.User
{
    public class UserServices : IUserServices
    {
        private readonly DataContext _context;
        public UserServices(DataContext context)
        {
            _context = context;
        }

        public void AddUser(Users user)
        {
            _context.User.Add(user);
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }
        public async Task<Users> GetUserById(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Users>> GetUserByStatus(bool status)
        {
            return await _context.User.Where(x => x.IsActive == status).ToListAsync();
        }
        public async Task<Users> GetUserByName(string name)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<Users> GetuserByUsername(string username)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
