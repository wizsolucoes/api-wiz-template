using System;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Wiz.Template.Domain.Interfaces.Bot;
using Wiz.Template.Domain.Models.Bot.Watson;

namespace Wiz.Template.Infra.Bot
{
    public class WatsonBot : IWatsonBot
    {
        private readonly IamAuthenticator _iamAuthenticator;

        private readonly AssistantService _assistantService;

        private readonly string _assistantId;

        private readonly IDistributedCache _distributedCache;
        public WatsonBot(IConfiguration configuration, IDistributedCache distributedCache)
        {
            _iamAuthenticator = new IamAuthenticator(configuration["Watson:Key"]);

            _assistantService = new AssistantService("2019-02-28", _iamAuthenticator);

            _assistantService.SetServiceUrl(configuration["Watson:Url"]);

            _assistantId = configuration["Watson:AssistantId"];

            _distributedCache = distributedCache;
        }
        public ResponseModel SendMessage(MessageRequestModel message)
        {
            var _sessionid = _distributedCache.GetString(message.UserId);
            if (string.IsNullOrWhiteSpace(_sessionid))
            {
                _sessionid = _assistantService.CreateSession(_assistantId).Result.SessionId;

                _distributedCache.SetString(message.UserId, _sessionid, new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(00, 4, 00) });
            }

            var messageObj = new IBM.Watson.Assistant.v2.Model.MessageInput()
            {
                Text = message.Text
            };

            MessageContextSkills skills = new MessageContextSkills();
            MessageContextSkill skill = new MessageContextSkill
            {
                UserDefined = new Dictionary<string, object>()
            };

            if (message.ContextVariables != null)
            {
                foreach (var item in message.ContextVariables)
                {
                    skill.UserDefined.Add(item.Key, item.Value);
                }
            }

            skills.Add("main skill", skill);

            var MessageContext = new IBM.Watson.Assistant.v2.Model.MessageContext()
            {
                Global = new MessageContextGlobal()
                {
                    System = new MessageContextGlobalSystem()
                    {
                        UserId = message.UserId,
                        Timezone = "GMT3"
                    }
                },
                Skills = skills
            };

            var result = _assistantService.Message(_assistantId, _sessionid, messageObj, MessageContext);

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(result.Response);

            return response;
        }
    }
}