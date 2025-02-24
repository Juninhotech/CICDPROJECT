namespace CICDPROJECT.Model
{
    using System;

    public static class NumberToWordsConverter
    {
        private static readonly string[] Units =
        { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
      "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        private static readonly string[] Tens =
        { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static string ConvertToWords(long number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + ConvertToWords(Math.Abs(number));

            string words = "";

            if ((number / 1_000_000_000) > 0) 
            {
                words += ConvertToWords(number / 1_000_000_000) + " Billion ";
                number %= 1_000_000_000;
            }

            if ((number / 1_000_000) > 0) 
            {
                words += ConvertToWords(number / 1_000_000) + " Million ";
                number %= 1_000_000;
            }

            if ((number / 1_000) > 0) 
            {
                words += ConvertToWords(number / 1_000) + " Thousand ";
                number %= 1_000;
            }

            if ((number / 100) > 0) 
            {
                words += ConvertToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "And ";

                if (number < 20)
                    words += Units[number];
                else
                {
                    words += Tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + Units[number % 10];
                }
            }

            return words.Trim();
        }
    }

}
