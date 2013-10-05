using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using Calculator.Models;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        public ActionResult BitCalculator(CalculatorModel calcModel)
        {
            CalculatorModel result;
            var types = new List<string>()
            {
                "bit - b",
                "Byte - B",
                "Kilobit - Kb",
                "Kilobyte - KB",
                "Megabit - Mb",
                "Megabyte - MB",
                "Gigabit - Gb",
                "Gigabyte - GB",
                "Terabit - Tb",
                "Terabyte - TB",
                "Petabit - Pb",
                "Petabyte - PB",
                "Exabit - Eb",
                "Exabyte - EB",
                "Zettabit - Zb",
                "Zettabyte - ZB",
                "Yottabit - Yb",
            };
            var kilos = new List<string>()
            {
                "1024",
                "1000"
            };

            if (calcModel == null)
            {
                calcModel = new CalculatorModel();
            }

            calcModel.Types =
                types.Select((x, i) => new SelectListItem { Text = x, Value = i.ToString() }).ToList();
            calcModel.Kilos =
                kilos.Select((x, i) => new SelectListItem { Text = x, Value = i.ToString() }).ToList();

            if (calcModel != null &&
                calcModel.Value != null &&
                calcModel.SelectedType != null &&
                calcModel.SelectedKilo != null)
            {
                calcModel.Result = new Dictionary<string, string>();
                for (int i = 0; i < 10; i++)
                {
                    calcModel.Result.Add(types[i], GetType(
                        i, calcModel.Value, calcModel.SelectedKilo, calcModel.SelectedType));
                }
            }

            return View(calcModel);
        }

        private string GetType(int id, decimal value, string selectedKilo, string selectedType)
        {
            var inputValue = Int32.Parse(selectedType);
            int inputKilo;

            if (selectedKilo == "0")
            {
                inputKilo = 1024;
            }
            else
            {
                inputKilo = 1000;
            }

            decimal currValue = value;

            if (inputValue == 1)
            {
                currValue = value * 8;
            }
            else if (inputValue > 1)
            {
                if (inputValue % 2 == 0)
                {
                    currValue = value * (Pow(inputKilo, inputValue / 2));
                }
                else
                {
                    currValue = value * 8 * (Pow(inputKilo, inputValue / 2));
                }

            }

            decimal typeResult = 0;

            if (id == 0)
            {
                return currValue.ToString();
            }
            else if (id == 1)
            {
                return (currValue / 8).ToString();
            }
            else
            {
                if (id % 2 == 0)
                {
                    typeResult = currValue / (Pow(inputKilo, id / 2));
                }
                else
                {
                    typeResult = (currValue / (Pow(inputKilo, id / 2))) / 8;
                }
            }

            return typeResult.ToString();
        }

        private static decimal Pow(decimal @base, decimal exponent)
        {
            if (exponent == 0M) return 1M;

            decimal result = @base;
            for (decimal iteration = 1M; iteration < exponent; iteration += 1M)
            {
                result *= @base;
            }

            return result;
        }
    }
}