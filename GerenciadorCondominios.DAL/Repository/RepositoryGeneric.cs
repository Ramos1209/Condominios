using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCondominios.DAL.Repository
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        private readonly CondominioContext _context;

        public RepositoryGeneric(CondominioContext context)
        {
            _context = context;
        }

        public async Task Insert(TEntity entity)
        {
            try
            {

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Insert(List<TEntity> entity)
        {
            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task Excluir(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await GetbyId(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Excluir(string id)
        {
            try
            {
                var entity = await GetbyId(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
               
               return await _context.Set<TEntity>().ToListAsync();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<TEntity> GetbyId(int id)
        {
            try
            {

                return await _context.Set<TEntity>().FindAsync(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<TEntity> GetbyId(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

      
    }
}
