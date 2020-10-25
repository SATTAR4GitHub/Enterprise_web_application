using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
    public interface IShipmentRepository
    {
        Shipment FindShipmentById(long id);
        IEnumerable<Shipment> GetAllShipments();
        void SaveShipment(Shipment shipment);
        void UpdateShipment(Shipment shipment);
        void DeleteShipment(Shipment shipment);
    }
}
