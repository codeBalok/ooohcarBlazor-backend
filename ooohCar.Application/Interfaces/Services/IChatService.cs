using ooohCar.Application.Models.Chat;
using ooohCar.Application.Responses.Identity;
using ooohCar.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}