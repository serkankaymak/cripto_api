using Application.Dtos.Hubs;

namespace api.Hubs;
public interface ICriptoAnalysesHub
{
    public Task SendAnalyses();
    public Task SendMessage(MessageDto message);
}

