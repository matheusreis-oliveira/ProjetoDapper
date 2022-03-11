using System;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using ProjetoDapper.Models;
using ProjetoDapper.Repositories;

namespace ProjetoDapper
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=MATHEUS;Database=Blog;User ID=sa;Password=123456";

        static void Main(string[] args)
        {
            using var connection = new SqlConnection(CONNECTION_STRING);
            var repository = new Repository<UserModel>(connection);

            // CreateUser(repository);
            // UpdateUser(repository);
            // DeleteUser(repository);
            // ReadUser(repository);
            // ReadUsers(repository);
            //ReadWithRoles(connection);
        }

        private static void CreateUser(Repository<UserModel> repository)
        {
            var user = new UserModel
            {
                Bio = "user test",
                Email = "test@test.com",
                Image = "https://test.com/test.jpg",
                Name = "Test",
                Slug = "user-teste",
                PasswordHash = Guid.NewGuid().ToString()
            };

            repository.Create(user);
        }

        private static void ReadUsers(Repository<UserModel> repository)
        {
            var users = repository.GetAll();//todos
            foreach (var item in users)
                Console.WriteLine(item.Email);
        }

        private static void ReadUser(Repository<UserModel> repository)
        {
            var user = repository.Get(2);//id
            Console.WriteLine(user?.Email);
        }

        private static void UpdateUser(Repository<UserModel> repository)
        {
            var user = repository.Get(2);//id
            user.Email = "user.test@test.com";
            repository.Update(user);

            Console.WriteLine(user?.Email);
        }

        private static void DeleteUser(Repository<UserModel> repository)
        {
            var user = repository.Get(2);//id
            repository.Delete(user);
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.GetWithRole();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles) Console.WriteLine($" - {role.Slug}");
            }
        }
    }
}