using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class DeleteTaskCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext, Logger logger)
{
    public async Task<bool> DeleteTask(int id)
    {
        var task = await taskStorage.GetById(id);
        if (task == null)
        {
            throw new ArgumentException($"Задача с id {id} не найдена");
        }

        if (task.UserId != userContext.GetRequiredUserId())
        {
            throw new ArgumentException($"Задача с id {id} не принадлежит пользователю {userContext.GetRequiredUserId()}");
        }

        bool result = taskStorage.RemoveTask(task.Id);

        if (result)
        {
            logger.LogInfo($"Пользователь {userContext.GetRequiredUserId()} удалил задачу {id}");
        }
        
        return result;
    }
}