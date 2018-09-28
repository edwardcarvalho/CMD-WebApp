using CMD.Model.Models;
using CMD.Service.BaseRepository;
using System;
using System.Linq.Expressions;

namespace CMD.Service.Repositories.UsuariosRepository
{
    public interface IUsuariosService : IRepository<Usuarios>
    {
        Usuarios GetUserByGuid(string guid);

        Usuarios Obter(string guid, params Expression<Func<Usuarios, object>>[] navigationProperties);

    }
}