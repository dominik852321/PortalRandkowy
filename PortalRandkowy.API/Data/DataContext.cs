using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) {}

        public DbSet<Value> Values {get; set;}

        public DbSet<User> Users {get; set;}
        public DbSet<Photo> Photos {get; set;}
        public DbSet<Like> Likes {get; set;}
        public DbSet<Message> Messages {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>().HasKey(x => new { x.UserLikesId, x.UserIsLikedId });

            builder.Entity<Like>().HasOne(x => x.UserIsLiked)
                                  .WithMany(a => a.UserLikes)
                                  .HasForeignKey(u => u.UserIsLikedId)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>().HasOne(x => x.UserLikes)
                                  .WithMany(a => a.UserIsLiked)
                                  .HasForeignKey(u => u.UserLikesId)
                                  .OnDelete(DeleteBehavior.Restrict);   

            builder.Entity<Message>().HasOne(x=> x.Sender)
                                     .WithMany(x=> x.MessagesSend)
                                     .OnDelete(DeleteBehavior.Restrict);  

            
            builder.Entity<Message>().HasOne(x=> x.Recipient)
                                     .WithMany(x=> x.MessagesRecived)
                                     .OnDelete(DeleteBehavior.Restrict);    
                                     
                                                                            
                                    
        }

    }
}