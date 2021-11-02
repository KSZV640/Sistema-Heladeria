using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Heladeria
{
    class CategoriasBL: Categoria
    {
        internal object categorias;

        public CategoriasBL(): base()
        {

        }
        public DbSet<Categoria> categoria{ get; set; }
    }

    public class Categoria
    {
    }
}
