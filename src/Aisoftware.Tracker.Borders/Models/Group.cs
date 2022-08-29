using Newtonsoft.Json;

namespace Aisoftware.Tracker.Borders.Models
{
    public class Group
    {
        private int _id;
        private object _attributes;
        private int _groupId;
        private string _name;
        private string _document;
        private string _address;
        private string _city;
        private string _state;
        private string _country;
        private string _postalCode;
        private string _email;
        private string _phone;
        private string _valorPlano;
        private string _tipoCobranca;
        private string _dataVencimento;
        private string _obsFinanceiro;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }
        
        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }
        
        [JsonProperty("groupId")]
        public int GroupId { get => _groupId; set => _groupId = value; }
        
        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }
        
        [JsonProperty("document")]
        public string Document { get => _document; set => _document = value; }
        
        [JsonProperty("address")]
        public string Address { get => _address; set => _address = value; }
        
        [JsonProperty("city")]
        public string City { get => _city; set => _city = value; }
        
        [JsonProperty("state")]
        public string State { get => _state; set => _state = value; }
        
        [JsonProperty("country")]
        public string Country { get => _country; set => _country = value; }
        
        [JsonProperty("postalcode")]
        public string PostalCode { get => _postalCode; set => _postalCode = value; }
        
        [JsonProperty("email")]
        public string Email { get => _email; set => _email = value; }
        
        [JsonProperty("phone")]
        public string Phone { get => _phone; set => _phone = value; }
        
        [JsonProperty("valorplano")]
        public string ValorPlano { get => _valorPlano; set => _valorPlano = value; }
        
        [JsonProperty("tipocobranca")]
        public string TipoCobranca { get => _tipoCobranca; set => _tipoCobranca = value; }
        
        [JsonProperty("datavencimento")]
        public string DataVencimento { get => _dataVencimento; set => _dataVencimento = value; }
        
        [JsonProperty("obsfinanceiro")]
        public string ObsFinanceiro { get => _obsFinanceiro; set => _obsFinanceiro = value; }
    }
}