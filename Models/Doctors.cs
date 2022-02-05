using System;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Doctors
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private string _phone;
        private int _gender;
        private string _specialist;
        private DateTime _created;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public int Gender { get => _gender; set => _gender = value; }
        public string Specialist { get => _specialist; set => _specialist = value; }
        public DateTime Created { get => _created; set => _created = value; }
    }
}
