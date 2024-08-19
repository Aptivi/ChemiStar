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

using System.Collections.Generic;

namespace ChemiStar.Tests.Helpers
{
    public static class PeriodicTableQueryHelpers
    {
        public static IEnumerable<object[]> GetPeriodicTableNamesAndExpectedValues()
        {
            var substances = PeriodicTableParser.GetSubstances();
            foreach (var substance in substances)
            {
                yield return new object[]
                {
                    substance.Name,
                    substance.Name, substance.Symbol, substance.AtomicNumber, substance.Period, substance.Group
                };
            }
        }

        public static IEnumerable<object[]> GetPeriodicTableSymbolsAndExpectedValues()
        {
            var substances = PeriodicTableParser.GetSubstances();
            foreach (var substance in substances)
            {
                yield return new object[]
                {
                    substance.Symbol,
                    substance.Name, substance.Symbol, substance.AtomicNumber, substance.Period, substance.Group
                };
            }
        }

        public static IEnumerable<object[]> GetPeriodicTableAtomicNumbersAndExpectedValues()
        {
            var substances = PeriodicTableParser.GetSubstances();
            foreach (var substance in substances)
            {
                yield return new object[]
                {
                    substance.AtomicNumber,
                    substance.Name, substance.Symbol, substance.AtomicNumber, substance.Period, substance.Group
                };
            }
        }

        public static IEnumerable<object[]> GetPeriodicTablePositionsAndExpectedValues()
        {
            var substances = PeriodicTableParser.GetSubstances();
            foreach (var substance in substances)
            {
                yield return new object[]
                {
                    substance.Period, substance.Group,
                    substance.Name, substance.Symbol, substance.AtomicNumber, substance.Period, substance.Group
                };
            }
        }
    }
}
