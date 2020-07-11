using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Repository
{
    public class UserRespository: GenericRepository, IUserRepository
    {
        public readonly DataContext _dataContext;
        public UserRespository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

    
        public async Task<PagedList<User>> GetAll(UserParams userParams)
        {
            var users = _dataContext.Users.Include(s=>s.Photos)
                                          .OrderByDescending(s => s.LastActive).AsQueryable();

            users = users.Where(u => u.Userid != userParams.UserId);
            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.UserLikes)
            {
                var userLikes = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(i => userLikes.Contains(i.Userid));
            }

            if (userParams.UserIsLiked)
            {
                var userIsLiked = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(u => userIsLiked.Contains(u.Userid));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 100)
            {
                var minDate = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDate = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDate && u.DateOfBirth <= maxDate);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                       users = users.OrderByDescending( u => u.Created);
                       break;
                    default:
                       users = users.OrderByDescending(u => u.LastActive);
                       break;   
                }
            }

            return await PagedList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }

            

        public async Task<IEnumerable<Photo>> GetPhotos(int userId)
            => await _dataContext.Photos.Where(s=>s.UserId == userId).ToListAsync();

        public async Task<User> GetUser(int id)
            => await _dataContext.Users.Include(s=>s.Photos).Include(s=>s.UserIsLiked).FirstOrDefaultAsync(s=>s.Userid==id);
       
        public async Task<Photo> GetPhoto(int id)
            =>await _dataContext.Photos.FirstOrDefaultAsync(s=>s.id==id);
          
        
        public async Task<Photo> GetMainPhotoForUser(int userId)
            =>await _dataContext.Photos.Where(s=>s.UserId == userId).FirstOrDefaultAsync(s=>s.MainPhoto == true);

    
        public async Task<Like> GetLike(int userId, int recipientId)
            => await _dataContext.Likes.FirstOrDefaultAsync(u => u.UserLikesId == userId && u.UserIsLikedId == recipientId);

       


        private async Task<IEnumerable<int>> GetUserLikes(int id, bool userLikes)
        {
            var user = await _dataContext.Users
                     .Include(x => x.UserLikes)
                     .Include(x => x.UserIsLiked)
                     .FirstOrDefaultAsync(u => u.Userid == id);

            if (userLikes)
            {
                return user.UserLikes.Where(s=>s.UserIsLikedId==id).Select(s=>s.UserLikesId);

            }
            else
            {
                return user.UserIsLiked.Where(s=>s.UserLikesId==id).Select(u=>u.UserIsLikedId);
            }
        }

        public async Task<Message> GetMessage(int id)
            =>await _dataContext.Messages.FirstOrDefaultAsync(s=>s.Id == id);
       

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _dataContext.Messages.Include(s=>s.Sender).ThenInclude(z=>z.Photos)
                                                .Include(z=>z.Recipient).ThenInclude(z=>z.Photos).AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox" : 
                    messages = messages.Where(z =>z.RecipientId == messageParams.UserId && z.RecipienDelete == false);
                    break;
                case "Outbox" :
                    messages = messages.Where(z => z.SenderId == messageParams.UserId && z.SenderDelete == false);
                    break;    
                default:
                    messages = messages.Where(z => z.RecipientId == messageParams.UserId && z.IsRead == false && z.RecipienDelete == false);
                    break;    
            }

            messages = messages.OrderBy(z => z.DateSend);

            return await PagedList<Message>.CreateListAsync(messages, messageParams.PageNumber, messageParams.PageSize);

        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
             var messages = await _dataContext.Messages.Include(s=>s.Sender).ThenInclude(z=>z.Photos)
                                                 .Include(z=>z.Recipient).ThenInclude(z=>z.Photos)
                                                 .Where(z => z.RecipientId == userId && z.SenderId == recipientId && z.RecipienDelete == false ||
                                                        z.RecipientId == recipientId && z.SenderId == userId && z.SenderDelete == false)
                                                 .OrderByDescending(z => z.DateSend)
                                                 .ToListAsync();     
            return messages;                                       
        }
    }

}
