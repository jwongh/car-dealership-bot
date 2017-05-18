using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Services
{
    interface IPartsService
    {
        Task Service(IDialogContext context, IAwaitable<object> result);
    }

    class PartsService : IPartsService
    {
        async Task IPartsService.Service(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            Activity reply = activity.CreateReply($"Parts is not currently available through Bot. Please call our Parts department below");

            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "(123) 456-7890", Type="call", Value="tel:1234567890" }
                    }
            };

            await context.PostAsync(reply);
        }
    }
}
