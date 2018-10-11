using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using UserList.Models;
using UserList.Models.DatabaseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UserList.Models.TransferModels;
using System.IO;

namespace UserList.Services
{
    public class UserService
    {
        public UserService()
        {
            using (var db = new AppDbContext())
            {
                if (!db.Users.Any())
                {
                    for (var i = 0; i < 1000; i++)
                    {
                        db.Users.AddRange(new User[]
                        {
                        new User {
                            Name = "Ivan Ivanovich Ivanov",
                            Age = 30
                        },
                        new User {
                            Name = "Semyon Semyonovich Semyonov",
                            Age = 25
                        },
                        new User {
                            Name = "Petr Petrovich Petrov",
                            Age = 28
                        },
                        new User {
                            Name = "Dmitry Dmitrievich Dmitriev",
                            Age = 34
                        }
                        });
                    }

                    db.SaveChanges();
                }
            }
        }

        public int GetPageCount()
        {
            var pageSize = 25;
            var count = 0;
            using (var db = new AppDbContext())
            {
                count = db.Users.Count();
            }
            var result = count / pageSize;
            return count % pageSize == 0 ? result : result + 1;
        }

        public string GetUsers(int page)
        {
            var pageSize = 25;
            using (var db = new AppDbContext())
            {
                IQueryable<User> users = db.Users;
                if (page != 0) users = users.Skip(page * pageSize);
                users = users.Take(pageSize);
                var transferUsers = users.Select(u => new UserTransfer
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Age = u.Age
                });
                return Json(transferUsers);
            }
        }

        public async Task<string> AddUser(Stream body)
        {
            var receivedUser = await GetFromBody<UserTransfer>(body);
            using (var db = new AppDbContext())
            {
                var user = new User
                {
                    Name = receivedUser.Name,
                    Age = receivedUser.Age
                };
                db.Users.Add(user);
                db.SaveChanges();
                receivedUser.UserId = user.UserId;
                return Json(receivedUser);
            }
        }

        private string Json(object value) => JsonConvert.SerializeObject(value);

        private async Task<T> GetFromBody<T>(Stream body)
        {
            string content;
            using (var sr = new StreamReader(body))
            {
                content = await sr.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
