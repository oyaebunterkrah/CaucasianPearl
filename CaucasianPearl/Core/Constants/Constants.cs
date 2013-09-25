using System.Collections.Generic;

namespace CaucasianPearl.Core.Constants
{
    public static class Consts
    {
        public const string Title = "Жемчужина Кавказа";
        
        public static class Connections
        {
            public const string Default = "DefaultConnection";
            public const string CaucasianPearlContext = "CaucasianPearlContext";
        }
        
        public const string AdminLoginName = "admin";
        public const string AdminPassword = "Eekoogh4";

        public static class Culture
        {
            public const string Russian = "Русский";
            public const string Armenian = "Армянский";
            //
            public const string Ru = "ru";
            public const string Hy = "hy";
        }

        public const string MainLayout = "~/Views/Shared/_Layout.cshtml";

        public const string BreadcrumbsSeparator = " / ";
        public const string MenuSeparator = " | ";

        public const string PathDelimiter = "/";
        public const string RootPath = "/";

        public static class PaginatorControl
        {
            // Количество объектов на одной странице по умолчанию.
            public const int DefaultItemsPerPage = 1;
            // Количество отображаемых страниц перед многоточием по умолчанию.
            public const int DefaultNumberOfVisibleLinks = 1;

            // Количество объектов на одной странице для событий.
            public const int EventItemsPerPage = 10;
            // Количество отображаемых страниц перед многоточием для событий.
            public const int EventNumberOfVisibleLinks = 10;

            // Количество объектов на одной странице для новостей.
            public const int SponsorItemsPerPage = 10;
            // Количество отображаемых страниц перед многоточием для новостей.
            public const int SponsorNumberOfVisibleLinks = 10;

            // Количество объектов на одной странице для отзывов и предложений.
            public const int FeedbackItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для отзывов и предложений.
            public const int FeedbackNumberOfVisibleLinks = 5;

            // Количество объектов на одной странице для заявок.
            public const int RequestItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для заявок.
            public const int RequestNumberOfVisibleLinks = 5;

            // Количество объектов на одной странице для настроек сайта.
            public const int SiteSettingsItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для настроек сайта.
            public const int SiteSettingsNumberOfVisibleLinks = 5;

            // Количество объектов на одной странице для профилей пользователей.
            public const int ProfileItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для профилей пользователей.
            public const int ProfileNumberOfVisibleLinks = 5;

            // Количество объектов на одной странице для блоков контента.
            public const int ContentBlockItemsPerPage = 20;
            // Количество отображаемых страниц перед многоточием для блоков контента.
            public const int ContentBlockNumberOfVisibleLinks = 1;

            // Количество объектов на одной странице для фотоальбомов.
            public const int FlickrItemsPerPage = 20;
            // Количество отображаемых страниц перед многоточием для фотоальбомов.
            public const int FlickrNumberOfVisibleLinks = 1;
        }

        public const int OutputCacheDuration = 3600;

        public static class Roles
        {
            public const string Admin = "admin";
            public const string ContentManager = "contentmanager";
            public const string AdminContentManager = "admin,contentmanager";
        }

        public const string ProfileFirstName = "FirstName";
        public const string ProfileMiddleName = "MiddleName";
        public const string ProfileLastName = "LastName";
        public const string ProfilePhone = "Phone";
        public const string ProfileAddress = "Address";

        public static readonly List<string> ReservedWords = new List<string>
            {
                "home",
                "account",
                "index",
                "details",
                "create",
                "edit",
                "delete",
                "up",
                "down",
                "login",
                "logoff",
                "register",
                "changepassword",
                "changepasswordsuccess",
                "event",
                "sponsor"
            };

        // Изображение по умолчанию.
        public const string DefaultImage = "default-image.png";

        // Templates.
        public static class Templates
        {
            public const string Display = "Display";
            public const string Editor = "Editor";
        }

        public const string DateTimeFormat = @"{0:dd.MM.yyyy, 0:hh\:mm}";
        public const string DateFormat = "{0:dd.MM.yyyy}";
        public const string TimeFormat = @"{0:hh\:mm}";

        // Controllers.
        public static class Controllers
        {
            // Home.
            public static class Home
            {
                public const string Name = "home";

                // Actions.
                public static class Actions
                {
                    // О нас.
                    public const string About = "about";
                    // События.
                    public const string Events = "events";
                    // Афиша.
                    public const string Affiche = "affiche";
                }
            }

