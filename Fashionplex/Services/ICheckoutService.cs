using Fashionplex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    /// <summary>
    /// Checkout interface that forces the Checkout Service to implement the declared method.
    /// </summary>
    public interface ICheckoutService
    {
        void ProcessCheckout(CheckoutViewModel checkoutViewModel);

    }
}
