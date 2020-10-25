using Fashionplex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    public interface ICheckoutService
    {
        void ProcessCheckout(CheckoutViewModel checkoutViewModel);

    }
}
