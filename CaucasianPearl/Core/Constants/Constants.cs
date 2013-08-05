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
            public const int NewsItemsPerPage = 10;
            // Количество отображаемых страниц перед многоточием для новостей.
            public const int NewsNumberOfVisibleLinks = 10;

            // Количество объектов на одной странице для отзывов и предложений.
            public const int FeedbackItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для отзывов и предложений.
            public const int FeedbackNumberOfVisibleLinks = 5;

            // Количество объектов на одной странице для заявок.
            public const int RequestItemsPerPage = 5;
            // Количество отображаемых страниц перед многоточием для заявок.
            public const int RequestNumberOfVisibleLinks = 5;

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
                "onenews"
            };

        #region Пути к папкам

        public static class FoldersPathes
        {
            public const string EntityImagesFolder = "~/content/img/cp";
            public const string CommonImagesFolder = "/content/img/site";
        }
        
        #endregion

        // Изображение по умолчанию.
        public const string NoImage = "no-image.png";

        // Templates.
        public static class Templates
        {
            public const string Display = "Display";
            public const string Editor = "Editor";
        }

        public const string DateTimeFormat = @"{0:dd.MM.yyyy, 0:hh\:mm}";
        public const string DateFormat = "{0:yyyy-MM-dd}";
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
                    // Контакты.
                    public const string Contacts = "contacts";
                    // Отзывы и предложения.
                    public const string Feedback = "feedback";
                    // Написать нам.
                    public const string Contact = "contact";
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
                    public const string GetEvents = "getevents";
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
                }
            }

            // OneNews.
            public static class OneNews
            {
                public const string Name = "onenews";

                // Размеры картинок для раздела Товары.
                public const int OneNewsImagesHeight = 320;
                public const int OneNewsImagesWidth = 240;

                // Папки для загрузки картинок для раздела Мероприятия.
                public const string OneNewsImagesFolder = "news";

                // Actions.
                public static class Actions
                {
                    // OneNews actions.
                    public const string News = "news";
                    public const string UploadImage = "uploadimage";
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
                    public const string UploadImage = "uploadimage";
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
            public const string CreateOrEdit = "createoredit";
            public const string Edit = "edit";
            public const string Delete = "delete";
            public const string Details = "details";
            public const string GetByShortName = "GetByShortName";
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
            public const string Delete = "Delete";
            public const string Details = "Details";

            // Shared.
            public static class Shared
            {
                
            }
        }
        
        public enum DataType
        {
            Text,
            MultilineText,
            ImageUrl,
            Date,
            Time
        }

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

        public static class UserControlsPaths
        {
            private const string PathPrefix = "UserControls/";

            public const string Culture = PathPrefix + "CultureControl";
            public const string Login = PathPrefix + "LoginControl";
            public const string Paginator = PathPrefix + "PaginatorControl";
        }

        public static class QueryStringParameters
        {
            public const string Page = "page";
        }

        public static class Paths
        {
            public const string PluginsPrefix = "~/content/plugins";
            //private const string CpPrefixPath = "~/content/js/cp/";
            //private const string SysPrefixPath = "~/content/js/sys/";

            public const string CKEditor = PluginsPrefix + "/ckeditor/ckeditor.js";
            public const string CKEditorConfig = PluginsPrefix + "/ckeditor/config.js";
            public const string Galleria = PluginsPrefix + "/galleria/galleria-1.2.9.min.js";
            public const string GalleriaClassicTheme = PluginsPrefix + "/galleria/themes/classic/galleria.classic.min.js";
        }
    }
}