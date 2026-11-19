using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class DoneTaskCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext, Logger logger)
{
    public async Task<bool> DoneTask(int id)
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

        bool result = taskStorage.Done(task.Id);

        if (result)
        {
            logger.LogInfo($"Пользователь {userContext.GetRequiredUserId()} отметил задачу {id}");
        }

        return result;
    }
}