using CMD.Data.EntityContext;
using CMD.Model.Models;
using CMD.Service.AccountService;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CMD.Service.AccountControllerService
{
    public class AccountService : IAccountService
    {
        public List<Usuarios> Consultar()
        {
            List<Usuarios> users = new List<Usuarios>();
            try
            {
                using (var ctx = new EfContext())
                {
                    users.AddRange(
                        ctx.Usuario
                        .Include("Funcionario")
                        .Include("Perfil")
                        .Where(c => c.Ativo).ToList()
                        );
                }

                return users;
            }
            catch (Exception ex)
            {
                return users;
            }
        }

        public List<Usuarios> Consultar(Expression<Func<Usuarios, bool>> filtro)
        {
            List<Usuarios> users = new List<Usuarios>();
            try
            {
                using (var db = new EfContext())
                {
                    users.AddRange(
                        db.Usuario
                        .Include("Funcionario")
                        .Include("Perfil")
                        .Where(filtro)
                        );
                }

                return users;
            }
            catch (Exception ex)
            {
                return users;
            }
        }

        public Usuarios GetUserByCredentials(string login, string senha)
        {
            Usuarios user = null;
            try
            {
                using (var ctx = new EfContext())
                {
                    user = ctx.Usuario.Where(c => c.Login == login && c.Senha == senha).First();
                }

                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        public Usuarios GetUserById(long id)
        {
            Usuarios user = null;

            using (var ctx = new EfContext())
            {
                user = ctx.Usuario.Find(id);
                ctx.Entry(user).Reference(c => c.Funcionario).Load();
                ctx.Entry(user).Reference(c => c.Perfil).Load();
            }

            return user;
        }
    }
}
