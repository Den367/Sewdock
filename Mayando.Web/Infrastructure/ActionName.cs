
namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Defines the available action names.
    /// </summary>
    public enum ActionName
    {
        // General
        Create,
        Delete,
        Details,
        Edit,
        Index,
        Move,
        All,

        // Photos
        Published,
        Taken,
        Tagged,
        Titled,
        Latest,
        Search,
        AddComment,
        BulkEdit,
        BulkEditExecute,
        Hidden,
        Upload,
        // Account
        LogOn,
        LogOff,
        ChangePassword,
        Reset,

        // PhotoProvider
        Synchronize,
        SelectProvider,
        SaveAutoSync,

        // Admin
        EmailTest,
        Urls,
        EventLog,
        ClearEventLog,
        About,

        // Contact
        SendMessage,

        // Feeds
        Photos,
        Comments,

        // Menus
        CreateMenuForPage,

        // Settings
        User,
        EditUserSettings,
        Add,

        // Services
        EnableServiceApi,
        DisableServiceApi,
        RequestNewApiKey,

        // Qrcode
        QrcodeForm,
        GetQrSvg,
        GetQrPng,
        Download,
        ContourSvg
    }
}