            // Account.
            public static class Account
            {
                public const string Name = "account";

                // Actions.
                public static class Actions
                {
                    // Регистрация.
                    public const string Register = "register";
                    // Вход.
                    public const string Login = "login";
                    // Выход.
                    public const string LogOff = "logoff";
                    // Управление.
                    public const string Manage = "manage";
                    // Изменить язык.
                    public const string ChangeCulture = "changeculture";
                    // Успешное изменение пароля.
                    public const string ChangePasswordSuccess = "changepasswordsuccess";
                    
                }
            }
            
            // Event.
            public static class Event
            {
                public const string Name = "event";
                public const int EventCount = 4;
                public const int DifferenceCount = 2;
                public const string DefaultImagePath = "~/content/img/cp/event/";

                // Размеры картинок для раздела Товары.
                public const int EventImagesHeight = 320;
                public const int EventImagesWidth = 240;

                // Папки для загрузки картинок для раздела Мероприятия.
                public const string EventImagesFolder = "event";

                // Actions.
                public static class Actions
                {
                    // Event actions.
                    public const string Events = "events";
                    public const string GetHomePageEvents = "gethomepageevents";
                    public const string SelectMediaItemsOnCreate = "selectmediaitemsoncreate";
                    public const string SelectMediaItemsOnEdit = "selectmediaitemsonedit";

                    public const string UploadImage = "uploadimage";
                }

                // Views.
                public static class Views
                {
                    // Event views.
                    public const string SelectMediaItemsOnCreate = "SelectMediaItemsOnCreate";
                    public const string SelectMediaItemsOnEdit = "SelectMediaItemsOnEdit";
                    public const string GetHomePageEvents = "GetHomePageEvents";
                }
            }

            // Sponsor.
            public static class Sponsor
            {
                public const string Name = "sponsor";
                public const string DefaultImagePath = "~/content/img/cp/sponsor/";
                public const int DefaultBigSponsorsCount = 3;
                public const int DefaultSmallSponsorsCount = 12;

                // Размеры картинок для раздела Товары.
                public const int SponsorImagesHeight = 320;
                public const int SponsorImagesWidth = 240;

                // Папки для загрузки картинок для раздела Мероприятия.
                public const string SponsorImagesFolder = "sponsor";

                // Actions.
                public static class Actions
                {
                    // Sponsor actions.
                    public const string Sponsor = "sponsor";
                    public const string UploadImage = "uploadimage";
                }

                // Views.
                public static class Views
                {
                    // Sponsor views.
                }
            }

            // Feedback.
            public static class Feedback
            {
                public const string Name = "feedback";

                // Actions.
                public static class Actions
                {
                    // Feedback actions.
                    public const string Feedbacks = "feedbacks";
                }
            }

            // Request.
            public static class Request
            {
                public const string Name = "request";

                // Actions.
                public static class Actions
                {
                    // Request actions.
                    public const string Requests = "requests";
                    public const string Thanks = "thanks";
                }
            }

            // User.
            public static class Profile
            {
                public const string Name = "profile";

                // Размеры картинок для раздела Профили.
                public const int ProfileImagesHeight = 320;
                public const int ProfileImagesWidth = 240;

                // Количество мини-изображений профилей в одной строке на странице списка.
                public const int ProfileNumberInRow = 3;

                // Папки для загрузки картинок для раздела Мероприятия.
                public const string ProfileImagesFolder = "profile";
                
                // Actions.
                public static class Actions
                {
                    // User actions.
                    public const string Profiles = "profiles";
                    public const string Members = "members";
                    public const string UploadImage = "uploadimage";
                }

                // Views.
                public static class Views
                {
                    // Profile views.
                }
            }

            // Gallery.
            public static class Gallery
            {
                public const string Name = "gallery";

                // Actions.
                public static class Actions
                {
                    // Request actions.
                    public const string Gallery = "gallery";
                    public const string GetLastPhotos = "getlastphotos";
                    public const string GetPhotosets = "getphotosets";
                    public const string Preview = "preview";
                    public const string Photo = "photo";
                }

                // Views.
                public static class Views
                {
                    public const string GetPhotosets = "getphotosets";
                }
            }

            // ContentBlock.
            public static class ContentBlock
            {
                public const string Name = "contentblock";

                // Actions.
                public static class Actions
                {
                    // ContentBlock actions.
                    public const string ContentBlocks = "contentblocks";
                }
            }

