using System;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Driver
    {
        private int _id;
        private object _attributes;
        private string _name;
        private string _uniqueId;
        private string _photo;
        private string _document;
        private DateTime? _documentValidAt;

        public int Id { get => _id; set => _id = value; }
        public object Attributes { get => _attributes; set => _attributes = value; }
        public string Name { get => _name; set => _name = value; }
        public string UniqueId { get => _uniqueId; set => _uniqueId = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public string Document { get => _document; set => _document = value; }
        public DateTime? DocumentValidAt { get => _documentValidAt; set => _documentValidAt = value; }
    }
}