using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Message;
using Wiz.Template.Domain.Interfaces.Bot;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Models.Bot.Watson;
using Wiz.Template.Domain.Models.Services.Blip;

namespace Wiz.Template.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBlipService _blipService;
        private readonly IWatsonBot _watsonBot;
        private readonly IDistributedCache _distributedCache;

        public MessageService(IBlipService blipService,
                              IWatsonBot watsonBot,
                              IDistributedCache distributedCache)
        {
            _blipService = blipService;
            _watsonBot = watsonBot;
            _distributedCache = distributedCache;
        }


        /// <summary>
        /// Recebe uma mensagem da Blip e envia para o Watson
        /// </summary>
        /// <param name="message">Padrão da mensagem que a Blip envia</param>
        /// <returns></returns>
        public async Task<string> PostAsync(MessageViewModel message)
        {
            message.Content = message.Content.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");

            //O Watson espera algumas variaveis de contexto que podemos informar para facilitar o fluxo da conversa.
            Dictionary<string, string> contextVariables = new Dictionary<string, string>
            {
                { "cpf_usuario", null },
                { "nome_usuario", null },
                { "telefone_usuario", null }
            };

            MessageRequestModel messageRequestWatson = new MessageRequestModel()
            {
                Text = message.Content,
                UserId = "UserId_" + message.From.Split('@')[0].TrimStart('5'),
                ContextVariables = contextVariables
            };

            var retorno = _watsonBot.SendMessage(messageRequestWatson);

            //Pausa no fluxo do Watson para buscar uma informação na API, depois volta para o Watson
            if (retorno.Output.Actions != null && retorno.Output.Actions.Any())
            {
                foreach (var item in retorno.Output.Actions)
                {
                    switch (item.Name)
                    {
                        case "actionNameABC":

                            break;
                        case "actionNameXYZ":
                            if (retorno.Output.Generic != null && retorno.Output.Generic.Any())
                            {
                                //Envia uma mensagem para a Blip (WhatsApp)
                                await SendMessageBlip(message, retorno);
                            }

                            contextVariables.Add("novaVariavel", "lorem ipsum");

                            messageRequestWatson.ContextVariables = contextVariables;
                            retorno = _watsonBot.SendMessage(messageRequestWatson);
                            break;
                    }
                }
            }

            await SendMessageBlip(message, retorno);

            return "";
        }


        private async Task SendMessageBlip(MessageViewModel message, ResponseModel retornoWatson)
        {
            MessageModel messageModel = null;

            foreach (GenericModel resp in retornoWatson.Output.Generic)
            {
                switch (resp.Response_Type)
                {
                    case "text":

                        messageModel = new MessageModel
                        {
                            Id = Guid.NewGuid(),
                            To = message.From,
                            Type = "text/plain",
                            Content = resp.Text
                        };

                        break;
                    case "option":
                        string content = resp.Title;

                        content += "\n";

                        foreach (OptionModel opt in resp.Options)
                        {
                            content += opt.Label + "\n";
                        }

                        messageModel = new MessageModel
                        {
                            Id = Guid.NewGuid(),
                            To = message.From,
                            Type = "text/plain",
                            Content = content
                        };

                        break;
                }
                if (messageModel != null)
                {
                    await _blipService.SendMessageAsync(messageModel);
                }
            }
        }
    }
}