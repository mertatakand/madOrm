using madOrm.Console;
using madOrm.Library.DbContext;
using madOrm.Library.Interfaces.DatabaseProviders;

var context = new MadContext(opt =>
{
    opt.UseSqlServer("Server=localhost;Database=ORM;User Id=sa;Password=123456;TrustServerCertificate=True;");
    opt.AddEntity<User>();
});

var users = await context.ReadList<User>();
await context.ReadList<User>();

Console.ReadLine();