using AuktionProjekt.Controllers;
using AuktionProjekt.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuktionProjekt.Test
{
    public class BidTest
    {

        [Fact]
        public void TestingIfStatementSimulationOfBidPriceCheck()
        {
            //Exempel på test fast inte riktigt
            //Arrange- sätt upp förutsättingarna för testet
            decimal value1 = 1.2m;
            decimal value2 = 2;
            bool expectedResult = true;
            bool actualResult;

            //Act- kör koden som skall testas
            if (value2 <= value1)
            {
                actualResult = false;
            }
            else
            {
                actualResult = true;
            }
            //Assert- utvärdera resultatet. kontrollera att det är förväntat utfall
            Assert.Equal(expectedResult, actualResult);

        }

    }
}
