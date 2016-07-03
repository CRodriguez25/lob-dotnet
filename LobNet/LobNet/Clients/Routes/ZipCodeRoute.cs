namespace LobNet.Clients.Routes
{
    public class ZipCodeRoute
    {
        /// <summary>
        /// Required. The Zipcode to filter on.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Optional. Filter on a specific route.
        /// </summary>
        public string Route { get; set; }

        public override string ToString()
        {
            var result = ZipCode;
            if (Route != null) result += "-" + Route;
            return result;
        }
    }
}