using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Models.EDM;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Controllers
{
    public class ProfileController : UrlFriendlyController<Profile, IProfileService<Profile>>
    {
        public ProfileController(IProfileService<Profile> service) :
            base(service: service)
        {
        }

        #region Properties

        // �������� ������������ �����.
        protected override bool IsPageable
        {
            get { return true; }
        }

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            var profiles = _service.GetProfiles(IsPageable);

            foreach (var profile in profiles)
            {
                profile.Description = StringHelper.Substring(profile.Description, 50);
                ModifyValuesOnDetails(profile);
            }

            return View(profiles);
        }

        // �������� ����� �������������� �������.
        [AllowAnonymous]
        public override ActionResult Edit(int id)
        {
            return User.IsInAnyRole(Consts.Roles.AdminContentManager) || WebSecurity.CurrentUserId == id
                       ? View(_service.Get(id))
                       : WebSecurity.IsAuthenticated
                             ? View(Consts.Controllers.Error.Views.AccessDenied)
                             : View(Consts.Controllers.Error.Views.NotAuthorized);
        }

        // ��������� �������� ��������
        [HttpPost]
        public ActionResult UploadImage(int profileId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(Consts.Actions.Edit);

            // �������� ������, ��� �������� ��������� ��������
            var profile = _service.Get(profileId);
            if (profile == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            var postedImage = Request.Files[0];
            if (postedImage != null &&
                (int.Equals(Request.Files.Count, 0) ||
                 (Request.Files.Count > 0 && int.Equals(postedImage.ContentLength, 0))))
            {
                ViewBag.ErrorMessage = ErrorRes.YouDidNotSelectAFile;
                if (HttpContext.Request.UrlReferrer != null)
                    ViewBag.RedirectedUrl = HttpContext.Request.UrlReferrer.AbsolutePath;

                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            try
            {
                if (postedImage != null)
                {
                    // ���������� �������� � ������ ���� �������������� �������� � ���������
                    var extension = Path.GetExtension(postedImage.FileName);
                    var fileName = profileId + extension;
                    var fileSavePath = Path.Combine(
                        Server.MapPath(Url.Content(Consts.Paths.Img.EntityImgFolder)),
                        Consts.Controllers.Profile.ProfileImagesFolder,
                        fileName);

                    // ���� ����� � ������ ���������� ��� �������, ������� ��
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // ��������� ������ �������� � ���������
                    postedImage.ResizeAndSave(Consts.Controllers.Profile.ProfileImagesHeight,
                                              Consts.Controllers.Profile.ProfileImagesWidth, fileSavePath);

                    // ���������� ����� ���������� � ���� ������ � ���� ImageExt
                    profile.ImageExt = extension;
                    _service.Update(profile);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = exception.Message;
                if (exception.InnerException != null)
                    errorMessage =
                        string.Format(
                            "ProfileController.UploadImage() exception. Exception Message: {0}. Inner Exception Message{1}",
                            errorMessage, exception.InnerException.Message);
                ViewBag.ErrorMessage = errorMessage;

                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            return ReturnToEdit(profile);

            /*�������� chunk'��� {
            var fileUpload = Request.Files[0];
            var uploadPath = Server.MapPath("~/App_Data");
            chunk = chunk ?? 0;
            using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            return Json(new { message = "chunk uploaded", name = name }); }*/
        }

        #endregion

        #region Overrides

        protected override void ModifyValuesOnCreate(Profile profile)
        {
            base.ModifyValuesOnCreate(profile);

            // ���������, ��� ������������ � ����� ������� ��� �� ���������� {
            var profileEntityService = ServiceHelper<IProfileService<Profile>>.GetService();
            if (profileEntityService.Get().Any(p => p.UserName == profile.UserName))
                ModelState.AddModelError("error", ErrorRes.UserAlreadyExist); // }

            if (ModelState.IsValid)
                profile.ShortName = _service.CreateUniqueShortName(profile.ID, profile.ShortName);
        }

        protected override ActionResult OnCreated(Profile profile)
        {
            // �������� ����
            var role = HttpContext.Request.Form["Role"];

            // ��������� ������������ � ����
            Roles.AddUserToRole(profile.UserName, role); // }  

            return base.OnCreated(profile);
        }

        protected override void ModifyValuesOnEdit(Profile profile)
        {
            base.ModifyValuesOnCreate(profile);

            var profileEntityService = ServiceHelper<IProfileService<Profile>>.GetService();
            var oldProfile = profileEntityService.GetFirstByCondition(p => p.ID == profile.ID);

            // ���������, ��� ��������� ����� �� ��������� � ������������ � ���� {
            if (profile.UserName != oldProfile.UserName)
                if (profileEntityService.Get().Any(p => p.UserName == profile.UserName))
                    ModelState.AddModelError("error", ErrorRes.UserAlreadyExist); // }

            if (!ModelState.IsValid)
                return;

            // ����� ����
            var newRole = HttpContext.Request.Form["Role"];

            _service.Update(profile);

            // ���� ���� ���� ��������, �� ��������� {
            if (profile.webpages_Roles.RoleName != newRole)
            {
                Roles.RemoveUserFromRole(profile.UserName, profile.webpages_Roles.RoleName);
                Roles.AddUserToRole(profile.UserName, newRole); // }    
            }

            // ���������� ���������� ��� ��� �������� ������
            if (oldProfile.ShortName != profile.ShortName)
                profile.ShortName = _service.CreateUniqueShortName(profile.ID, profile.ShortName);
        }

        protected override void ModifyValuesOnDetails(Profile profile)
        {
            base.ModifyValuesOnDetails(profile);

            profile.ImageUrl = ImageHelper.GetImageUrl(profile, Consts.Controllers.Profile.ProfileImagesFolder);
        }

        #endregion
    }
}