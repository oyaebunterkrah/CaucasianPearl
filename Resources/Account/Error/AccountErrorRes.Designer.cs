﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Account {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AccountErrorRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AccountErrorRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Account.Error.AccountErrorRes", typeof(AccountErrorRes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Произошла неизвестная ошибка. Пожалуйста, проверьте введённые данные и попробуйте снова. Если проблема не устранена, пожалуйста, обратитесь к системному администратору.
        /// </summary>
        public static string MembershipCreateStatus_Default {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пользователь с таким адресом электронной почты уже существует. Пожалуйста, введите другой адрес электронной почты.
        /// </summary>
        public static string MembershipCreateStatus_DuplicateEmail {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_DuplicateEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пользователь с таким именем уже существует. Пожалуйста, введите другое имя.
        /// </summary>
        public static string MembershipCreateStatus_DuplicateUserName {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_DuplicateUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ответ для восстановления пароля является недействительным. Пожалуйста, проверьте значение и попробуйте снова.
        /// </summary>
        public static string MembershipCreateStatus_InvalidAnswer {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidAnswer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Адрес электронной почты является недействительным. Пожалуйста, проверьте значение и попробуйте снова.
        /// </summary>
        public static string MembershipCreateStatus_InvalidEmail {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Введённый пароль недопустим. Пожалуйста, введите допустимый пароль.
        /// </summary>
        public static string MembershipCreateStatus_InvalidPassword {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вопрос для восстановления пароля является недействительным. Пожалуйста, проверьте значение и попробуйте снова.
        /// </summary>
        public static string MembershipCreateStatus_InvalidQuestion {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Имя пользователя является недействительным. Пожалуйста, проверьте значение и попробуйте снова.
        /// </summary>
        public static string MembershipCreateStatus_InvalidUserName {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_InvalidUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Провайдер аутентификации вернул ошибку. Пожалуйста, проверьте ваш ввод и попробуйте снова. Если проблема не устранена, пожалуйста, обратитесь к системному администратору.
        /// </summary>
        public static string MembershipCreateStatus_ProviderError {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_ProviderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Запрос на регистрацию пользователя был отменён. Пожалуйста, проверьте введённые данные и попробуйте снова. Если проблема не устранена, пожалуйста, обратитесь к системному администратору.
        /// </summary>
        public static string MembershipCreateStatus_UserRejected {
            get {
                return ResourceManager.GetString("MembershipCreateStatus_UserRejected", resourceCulture);
            }
        }
    }
}