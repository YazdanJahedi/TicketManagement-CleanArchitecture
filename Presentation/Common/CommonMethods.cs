using Application.Repository;

namespace Presentation.Common
{
    public static class CommonMethods
    {
        public static bool CalculateIsCheckedField(long ticketId, 
                                                   IUsersRepository usersRepository,
                                                   IMessagesRepository messagesRepository )
        {
            var message = messagesRepository.FindLastMessageByTicketId(ticketId);
            if (message == null) return false;
            var user = usersRepository.FindByEmail(message.CreatorEmail);
            if (user == null) return false;
            return user.Role.Equals("Admin");
        }
    }
}
