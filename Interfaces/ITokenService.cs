using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_api.Models;

namespace todo_api.Interfaces
{
    public interface ITokenService
    {
        string createToken(User user);
    }
}