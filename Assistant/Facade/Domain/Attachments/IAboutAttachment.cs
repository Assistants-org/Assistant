﻿using System;
using Rovecode.Assistant.Facade.Domain.Models;

namespace Rovecode.Assistant.Facade.Domain.Attachments
{
    public interface IAboutAttachment : IWebSourceAttachment
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public string Text { get; set; }

        public IImageInfo Image { get; set; }

        public Uri Link { get; set; }
    }
}
