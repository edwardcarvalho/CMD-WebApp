using CMD.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.AccountService
{
    public interface IAccountService
    {
        Usuarios GetUserById(long id);

        Usuarios GetUserByCredentials(string login, string senha);

        List<Usuarios> Consultar();

        List<Usuarios> Consultar(Expression<Func<Usuarios, bool>> filtro);
    }
}
