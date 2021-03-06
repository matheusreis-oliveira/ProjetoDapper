using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using ProjetoDapper.Models;

namespace ProjetoDapper.Repositories
{
    public class UserRepository : Repository<UserModel>
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection) : base(connection) => _connection = connection;

        public List<UserModel> GetWithRole()
        {
            var query = @"
                SELECT usr.*, rl.*
                FROM [User] usr
                    LEFT JOIN [UserRole] usrrl ON usrrl.UserId = usr.Id
                    LEFT JOIN [Role] rl ON usrrl.RoleId = rl.Id";

            var users = new List<UserModel>();
            var items = _connection.Query<UserModel, RoleModel, UserModel>
            (
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;
                        if (role != null)
                            usr.Roles.Add(role);
                        users.Add(usr);
                    }
                    else
                        usr.Roles.Add(role);

                    return user;
                }, splitOn: "Id"
            );
            return users;
        }
    }
}