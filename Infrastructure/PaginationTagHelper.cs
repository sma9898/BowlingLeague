﻿using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Infrastructure
{
    //For div tags
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        //Constructor
        public PaginationTagHelper(IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }

        public PageNumberingInfo PageInfo { get; set; }
        //public string TeamName { get; set; }

        //Our own dictionary
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        //ViewContext
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        //Process
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            //Fix paging display
            for (int i = 1; i <= PageInfo.NumPages; i++) 
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;

                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
