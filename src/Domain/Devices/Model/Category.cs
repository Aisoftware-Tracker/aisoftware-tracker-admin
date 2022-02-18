using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Domain.Devaces.Model
{
    public static class Category
    {
        public static Dictionary<string, string> Get()
        {
            return new Dictionary<string, string>
            {
                { "Seta", "fa fa-arrow-right" },
                { "Padrão", "fa fa-car" },
                { "Animal", "fa fa-paw" },
                { "Bicicleta", "fa fa-bicycle" },
                { "Barco", "fa fa-sailboat" },
                { "Onibus", "fa fa-bus" },
                { "Carro", "fa fa-car" },
                { "Guidaste", "fa fa-truck-ramp-box" },
                { "Helicoptro", "fa fa-helicopter" },
                { "Motocileta", "fa fa-motorcycle" },
                { "Offroad", "fa fa-truck-monster" },
                { "Pessoa", "fa-solid fa-user" },
                { "Pick-up", "fa fa-truck-pickup" },
                { "Avião", "fa fa-plane" },
                { "Navio", "fa fa-ship" },
                { "Trator", "fa fa-tractor" },
                { "Trem", "fa fa-train" },
                { "Bonde", "fa fa-train-tram" },
                { "Onibus eltrico", "fa fa-bus" },
                { "Caminhão", "fa fa-truck" },
                { "Van", "fa fa-van-shuttle" }
            };
        }
    }
}
