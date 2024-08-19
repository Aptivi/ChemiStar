//
// ChemiStar  Copyright (C) 2024  Aptivi
//
// This file is part of ChemiStar
//
// ChemiStar is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// ChemiStar is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using ChemiStar.Data;
using ChemiStar.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ChemiStar.Tests
{
    [TestClass]
    public class PeriodicTableTests
    {
        [TestMethod]
        public void GetWholeTable()
        {
            // Get the entire list of substances
            var substances = PeriodicTableParser.GetSubstances();
            substances.ShouldNotBeNull();
            substances.ShouldNotBeEmpty();
            substances.Length.ShouldBe(119);

            // Get the hydrogen element and test it
            var hydrogen = substances[0];
            ValidateSubstance(hydrogen, "Hydrogen", "H", 1, 1, 1);
        }

        [DataTestMethod]
        [DataRow("Hydrogen", "Hydrogen", "H", 1, 1, 1)]
        [DataRow("Helium", "Helium", "He", 2, 1, 18)]
        [DataRow("Lithium", "Lithium", "Li", 3, 2, 1)]
        public void TestGetSubstanceFromName(string name, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance name and test it
            var substance = PeriodicTableParser.GetSubstanceFromName(name);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DynamicData(nameof(PeriodicTableQueryHelpers.GetPeriodicTableNamesAndExpectedValues), typeof(PeriodicTableQueryHelpers), DynamicDataSourceType.Method)]
        public void TestGetSubstanceFromNameAuto(string name, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance name and test it
            var substance = PeriodicTableParser.GetSubstanceFromName(name);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DataRow("H", "Hydrogen", "H", 1, 1, 1)]
        [DataRow("He", "Helium", "He", 2, 1, 18)]
        [DataRow("Li", "Lithium", "Li", 3, 2, 1)]
        public void TestGetSubstanceFromSymbol(string symbol, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substance = PeriodicTableParser.GetSubstance(symbol);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DynamicData(nameof(PeriodicTableQueryHelpers.GetPeriodicTableSymbolsAndExpectedValues), typeof(PeriodicTableQueryHelpers), DynamicDataSourceType.Method)]
        public void TestGetSubstanceFromSymbolAuto(string symbol, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substance = PeriodicTableParser.GetSubstance(symbol);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DataRow(1, "Hydrogen", "H", 1, 1, 1)]
        [DataRow(2, "Helium", "He", 2, 1, 18)]
        [DataRow(3, "Lithium", "Li", 3, 2, 1)]
        public void TestGetSubstanceFromAtomicNumber(int atomicNumber, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substance = PeriodicTableParser.GetSubstance(atomicNumber);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DynamicData(nameof(PeriodicTableQueryHelpers.GetPeriodicTableAtomicNumbersAndExpectedValues), typeof(PeriodicTableQueryHelpers), DynamicDataSourceType.Method)]
        public void TestGetSubstanceFromAtomicNumberAuto(int atomicNumber, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substance = PeriodicTableParser.GetSubstance(atomicNumber);
            ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DataRow(1, 1, "Hydrogen", "H", 1, 1, 1)]
        [DataRow(1, 18, "Helium", "He", 2, 1, 18)]
        [DataRow(2, 1, "Lithium", "Li", 3, 2, 1)]
        public void TestGetSubstancesFromPosition(int period, int group, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substance = PeriodicTableParser.GetSubstances(period, group);
            ValidateSubstance(substance[0], expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
        }

        [DataTestMethod]
        [DynamicData(nameof(PeriodicTableQueryHelpers.GetPeriodicTablePositionsAndExpectedValues), typeof(PeriodicTableQueryHelpers), DynamicDataSourceType.Method)]
        public void TestGetSubstancesFromPositionAuto(int period, int group, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            // Get the substance symbol and test it
            var substances = PeriodicTableParser.GetSubstances(period, group);
            foreach (var substance in substances)
            {
                if (substance.Name == expectedName)
                {
                    ValidateSubstance(substance, expectedName, expectedSymbol, expectedNumber, expectedPeriod, expectedGroup);
                    return;
                }
            }
            Assert.Fail("No more substances.");
        }

        private void ValidateSubstance(SubstanceInfo substance, string expectedName, string expectedSymbol, int expectedNumber, int expectedPeriod, int expectedGroup)
        {
            substance.ShouldNotBeNull();
            substance.Name.ShouldNotBeNull();
            substance.Name.ShouldNotBeEmpty();
            substance.Name.ShouldBe(expectedName);
            substance.Symbol.ShouldBe(expectedSymbol);
            substance.AtomicNumber.ShouldBe(expectedNumber);
            substance.Period.ShouldBe(expectedPeriod);
            substance.Group.ShouldBe(expectedGroup);
        }
    }
}
