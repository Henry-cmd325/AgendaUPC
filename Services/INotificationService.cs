using AgendaUpc.Models.Responses;
using AgendaUpc.Models.Requests;

namespace AgendaUpc.Services;

public interface INotificationService
{
    public ServerResponse<NotificationResponse> DeleteNotification(int idUsuario, int idNotication);
    public ServerResponse<List<NotificationResponse>> GetAllNotifications(int idUsuario);
    public ServerResponse<NotificationResponse> GetNotification(int idUsuario, int idNotication);
    public ServerResponse<NotificationResponse> PutNotification(int idUsuario, int idNotication, NotificationRequest request);
    public ServerResponse<NotificationResponse> PutNotified(int idNotication);
    public ServerResponse<List<NotificationResponse>> CheckNotifications(int idUsuario);
}