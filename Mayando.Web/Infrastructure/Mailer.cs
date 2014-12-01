using System;
using System.Net.Mail;
using System.Threading;
using Myembro.Models;

namespace Myembro.Infrastructure
{
    /// <summary>
    /// Sends notification emails.
    /// </summary>
    internal static class Mailer
    {
        #region SendNotificationMail

        /// <summary>
        /// Sends a notification mail to the site owner.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">Defines if the message body is HTML markup.</param>
        /// <param name="async">Defines if the message should be sent asynchronously.</param>
        /// <returns><see langword="true"/> if the message was sent, <see langword="false"/> otherwise (i.e. if no email address was configured).</returns>
        public static bool SendNotificationMail(ApplicationSettings settings, string subject, string body, bool isBodyHtml, bool async)
        {
            return SendNotificationMail(settings, subject, body, isBodyHtml, async, null, null, false);
        }

        /// <summary>
        /// Sends a notification mail to the site owner.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">Defines if the message body is HTML markup.</param>
        /// <param name="async">Defines if the message should be sent asynchronously.</param>
        /// <param name="ignoreExceptions">Defines if exceptions during sending should be ignored (i.e. not logged).</param>
        /// <returns><see langword="true"/> if the message was sent, <see langword="false"/> otherwise (i.e. if no email address was configured).</returns>
        public static bool SendNotificationMail(ApplicationSettings settings, string subject, string body, bool isBodyHtml, bool async, bool ignoreExceptions)
        {
            return SendNotificationMail(settings, subject, body, isBodyHtml, async, null, null, ignoreExceptions);
        }

        /// <summary>
        /// Sends a notification mail to the site owner.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">Defines if the message body is HTML markup.</param>
        /// <param name="async">Defines if the message should be sent asynchronously.</param>
        /// <param name="senderEmail">The email address of the sender.</param>
        /// <param name="senderName">The name of the sender.</param>
        /// <returns><see langword="true"/> if the message was sent, <see langword="false"/> otherwise (i.e. if no email address was configured).</returns>
        public static bool SendNotificationMail(ApplicationSettings settings, string subject, string body, bool isBodyHtml, bool async, string senderEmail, string senderName)
        {
            return SendNotificationMail(settings, subject, body, isBodyHtml, async, senderEmail, senderName, false);
        }

        /// <summary>
        /// Sends a notification mail to the site owner.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">Defines if the message body is HTML markup.</param>
        /// <param name="async">Defines if the message should be sent asynchronously.</param>
        /// <param name="senderEmail">The email address of the sender.</param>
        /// <param name="senderName">The name of the sender.</param>
        /// <param name="ignoreExceptions">Defines if exceptions during sending should be ignored (i.e. not logged).</param>
        /// <returns><see langword="true"/> if the message was sent, <see langword="false"/> otherwise (i.e. if no email address was configured).</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static bool SendNotificationMail(ApplicationSettings settings, string subject, string body, bool isBodyHtml, bool async, string senderEmail, string senderName, bool ignoreExceptions)
        {
            if (string.IsNullOrEmpty(settings.OwnerEmail))
            {
                return false;
            }
            else
            {
                var ownerEmail = new MailAddress(settings.OwnerEmail, settings.OwnerName);
                var fromMail = new MailAddress((string.IsNullOrEmpty(senderEmail) ? settings.OwnerEmail : senderEmail), (string.IsNullOrEmpty(senderName) ? settings.OwnerName : senderName));
                var message = new MailMessage(fromMail, ownerEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml
                };
                var client = new SmtpClient(settings.SmtpServer);
                var sent = false;
                if (async)
                {
                    ThreadPool.QueueUserWorkItem(s =>
                    {
                        try
                        {
                            client.Send(message);
                        }
                        catch (Exception exc)
                        {
                            if (!ignoreExceptions)
                            {
                                Logger.LogException(exc);
                            }
                        }
                    });
                    // Since we are sending asynchronously, we can't really say if the mail has been sent yet.
                    // As far as the caller is concerned, let's say it has been sent successfully.
                    sent = true;
                }
                else
                {
                    try
                    {
                        client.Send(message);
                        sent = true;
                    }
                    catch (Exception exc)
                    {
                        if (!ignoreExceptions)
                        {
                            Logger.LogException(exc);
                            throw;
                        }
                    }
                }
                return sent;
            }
        }

        #endregion
    }
}