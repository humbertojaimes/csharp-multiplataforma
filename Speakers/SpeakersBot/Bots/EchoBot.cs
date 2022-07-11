// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EchoBot .NET Template version v4.15.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace EchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly ILogger<EchoBot> _logger;
        private readonly IConfiguration _configuration;
        SpeakersApiClient.SpeakersApiClient _speakersApiClient;


        public EchoBot(ILogger<EchoBot> logger, IConfiguration configuration, SpeakersApiClient.SpeakersApiClient speakersApiClient)
            => (_configuration, _logger, _speakersApiClient) = (configuration, logger,speakersApiClient);

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var id = turnContext.Activity.Text;

            //SpeakersApiClient.SpeakersApiClient speakersApiClient = new(_logger, _configuration);

            Speaker result = await _speakersApiClient.GetById(id);

            var replyText = $"Speaker: {result.Name}";
            HeroCard heroCard = new()
            {
                Title = result.Name,
                Images = new List<CardImage> { new(result.PhotoUrl) }
            };

            var reply = MessageFactory.Attachment(heroCard.ToAttachment());
            await turnContext.SendActivityAsync(reply, cancellationToken);

        }
    

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hola y bienvenido!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}

