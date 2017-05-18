using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Bot_Application1.Services;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // direct to corresponding the interface
            if (activity.Text.ToUpper() == "SALES")
            {
                ISalesService obj = new SalesService();
                await obj.Service(context, result);
            }
            else if (activity.Text.ToUpper() == "SERVICE")
            {
                IServiceService obj = new ServiceService();
                await obj.Service(context, result);
            }
            else if (activity.Text.ToUpper() == "PARTS")
            {
                IPartsService obj = new PartsService();
                await obj.Service(context, result);
            }
            else if (activity.Text.ToUpper() == "MENU")
            {
                // Main menu
                Activity reply = activity.CreateReply($"Please choose one of the departments you want to reach:");

                reply.Type = ActivityTypes.Message;
                reply.TextFormat = TextFormatTypes.Plain;

                reply.SuggestedActions = new SuggestedActions()
                {
                    Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "Sales", Type=ActionTypes.ImBack, Value="SALES" },
                        new CardAction(){ Title = "Service", Type=ActionTypes.ImBack, Value="SERVICE" },
                        new CardAction(){ Title = "Parts", Type=ActionTypes.ImBack, Value="PARTS" }
                    }
                };
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"You have entered an invalid option.  Please try again or type \"Menu\" to display the menu:");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}