            // SiteSettings.
            public static class SiteSettings
            {
                public const string Name = "sitesettings";

                // Actions.
                public static class Actions
                {
                    // SiteSettings actions.
                    public const string SiteSettings = "sitesettings";
                    public const string GetCoverImages = "getcoverimages";
                    public const string UploadCoverImage = "uploadcoverimage";
                }
            }

            // Error.
            public static class Error
            {
                public const string Name = "error";

                // Actions.
                public static class Actions
                {
                    // Error actions.
                    public const string AccessDenied = "accessdenied";
                    public const string NotAuthorized = "notauthorized";
                    public const string NotFound = "notfound";
                    public const string Unexpected = "unexpected";
                }

                // Views.
                public static class Views
                {
                    public const string ViewPathPrefix = "Error/";

                    public const string AccessDenied = ViewPathPrefix + "AccessDenied";
                    public const string DeleteFail = ViewPathPrefix + "DeleteFail";
                    public const string NotAuthorized = ViewPathPrefix + "NotAuthorized";
                    public const string NotFound = ViewPathPrefix + "NotFound";
                    public const string Unexpected = ViewPathPrefix + "Unexpected";
                }
            }
        }

        // Common actions.
        public static class Actions
        {
            public const string Index = "index";
            public const string Create = "create";
            public const string CreatePartial = "createpartial";
            public const string CreatePartialWithCaptcha = "createpartialwithcaptcha";
            public const string CreateExpress = "createexpress";
            public const string CreateOrEdit = "createoredit";
            public const string Edit = "edit";
            public const string EditPartial = "editpartial";
            public const string EditExpress = "editexpress";
            public const string Delete = "delete";
            public const string DeletePartial = "deletepartial";
            public const string DeleteExpress = "deleteexpress";
            public const string Details = "details";
            public const string GetByShortName = "getbyshortname";
            public const string Up = "up";
            public const string Down = "down";
        }

        // Views.
        public static class Views
        {
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string CreateOrEdit = "CreateOrEdit";
            public const string CreateOrEditPartial = "CreateOrEditPartial";
            public const string Delete = "Delete";
            public const string Details = "Details";

            // Shared.
            public static class Shared
            {
                
            }
        }
        
        #region Enums

        public enum DataType
        {
            Text,
            MultilineText,
            ImageUrl,
            Date,
            Time
        }

        #endregion

        #region Translit dictionary

        public static readonly Dictionary<char, string> Translit = new Dictionary<char, string>
            {
                {'а', "a"},
                {'б', "b"},
                {'в', "v"},
                {'г', "g"},
                {'д', "d"},
                {'е', "e"},
                {'ё', "jo"},
                {'ж', "z"},
                {'з', "z"},
                {'и', "i"},
                {'й', "j"},
                {'к', "k"},
                {'л', "l"},
                {'м', "m"},
                {'н', "n"},
                {'о', "o"},
                {'п', "p"},
                {'р', "r"},
                {'с', "s"},
                {'т', "t"},
                {'у', "u"},
                {'ф', "f"},
                {'х', "h"},
                {'ц', "c"},
                {'ч', "c"},
                {'ш', "s"},
                {'щ', "sc"},
                {'ъ', ""},
                {'ы', "y"},
                {'ь', ""},
                {'э', "e"},
                {'ю', "ju"},
                {'я', "ja"},
                {'А', "a"},
                {'Б', "b"},
                {'В', "v"},
                {'Г', "g"},
                {'Д', "d"},
                {'Е', "e"},
                {'Ё', "jo"},
                {'Ж', "z"},
                {'З', "z"},
                {'И', "i"},
                {'Й', "j"},
                {'К', "k"},
                {'Л', "l"},
                {'М', "m"},
                {'Н', "n"},
                {'О', "o"},
                {'П', "p"},
                {'Р', "r"},
                {'С', "s"},
                {'Т', "t"},
                {'У', "u"},
                {'Ф', "f"},
                {'Х', "h"},
                {'Ц', "c"},
                {'Ч', "c"},
                {'Ш', "s"},
                {'Щ', "sc"},
                {'Ъ', ""},
                {'Ы', "y"},
                {'Ь', ""},
                {'Э', "e"},
                {'Ю', "ju"},
                {'Я', "ja"},
                {'a', "a"},
                {'b', "b"},
                {'c', "c"},
                {'d', "d"},
                {'e', "e"},
                {'f', "f"},
                {'g', "g"},
                {'h', "h"},
                {'i', "i"},
                {'j', "j"},
                {'k', "k"},
                {'l', "l"},
                {'m', "m"},
                {'n', "n"},
                {'o', "o"},
                {'p', "p"},
                {'q', "q"},
                {'r', "r"},
                {'s', "s"},
                {'t', "t"},
                {'u', "u"},
                {'v', "v"},
                {'w', "w"},
                {'x', "x"},
                {'y', "y"},
                {'z', "z"},
                {'A', "a"},
                {'B', "b"},
                {'C', "c"},
                {'D', "d"},
                {'E', "e"},
                {'F', "f"},
                {'G', "g"},
                {'H', "h"},
                {'I', "i"},
                {'J', "j"},
                {'K', "k"},
                {'L', "l"},
                {'M', "m"},
                {'N', "n"},
                {'O', "o"},
                {'P', "p"},
                {'Q', "q"},
                {'R', "r"},
                {'S', "s"},
                {'T', "t"},
                {'U', "u"},
                {'V', "v"},
                {'W', "w"},
                {'X', "x"},
                {'Y', "y"},
                {'Z', "z"},
                {'0', "0"},
                {'1', "1"},
                {'2', "2"},
                {'3', "3"},
                {'4', "4"},
                {'5', "5"},
                {'6', "6"},
                {'7', "7"},
                {'8', "8"},
                {'9', "9"},
                {'_', "_"}
            };

