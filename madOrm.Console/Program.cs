using madOrm.Console;
using madOrm.Library.DbContext;
using madOrm.Library.Infrastructure.Configuration;
using madOrm.Library.Interfaces.DatabaseProviders;

var context = new MadContext(opt =>
{
    opt.UseSqlServer("Server=localhost;Database=ORM;User Id=sa;Password=123456;TrustServerCertificate=True;");
    //opt.AddEntity<User>();
    opt.AddEntityConfiguration(new UserConfiguration());
});

var user = new User()
{
    Id = new Random().Next(2, 101),
    UserName = "Test"
};

var results = await context.Insert(user);

var users = await context.ReadList<User>();

Console.ReadLine();

class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Build(EntityTypeBuilder<User> configuration)
    {
        configuration.ToColumn(i => i.Id, "id");
        configuration.ToColumn(i => i.UserName, "username");
    }
}