using CMD.Data.EntityContext;
using CMD.Model.Models;
using CMD.Service.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.Repositories.UsuariosRepository
{
    public class UsuariosService : Repository<Usuarios>, IUsuariosService
    {
        public Usuarios GetUserByGuid(string guid)
        {
            Usuarios user = null;
            try
            {
                using (var ctx = new EfContext())
                {
                    user = ctx.Users.Where(c => c.Id == guid).First();
                }

                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        public Usuarios Obter(string guid, params Expression<Func<Usuarios, object>>[] navigationProperties)
        {
            Usuarios user = null;
            try
            {
                using (var ctx = new EfContext())
                {
                    user = ctx.Users.Where(c => c.Id == guid).First();

                    if (user != null)
                    {
                        foreach (var nav in navigationProperties)
                        {
                            ctx.Entry(user).Reference(nav).Load();
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }
    }
}
