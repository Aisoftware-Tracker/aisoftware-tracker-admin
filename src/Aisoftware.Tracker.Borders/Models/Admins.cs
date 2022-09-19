namespace Aisoftware.Tracker.Borders.Models
{
    public class Admins
    {
        private int _id;
        private string _email;
        private string _password;
        private string _phone;
        private DateTime _created;

        public int Id { get => _id; set => _id = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public DateTime Created { get => _created; set => _created = value; }
    }
}
