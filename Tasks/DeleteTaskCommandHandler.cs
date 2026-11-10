using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class DeleteTaskCommandHandler(ITaskStorage taskStorage, UserSession userSession, Logger logger)
{
    public bool DeleteTask(int id)
    {
        bool result = taskStorage.RemoveTask(id, userSession.CurrentUser!.Id);

        if (result)
        {
            logger.LogInfo($"Пользователь {userSession.CurrentUser.Name} удалил задачу {id}");
        }
        
        return result;
    }
}