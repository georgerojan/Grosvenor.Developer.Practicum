using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Grosvenor.Developer.Practicum.Common;
using Grosvenor.Developer.Practicum.Interfaces;

namespace Grosvenor.Developer.Practicum
{
    class BusinessUnit
    {
        [Import(typeof(IRestaurant))]
        public IRestaurant RestaurantInstance { get; set; }

        [Import(typeof(IDevice))]
        public IDevice DeviceInstance { get; set; }

        private CompositionContainer container;

        static void Main(string[] args)
        {
            BusinessUnit currentInstance = new BusinessUnit();

            currentInstance.DeviceInstance.Write("Your order please... ");
            var orderNumber = currentInstance.RestaurantInstance.PlaceOrder(currentInstance.DeviceInstance.Read());
            if (orderNumber == 0)
                currentInstance.DeviceInstance.Write(Constants.ErrorText);
            else
                currentInstance.DeviceInstance.Write(currentInstance.RestaurantInstance.ProcessOrder());

            currentInstance.DeviceInstance.Read();
        }

        private BusinessUnit()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IRestaurant).Assembly));
            this.container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this.container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
