using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Domain.Devices.Models
{
    public static class Category
    {
        public static IDictionary<string, string> Get()
        {
            return new Dictionary<string, string>
            {
                { "seta", "Seta" },
                { "padrão", "Padrão" },
                { "animal", "Animal" },
                { "bicicleta", "Bicicleta" },
                { "barco", "Barco" },
                { "onibus", "Onibus" },
                { "carro", "Carro" },
                { "guidaste","Guidaste" },
                { "helicoptro", "Helicoptro" },
                { "motocileta", "Motocileta" },
                { "offroad", "Offroad" },
                { "pessoa", "Pessoa" },
                { "pick-up", "Pick-up" },
                { "avião", "Avião" },
                { "navio", "Navio" },
                { "trator", "Trator" },
                { "trem", "Trem" },
                { "bonde", "Bonde" },
                { "onibus eltrico", "Onibus eltrico" },
                { "caminhão", "Caminhão" },
                { "van", "Van" }
            };
        }

        public static IDictionary<string, string> GetIcons()
        {
            return new Dictionary<string, string>
            {
                { "seta", "fas fa-location-arrow" },
                { "padrão", "fas fa-car" },
                { "animal", "fas fa-paw" },
                { "bicicleta", "fas fa-bicycle" },
                { "barco", "fas fa-sailboat" },
                { "onibus", "fas fa-bus" },
                { "carro", "fas fa-car" },
                { "guidaste", "fas fa-truck-ramp-box" },
                { "helicoptro", "fas fa-helicopter" },
                { "motocileta", "fas fa-motorcycle" },
                { "offroad", "fas fa-truck-monster" },
                { "pessoa", "fa-solid fa-user" },
                { "pick-up", "fas fa-truck-pickup" },
                { "avião", "fas fa-plane" },
                { "navio", "fas fa-ship" },
                { "trator", "fas fa-tractor" },
                { "trem", "fas fa-train" },
                { "bonde", "fas fa-train-tram" },
                { "onibus eltrico", "fas fa-bus-alt" },
                { "caminhão", "fas fa-truck" },
                { "van", "fas fa-shuttle-van" },
                { "null", "fas fa-question" }
            };
        }
    }
}
