using System;
using System.Collections.Generic;

namespace Waterverbruik
{
    internal class WaterUseCost
    {
        private const string payOptionOneName = "tarief 1";
        private const double payOptionOnePriceStart = 100;
        private const double payOptionOnePricePerLiter = 0.25;
        private const string payOptionOneString = "the total pirce is ";
        private const string payOptionTwoName = "tarief 2";
        private const double payOptionTwoPriceStart = 75;
        private const double payOptionTwoPricePerLiter = 0.38;
        private const string payOptionTwoString = "the total pirce is ";
        private const string payOptionThreeName = "tarief 0";
        private const string payOptionThreeStringPartOne = "the cheapest pay option is ";
        private const string payOptionThreeStringPartTwo = " with a total price of ";

        private const string ExceptionPayOptionNotFound = "the given pay option name was not found among the pay options";

        private static List<PayOption> payOptions;

        internal static string[] GetPayOptions()
        {
            SetupPayOptions();
            string[] toReturn = new string[payOptions.Count];
            for(int i=0; i < toReturn.Length; i++)
            {
                toReturn[i] = payOptions[i].GetName();
            }
            return toReturn;
        }
        internal static string GetPriceString(int amountOfWaterInLiters, string payOption)
        {
            SetupPayOptions();
            foreach(PayOption option in payOptions)
            {
                if (option.IsSameName(payOption))
                {
                    return option.GetStringPrice(amountOfWaterInLiters);
                }
            }
            throw new Exception(ExceptionPayOptionNotFound);
        }
        private static void SetupPayOptions()
        {
            if (payOptions == null)
            {
                payOptions = new List<PayOption>();

                Func<double, double> payOptionOneCalculatePriceLambda = (double amountLiter) =>
                {
                    return payOptionOnePriceStart + amountLiter * payOptionOnePricePerLiter;
                };
                Func<double, string> payOptionOneMakeStringWithPriceLambda = (double amountLiter) =>
                {
                    return payOptionOneString + payOptionOneCalculatePriceLambda.Invoke(amountLiter);
                };
                payOptions.Add(new PayOption(payOptionOneName, payOptionOneCalculatePriceLambda, payOptionOneMakeStringWithPriceLambda));

                Func<double, double> payOptionTwoCalculatePriceLambda = (double amountLiter) =>
                {
                    return payOptionTwoPriceStart + amountLiter * payOptionTwoPricePerLiter;
                };
                Func<double, string> payOptionTwoMakeStringWithPriceLambda = (double amountLiter) =>
                {
                    return payOptionTwoString + payOptionTwoCalculatePriceLambda.Invoke(amountLiter);
                };
                payOptions.Add(new PayOption(payOptionTwoName, payOptionTwoCalculatePriceLambda, payOptionTwoMakeStringWithPriceLambda));

                Func<double, double> payOptionThreeCalculatePriceLambda = (double amountLiter) =>
                {
                    return Math.Min(payOptionOneCalculatePriceLambda.Invoke(amountLiter), payOptionTwoCalculatePriceLambda.Invoke(amountLiter));
                };
                Func<double, string> payOptionThreeMakeStringWithPriceLambda = (double amountLiter) =>
                {
                    double optionOne = payOptionOneCalculatePriceLambda.Invoke(amountLiter);
                    double optionTwo = payOptionTwoCalculatePriceLambda.Invoke(amountLiter);
                    string name;
                    if(optionOne< optionTwo)
                    {
                        name = payOptionOneName;
                    }
                    else
                    {
                        name = payOptionTwoName;
                    }
                    return payOptionThreeStringPartOne + name + payOptionThreeStringPartTwo + Math.Min(optionOne, optionTwo);
                };
                payOptions.Add(new PayOption(payOptionThreeName, payOptionThreeCalculatePriceLambda, payOptionThreeMakeStringWithPriceLambda));
            }
        }

        private class PayOption
        {
            private string name;
            private Func<double, double> calculatePriceLambda;
            private Func<double, string> makeStringWithPriceLambda;

            internal PayOption(string name, Func<double, double> calculatePriceLambda, Func<double, string> makeStringWithPriceLambda)
            {
                this.name = name;
                this.calculatePriceLambda = calculatePriceLambda;
                this.makeStringWithPriceLambda = makeStringWithPriceLambda;
            }
            internal string GetName()
            {
                return name;
            }
            internal bool IsSameName(string name)
            {
                return this.name.Equals(name);
            }
            internal double GetPrice(int amountOfLiters)
            {
                return calculatePriceLambda.Invoke(amountOfLiters);
            }
            internal string GetStringPrice (int amountOfLiters)
            {
                return makeStringWithPriceLambda(amountOfLiters);
            }
        }
    }
}