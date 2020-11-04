using Fashionplex.Enums;
using Fashionplex.IRepository;
using Fashionplex.Models;
using Fashionplex.ViewModels;
using Fashionplex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    public class CheckoutService : ICheckoutService
    {
        private ICustomerRepository _customerRepository;
        private IShipmentRepository _shipmentRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailsRepository _orderItemRepository;
        private ICartRepository _cartRepository;
        private ICartDetailsRepository _cartItemRepository;
        private ICartService _cartService;
        public CheckoutService(ICustomerRepository customerRepository,
            IShipmentRepository addressRepository,
            IOrderRepository orderRepository,
            IOrderDetailsRepository orderItemRepository,
            ICartRepository cartRepository,
            ICartDetailsRepository cartItemRepository,
            ICartService cartService)
        {
            _customerRepository = customerRepository;
            _shipmentRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
        }

        public void ProcessCheckout(CheckoutViewModel checkoutViewModel)
        {
            var customer = new Customer
            {
                FirstName = checkoutViewModel.FirstName,
                LastName = checkoutViewModel.LastName,
                Email = checkoutViewModel.EmailAddress,
                IsDeleted = false
            };

            _customerRepository.SaveCustomer(customer);

            var shipment = new Shipment
            { 
                Name = checkoutViewModel.FirstName + " " + checkoutViewModel.LastName,
                AddressLine1 = checkoutViewModel.AddressLine1,
                AddressLine2 = checkoutViewModel.AddressLine2,
                City = checkoutViewModel.City,
                Province = checkoutViewModel.Province,
                Country = checkoutViewModel.Country,
                PostalCode = checkoutViewModel.PostalCode,
                IsDeleted = false
            };

            _shipmentRepository.SaveShipment(shipment);

            //var customer = new Customer
            //{
            //    PersonId = person.Id,
            //    Person = person,
            //    IsDeleted = false
            //};

            //_customerRepository.SaveCustomer(customer);

            var cart = _cartService.GetCart();

            if (cart != null)
            {
                IEnumerable<CartDetail> cartItems = _cartItemRepository.FindCartItemsByCartId(cart.Id).ToList();
                var cartTotal = _cartService.GetCartTotal();
                decimal shippingCharge = 0;
                var orderTotal = cartTotal*1.13m + shippingCharge;

                var order = new Order
                {
                    OrderTotal = orderTotal,
                    OrderItemTotal = cartTotal,
                    ShippingCost = shippingCharge,
                    ShipmentId = shipment.Id,
                    ShipmentInfo = shipment,
                    CustomerId = customer.Id,
                    Customer = customer,
                    OrderStatus = OrderStatus.Submitted
                };

                _orderRepository.SaveOrder(order);

                    foreach (var cartItem in cartItems)
                    {
                        var orderItem = new OrderDetails
                        {
                            Order = order,
                            OrderId = order.Id,
                            Product = cartItem.Product,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity
                        };

                    _orderItemRepository.SaveOrderItem(orderItem);
                    }
                
                _cartRepository.DeleteCart(cart);
            }
        }
    }
}
