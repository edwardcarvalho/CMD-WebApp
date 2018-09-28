using CMD.Data.EntityContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CMD.Service.BaseRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Obtem uma entidade através do ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Obter(long id)
        {
            T item = null;
            using (var context = new EfContext())
            {
                var ctx = context.Set<T>();
                item = ctx.Find(id);
            }
            return item;
        }

        public T Obter(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new EfContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery.AsNoTracking().FirstOrDefault(where);
            }
            return item;
        }

        /// <summary>
        /// Obtem uma lista de objetos sem parametros
        /// </summary>
        /// <returns></returns>
        public List<T> Consultar()
        {
            List<T> list;
            using (var context = new EfContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        /// <summary>
        /// Obtem uma lista de objetos com parametros incluindo propriedades de navegação (FK)
        /// </summary>
        /// <param name="where">variavel contendo a condição de busca</param>
        /// <param name="navigationProperties">variavel contendo a lista de entidades a serem incluidas na busca</param>
        /// <returns></returns>
        public List<T> Consultar(Func<T, bool> where, Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new EfContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery.AsNoTracking().Where(where).ToList<T>(); //Apply where clause
            }
            return list;
        }

        /// <summary>
        /// Adicionar um novo objeto no banco de dados.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Adicionar(T entity)
        {
            bool salvou = false;
            using (var context = new EfContext())
            {
                try
                {
                    context.Entry(entity).State = EntityState.Added;
                    context.SaveChanges();
                    salvou = true;
                }
                catch (Exception ex)
                {
                    salvou = false;
                }
            }

            return salvou;
        }

        /// <summary>
        /// Atualiza um objeto existente no banco de dados.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Atualizar(T entity)
        {
            bool salvou = false;
            using (var context = new EfContext())
            {
                try
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    salvou = true;
                }
                catch (Exception ex)
                {
                    salvou = false;
                }
            }
            return salvou;
        }

        /// <summary>
        /// Remove um objeto existente no banco de dados.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Excluir(T entity)
        {
            bool salvou = false;
            using (var context = new EfContext())
            {
                try
                {
                    context.Entry(entity).State = EntityState.Deleted;
                    context.SaveChanges();
                    salvou = true;
                }
                catch (Exception ex)
                {
                    salvou = false;
                }
            }
            return salvou;
        }

        
    }
}