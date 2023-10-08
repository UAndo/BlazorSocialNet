using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSocialNet.Entities;
using Microsoft.Extensions.Configuration;

namespace BlazorSocialNet.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}