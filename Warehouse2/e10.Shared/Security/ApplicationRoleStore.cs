using System;
using System.Linq;
using System.Threading.Tasks;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Security
{
    public class ApplicationRoleStore : IRoleStore<Role>
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRoleStore(ApplicationDbContext context)
            : base()
        {
            _context = context;
        }

        public Task CreateAsync(Role role)
        {
            if(role == null) { throw new ArgumentNullException("RoleIsRequired"); }
            _context.Roles.Add(role);
            return _context.SaveChangesAsync();

        }

        public Task DeleteAsync(Role role)
        {
            var roleEntity = _context.Roles.FirstOrDefault(x => x.Id == role.Id);
            if(roleEntity == null) throw new InvalidOperationException("No such role exists!");
            _context.Roles.Remove(roleEntity);
            return _context.SaveChangesAsync();
        }

        public Task<Role> FindByIdAsync(string roleId)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == roleId);
            return Task.FromResult(role);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == roleName);
            return Task.FromResult(role);
        }

        public Task UpdateAsync(Role role)
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}