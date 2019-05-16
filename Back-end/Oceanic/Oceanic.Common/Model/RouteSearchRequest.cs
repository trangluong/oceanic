namespace Oceanic.Common.Model
{
    public class RouteSearchRequest
    {
        public int fromCityId { get; set; }
        public int toCityId { get; set; }
        public int goodsTypeId { get; set; }
        public decimal weight { get; set; }
        public int height { get; set; }
        public int depth { get; set; }
        public int breadth { get; set; }
    }
}
