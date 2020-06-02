using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;


namespace Fivet.Dao
{
    public class FivetContext : DbContext
    {
        // Connection to the database
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Control> Controles { get; set; }
        public DbSet<Examen> Examenes { get; set; }

        // Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Using SQLite
            optionsBuilder.UseSqlite("Data Source = fivet.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        // Create the ER from Entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Update the model
                 modelBuilder.Entity<Persona>(per =>
           {
        
               per.Property(p => p.email).IsRequired();
               per.HasIndex(p => p.email).IsUnique();
               // Index in personal data
               per.Property(p => p.rut).IsRequired();
               per.HasIndex(p => p.rut).IsUnique();
               // Personal data
               per.Property(p => p.nombre).IsRequired();
               per.Property(p => p.apellido).IsRequired();
               per.Property(p => p.direccion).IsRequired();

            });

            // Insert the data
            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    uid = 1,
                    nombre = "Beatriz",
                    apellido = "Alvarez",
                    direccion = "Calle #2346",
                    email = "Bar011@alumnos.ucn.cl"
                }
            );

             modelBuilder.Entity<Control>(con =>
            {
                con.HasKey(c=> c.uid);
                con.Property(c=> c.fecha).IsRequired();
                con.Property(c=> c.temperatura).IsRequired();
                con.Property(c=> c.peso).IsRequired();
                con.Property(c=> c.altura).IsRequired();
                con.Property(c=> c.diagnostico).IsRequired();
            });

            modelBuilder.Entity<Control>().HasData(
                new Control()
                {
                    uid = 1,
                    fecha ="12/12/2019",
                    temperatura = 39,
                    peso= 60,
                    altura= 120,
                    diagnostico= "Infección",
                }
            );

            modelBuilder.Entity<Ficha>(fi =>
            {
                fi.HasKey(f=> f.uid);
                fi.Property(f=> f.nombre).IsRequired();
                fi.Property(f=> f.especie).IsRequired();
                fi.Property(f=> f.fechaNacimiento).IsRequired();
                fi.Property(f=> f.sexo).IsRequired();
                fi.Property(f=> f.color).IsRequired();
                fi.Property(f=> f.tipoPaciente).IsRequired();
                
            });

            modelBuilder.Entity<Ficha>().HasData(
                new Ficha()
                {
                    uid = 1,
                    numero =1,
                    nombre = "Eevee",
                    especie ="Perro",
                    fechaNacimiento ="5/05/2015",
                    raza ="Husky",
                    sexo = Sexo.MACHO,
                    color = "Cafe claro",
                    tipoPaciente = TipoPaciente.EXTERNO,
                }
            );

            modelBuilder.Entity<Examen>(ex =>
            {
                ex.HasKey(e=> e.uid);
                ex.Property(e=> e.nomExamen).IsRequired();
                ex.Property(e=> e.feExamen).IsRequired();
            }
            
            );


            modelBuilder.Entity<Examen>().HasData(
                new Examen()
                {
                    uid = 1,
                    nomExamen ="Radiografia",
                    feExamen = "24/09/2019",
                }
            );

       }

    }
}