using Edura.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edura.WebUI.Infrastructure
{
    [HtmlTargetElement("div" , Attributes="page_model")]
    public class PageLinkTagHelper:TagHelper
    {
        //taghelper kullanarak URL kullanabilmek için ;
        private IUrlHelperFactory UrlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory _UrlHelperFactory)
        {
            UrlHelperFactory = _UrlHelperFactory;
        }

        [ViewContext] // taghelper hangi View üzerindeyse o contexti kullanacak
        [HtmlAttributeNotBound] //HtmlAttribute ile bağlanmayacak
        public ViewContext ViewContext { get; set; } // ViewContexti UrlHelpera göndericez
        public PagingInfo PageModel { get; set; } // pagemodel nesnesini dışarıdan pagemodelattribute ile alıcam
                                                  // sayfaya taşımış olduğumuz bir pageinfo sınıfı pagemodel'a bağlanacak, pagemodel buraya gelecek
        public string PageAction  { get; set; } //sayfanın actionlinki
        public override void Process(TagHelperContext context, TagHelperOutput output) //dışarıya html çıktısı üreticem
        {
            var urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("div");
            for(int i=1; i < PageModel.TotalPages(); i++)
            { // her sayfa için a etiketi oluşturucam
                var tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                tag.InnerHtml.Append(i.ToString()); // a etiketleri arasına gelecek değerleri ayarlıyoruz
                if (i == PageModel.CurrentPage)
                    tag.AddCssClass("btn btn-success");
                else
                    tag.AddCssClass("btn btn-warning");
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
