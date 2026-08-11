using madOrm.Console;
using madOrm.Library.DbContext;
using madOrm.Library.Interfaces.DatabaseProviders;

var context = new MadContext(opt =>
{
    opt.UseSqlServer("localhost");
    opt.AddEntity<User>();
});

var users = context.ReadList<User>();

Console.ReadLine();