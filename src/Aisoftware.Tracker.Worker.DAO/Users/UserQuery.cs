namespace Aisoftware.Tracker.Worker.DAO.Users;

public struct UserQuery
{
    internal static string FindAll => (
        @"select u.id, 
            u.name, 
            u.email 
        from public.users as u"
    );

    internal static string FindById  => (
        @"select u.id, 
            u.name, 
            u.email 
        from public.users as u
        where u.id = @id"
    );

    internal static string Save => (
        @"insert into public.users (
            id, 
            name, 
            email
        )
        values (
            @id,
            @name,
            @email                 
        )"
    );
}
