namespace Supercon.Model
{
    public class Discount
    {
        public string code { get; set; }
        public double value { get; set; }
        public bool isPercentDiscount { get; set; }

        public Discount()
        {
            this.value = 0.0;
            this.isPercentDiscount = false;
            this.code = string.Empty;
        }
        public Discount(double _value, bool _isPercentDiscount, string _code)
        {
            this.value = _value;
            this.isPercentDiscount = _isPercentDiscount;
            this.code = _code;
        }

    }
}
