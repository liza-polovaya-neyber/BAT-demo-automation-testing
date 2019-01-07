﻿using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreated
    {
        public static async Task<UserLoginModel> CreateUserAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            return user;
        }        
    }
}
