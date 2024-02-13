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
        public DbSet<BancoEstado> BancoEstado => Set<BancoEstado>();
        public DbSet<AceptadoEstado> AceptadoEstado => Set<AceptadoEstado>();
        public DbSet<ValidacionEstado> ValidacionEstado => Set<ValidacionEstado>();
        public DbSet<RegistroEstado> RegistroEstado => Set<RegistroEstado>();
        public DbSet<Tipo> Tipo => Set<Tipo>();

        

    }
}
/* 
 comando para migraciones: 
 dotnet ef migrations add NameMigration
 dotnet ef database update  

*/