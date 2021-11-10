using PawApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Repository
{
    public static class RepositorioUsuario
    {
        public static bool Create(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public static bool Delete(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public static List<Usuario> GetAll()
        {
            using (var context = new PawHouseContext())
            {
                return context.Usuarios.ToList();
            }
        }

        public static IEntityBase GetById()
        {
            throw new NotImplementedException();
        }

        public static bool Update(IEntityBase entity)
        {
            throw new NotImplementedException();
        }
    }
}
