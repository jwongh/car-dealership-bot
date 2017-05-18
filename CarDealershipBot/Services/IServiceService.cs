using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Services
{
    interface IServiceService
    {
        Task Service(IDialogContext context, IAwaitable<object> result);
    }

    class ServiceService : IServiceService
    {
        async Task IServiceService.Service(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            Activity reply = activity.CreateReply($"Please access below to book your service appointment:");

            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction(){ Title = "Visit Service", Type= "openUrl", Value= $"http://cardealership.xyz/Service", }
                }
            };

            await context.PostAsync(reply);
        }
    }
}
