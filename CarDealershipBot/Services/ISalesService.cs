using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Services
{
    interface ISalesService
    {
        Task Service(IDialogContext context, IAwaitable<object> result);
    }

    class SalesService : ISalesService
    {
        async Task ISalesService.Service(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;       

            // return our reply to the user
            await context.PostAsync($"Please visit our location.  Our sales representatives are happy to serve you.");
        }
    }
}
