using Microsoft.EntityFrameworkCore;

namespace Worktime_Manger.Blazor.Models;

public class WorktimeManagerContext
{
    public class UserContext : DbContext {

        public UserContext(DbContextOptions<UserContext> options): base(options) 
        {

        }
    }
}