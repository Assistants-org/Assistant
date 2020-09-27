﻿using System;
using System.Collections.Generic;
using Rovecode.Assistant.Domain.Attachments;
using Rovecode.Assistant.Facade.Domain.Attachments;
using Rovecode.Assistant.Facade.Ferry.Contexts;

namespace Rovecode.Assistant.Tools.Builders.Yandex
{
    public class SearchLinkAttachmentBuilder : LinkAttachmentBuilder<SearchLinkAttachmentBuilder>
    {
        public SearchLinkAttachmentBuilder(ICommandContext context) : base(context)
        {
            _value = new LinkAttachment()
            {
                Source = Facade.Enums.WebSource.Yandex
            };
        }

        public override SearchLinkAttachmentBuilder SetText(string text)
        {
            _value.Text = text;
            return this;
        }

        public override SearchLinkAttachmentBuilder SetSearchKey(IEnumerable<string> linkKey)
        {
            _value.Link = new Uri($"https://yandex.ru/search/?text={String.Join("+", linkKey)}");
            return this;
        }

        public override ILinkAttachment Build()
        {
            ThrowIfNotValid();

            return _value;
        }
    }
}
