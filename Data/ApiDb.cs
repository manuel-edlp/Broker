using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Data
{
    public class ApiDb : DbContext
    {
        public ApiDb(DbContextOptions<ApiDb> options) : base(options)
        {

        }

        public DbSet<Banco> Banco => Set<Banco>();
        public DbSet<Cuenta> Cuenta => Set<Cuenta>();
        public DbSet<Transaccion> Transaccion => Set<Transaccion>();
        public DbSet<EstadoBanco> EstadoBanco => Set<EstadoBanco>();
        public DbSet<EstadoTransaccion> EstadoTransaccion => Set<EstadoTransaccion>();
        public DbSet<TipoTransaccion> TipoTransaccion => Set<TipoTransaccion>();

        

    }
}
/* 
 comando para migraciones: 
 dotnet ef migrations add NameMigration
 dotnet ef database update  

*/