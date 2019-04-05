using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Wiz.Template.Domain.Interfaces.Notifications;

namespace Wiz.Template.Domain.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        private readonly List<NotificationMessage> _notifications;

        public DomainNotification()
        {
            _notifications = new List<NotificationMessage>();
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationMessage(key, message));
        }

        public void AddNotifications(IEnumerable<NotificationMessage> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(new NotificationMessage(error.ErrorCode, error.ErrorMessage));
            }
        }
    }
}
