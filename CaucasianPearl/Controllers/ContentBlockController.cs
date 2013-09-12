using System.Web;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Models.EDM;
using Ninject;

namespace CaucasianPearl.Controllers
{
    // TODO: [OptionalAuthorizeAttribute(Roles = Consts.Roles.AdminContentManager)]
    public class ContentBlockController : OrderedController<ContentBlock, IOrderedService<ContentBlock>>
    {
        public ContentBlockController(IOrderedService<ContentBlock> service) :
            base(service: service)
        {
        }

        [Inject]
        public new ILogService LogService { get; set; }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        #region Overridden virtual methods

        public override ActionResult Edit(int id)
        {
            var model = _service.Get(id);
            model.Content = HttpUtility.HtmlDecode(model.Content);

            return View(model);
        }

        // Кодируем Html-символы перед сохранением.
        protected override void ModifyValuesOnEdit(ContentBlock contentBlock)
        {
            base.ModifyValuesOnEdit(contentBlock);

            contentBlock.Content = HttpUtility.HtmlEncode(contentBlock.Content);
        }

        // Кодируем Html-символы перед сохранением.
        protected override void ModifyValuesOnDetails(ContentBlock contentBlock)
        {
            base.ModifyValuesOnDetails(contentBlock);

            contentBlock.Content = HttpUtility.HtmlDecode(contentBlock.Content);
        }
        
        #endregion
    }
}