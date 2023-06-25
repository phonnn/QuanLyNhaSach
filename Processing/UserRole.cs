using Microsoft.AspNetCore.Authorization;
using QuanLyNhaSach.Entities;
using System;

namespace QuanLyNhaSach.Processing
{
	public class UserRoleProcessing : Processing<UserRole>, IUserRole
	{
		private readonly IBase<Role> _role = (IBase<Role>)Injector.Injector.GetProcessing<RoleProcessing>();
		public async Task Add(User user, List<string> roleIds)
		{
			if (user == null || roleIds.Count == 0)
			{
				throw new Exception("Invalid input");
			}

			List<UserRole> roles = new List<UserRole>();
			foreach (var roleId in roleIds) {
				Role role = await _role.SearchById(roleId);
				if (role == null)
				{
					throw new Exception("Role not found");
				}

				roles.Add(new UserRole()
				{
					User = user.Id,
					Role = role.Id
				});
			}

            await _model.BatchAddAsync(roles);
        }

		public async Task Update(User user, List<string> roleIds)
		{
			await DeleteByUser(user);
			await Add(user, roleIds);
		}

		public async Task DeleteByUser(User user)
		{
			if (user == null)
			{
				throw new Exception("Invalid input");
			}

			List<UserRole> items = (List<UserRole>)user.UserRoles;
			if (items.Count > 0)
			{
				await _model.BatchDeleteAsync(items);
			}

			user.UserRoles.Clear();
		}
	}
}