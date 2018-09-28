using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.BaseRepository
{
    public interface IRepository<T>
    {
        T Obter(long id);

        T Obter(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        List<T> Consultar();

        List<T> Consultar(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        bool Adicionar(T entity);

        bool Atualizar(T entity);

        bool Excluir(T entity);

    }
}
