using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Helpers;

public static class ControllerHelper
{
    public static ActionResult HandleCreate(ControllerBase controller, Func<int> createFunc, string routePrefix)
    {
        return HandleErrors(controller, () =>
        {
            int id = createFunc();
            return controller.Created($"{routePrefix}/{id}", id);
        });
    }

    public static ActionResult HandleUpdate(ControllerBase controller, Func<bool> updateFunc)
    {
        return HandleErrors(controller, () =>
        {
            bool isUpdated = updateFunc();
            if (isUpdated)
            {
                return controller.Ok();
            }
            else
            {
                return controller.NotFound();
            }
        });
    }

    private static ActionResult HandleErrors(ControllerBase controller, Func<ActionResult> callback)
    {
        try
        {
            return callback();
        }
        catch (DbUpdateException)
        {
            return controller.Conflict("An entity with the same unique value already exists.");
        }
        catch (Exception)
        {
            return controller.StatusCode(500, "An unexpected error occurred.");
        }
    }
}
