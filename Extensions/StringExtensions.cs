using System;
namespace PizzaShop.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEqual (this string left, string to)
        {
            return String.IsNullOrEmpty (left) || left.Equals (to);
        }
    }
}
