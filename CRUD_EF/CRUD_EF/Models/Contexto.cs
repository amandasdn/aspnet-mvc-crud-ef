using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CRUD_EF.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}