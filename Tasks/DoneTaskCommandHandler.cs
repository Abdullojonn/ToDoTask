using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class DoneTaskCommandHandler(ITaskStorage taskStorage, UserSession userSession, Logger logger)
{
    public bool DoneTask(int id)
    {
        bool result = taskStorage.Done(id, userSession.CurrentUser!.Id);

        if (result)
        {
            logger.LogInfo($"Пользователь {userSession.CurrentUser.Name} отметил задачу {id}");
        }
        
        return result;
    }
}