        #endregion

        // Session keys
        public static class SessionKeys
        {
            public const string Culture = "Culture";
        }

        #region Regex patterns

        public const string EmailRegex = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
        public const string UserNameRegex = "^[a-zA-Z0-9]+$";

        #endregion

        public static class QueryStringParameters
        {
            public const string Page = "page";
        }

        public static class Paths
        {
            public static class UserControls
            {
                private const string PathPrefix = "UserControls/";

                public const string BigSponsorsControl = PathPrefix + "BigSponsorsControl";
                public const string CultureControl = PathPrefix + "CultureControl";
                public const string FeedbacksControl = PathPrefix + "FeedbacksControl";
                public const string FeedbackFormControl = PathPrefix + "FeedbackFormControl";
                public const string FooterControl = PathPrefix + "FooterControl";
                public const string EventsControl = PathPrefix + "EventsControl";
                public const string LoginControl = PathPrefix + "LoginControl";
                public const string MembersControl = PathPrefix + "MembersControl";
                public const string PaginatorControl = PathPrefix + "PaginatorControl";
                public const string RequestFormControl = PathPrefix + "RequestFormControl";
                public const string SmallSponsorsControl = PathPrefix + "SmallSponsorsControl";
                public const string SocialShareControl = PathPrefix + "SocialShareControl";
                public const string SponsorsControl = PathPrefix + "SponsorsControl";
            }

            public static class Fields
            {
                private const string PathPrefix = "Fields/";

                public const string CaptchaField = PathPrefix + "CaptchaField";
            }

            public static class Plugins
            {
                public const string PluginsPrefix = "~/content/plugins";

                public const string CKEditor = PluginsPrefix + "/ckeditor/ckeditor.js";
                public const string CKEditorConfig = PluginsPrefix + "/ckeditor/config.js";
                public const string Galleria = PluginsPrefix + "/galleria/galleria-1.2.9.min.js";
                public const string GalleriaClassicTheme = PluginsPrefix + "/galleria/themes/classic/galleria.classic.min.js";
            }

            public static class Js
            {
                public const string CpJsPrefixPath = "~/content/js/cp";
                public const string SysJsPrefixPath = "~/content/js/sys";
            }

            public static class Css
            {
                public const string CpCssPrefixPath = "~/content/css/cp";
                public const string SiteCssPrefixPath = "~/content/css/site";
            }

            public static class Img
            {
                public const string CoversFolder = "~/content/img/cp/covers/";
                public const string EntityImgFolder = "~/content/img/cp";
                public const string SiteImgFolder = "~/content/img/site";
            }
        }

        public static class SiteSettings
        {
            // режим главной страницы. Возможны два режима работы: cover (режим обложки) и events (режим событий)
            public const string HomePageMode = "home_page_mode";
            // название изображения главной страницы (обложки)
            public const string CoverImageName = "cover_image_name";
            // флаг отображать/скрывать для футера.
            public const string FooterVisibility = "footer_visibility";
        }
    }
}