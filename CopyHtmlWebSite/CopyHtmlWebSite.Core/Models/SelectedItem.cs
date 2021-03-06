﻿namespace CopyHtmlWebSite.Core.Models
{
    public class SelectedItem : ISelectedItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}