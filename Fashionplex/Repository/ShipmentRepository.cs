using Fashionplex.Data;
using Fashionplex.IRepository;
using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Repository
{
    /// <summary>
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the shipment.
    /// </summary>
    public class ShipmentRepository : IShipmentRepository
    {
        private ApplicationDbContext _context;

        public ShipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Shipment FindShipmentById(long id)
        {
            var shipment = _context.Shipments.Find(id);
            return shipment;
        }

        public IEnumerable<Shipment> GetAllShipments()
        {
            var shipments = _context.Shipments;
            return shipments;
        }

        public void SaveShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
        }

        public void UpdateShipment(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            _context.SaveChanges();
        }

        public void DeleteShipment(Shipment shipment)
        {
            _context.Shipments.Remove(shipment);
            _context.SaveChanges();
        }
    }
}
