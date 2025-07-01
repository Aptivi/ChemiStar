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
using ChemiStar.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChemiStar
{
    /// <summary>
    /// Periodic table parsing tools
    /// </summary>
	public static class PeriodicTableParser
	{
        private static SubstanceInfo[] cachedSubstances = [];

        /// <summary>
        /// Gets all the substances
        /// </summary>
        /// <returns>Substance info array that contains all arrays</returns>
        public static SubstanceInfo[] GetSubstances() =>
            cachedSubstances;

        /// <summary>
        /// Checks to see if this substance is registered or not
        /// </summary>
        /// <param name="name">Substance name to check (case insensitive)</param>
        /// <param name="substance">Substance instance which will be filled if there is such a substance</param>
        /// <returns>True if registered; false otherwise</returns>
        /// <exception cref="ArgumentException">When a substance name is not provided by the user</exception>
        public static bool IsSubstanceRegisteredName(string name, out SubstanceInfo substance)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Substance name is not provided", nameof(name));
            return IsSubstanceRegisteredDelegated((si) => si.Name.Equals(name, StringComparison.OrdinalIgnoreCase), out substance);
        }

        /// <summary>
        /// Gets a substance from a substance name
        /// </summary>
        /// <param name="name">Substance name to query (case insensitive)</param>
        /// <returns><see cref="SubstanceInfo"/> instance of a substance</returns>
        /// <exception cref="ArgumentException">When a substance name is not provided by the user</exception>
        /// <exception cref="NoSubstanceException">When there is no such substance</exception>
        public static SubstanceInfo GetSubstanceFromName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Substance name is not provided", nameof(name));
            if (!IsSubstanceRegisteredName(name, out SubstanceInfo substance))
                throw new NoSubstanceException("There is no substance by this name:" + $" {name}");
            return substance;
        }

        /// <summary>
        /// Checks to see if this substance is registered or not
        /// </summary>
        /// <param name="symbol">Substance symbol to check (case insensitive)</param>
        /// <param name="substance">Substance instance which will be filled if there is such a substance</param>
        /// <returns>True if registered; false otherwise</returns>
        /// <exception cref="ArgumentException">When a substance name is not provided by the user</exception>
        public static bool IsSubstanceRegistered(string symbol, out SubstanceInfo substance)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException("Substance symbol is not provided", nameof(symbol));
            return IsSubstanceRegisteredDelegated((si) => si.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase), out substance);
        }

        /// <summary>
        /// Gets a substance from a substance symbol
        /// </summary>
        /// <param name="symbol">Substance symbol to query (case insensitive)</param>
        /// <returns><see cref="SubstanceInfo"/> instance of a substance</returns>
        /// <exception cref="ArgumentException">When a substance symbol is not provided by the user</exception>
        /// <exception cref="NoSubstanceException">When there is no such substance</exception>
        public static SubstanceInfo GetSubstance(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException("Substance symbol is not provided", nameof(symbol));
            if (!IsSubstanceRegistered(symbol, out SubstanceInfo substance))
                throw new NoSubstanceException("There is no substance by this symbol:" + $" {symbol}");
            return substance;
        }

        /// <summary>
        /// Checks to see if this substance is registered or not
        /// </summary>
        /// <param name="atomicNumber">Substance atomic number to check</param>
        /// <param name="substance">Substance instance which will be filled if there is such a substance</param>
        /// <returns>True if registered; false otherwise</returns>
        /// <exception cref="ArgumentException">When a substance name is not provided by the user</exception>
        public static bool IsSubstanceRegistered(int atomicNumber, out SubstanceInfo substance)
        {
            if (atomicNumber <= 0 && atomicNumber >= 120)
                throw new ArgumentOutOfRangeException(nameof(atomicNumber), atomicNumber, "Atomic number may not be less than 1 (Hydrogen) or greater than 119 (Ununennium).");
            return IsSubstanceRegisteredDelegated((si) => si.AtomicNumber == atomicNumber, out substance);
        }

        /// <summary>
        /// Gets a substance from a substance atomic number
        /// </summary>
        /// <param name="atomicNumber">Substance atomic number to query</param>
        /// <returns><see cref="SubstanceInfo"/> instance of a substance</returns>
        /// <exception cref="ArgumentOutOfRangeException">When a substance atomic number is less than 1 (Hydrogen) or greater than 119 (Ununennium)</exception>
        /// <exception cref="NoSubstanceException">When there is no such substance</exception>
        public static SubstanceInfo GetSubstance(int atomicNumber)
        {
            if (atomicNumber <= 0 && atomicNumber >= 120)
                throw new ArgumentOutOfRangeException(nameof(atomicNumber), atomicNumber, "Atomic number may not be less than 1 (Hydrogen) or greater than 119 (Ununennium).");
            if (!IsSubstanceRegistered(atomicNumber, out SubstanceInfo substance))
                throw new NoSubstanceException("There is no substance by this atomic number:" + $" {atomicNumber}");
            return substance;
        }

        /// <summary>
        /// Checks to see if there are substances in this period and group
        /// </summary>
        /// <param name="period">Substance period to check (row)</param>
        /// <param name="group">Substance group to check (column)</param>
        /// <param name="substance">Substance instance which will be filled if there is such a substance</param>
        /// <returns>True if registered; false otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">When a period is less than 1 or greater than 8 or when a group is less than 1 or greater than 18</exception>
        public static bool AreSubstancesRegistered(int period, int group, out SubstanceInfo[] substance)
        {
            if (period <= 0 && period >= 9)
                throw new ArgumentOutOfRangeException(nameof(period), period, "Period (row) may not be less than 1 or greater than 8.");
            if (group <= 0 && group >= 19)
                throw new ArgumentOutOfRangeException(nameof(group), group, "Group (column) may not be less than 1 or greater than 18.");
            return AreSubstancesRegisteredDelegated((si) => si.Period == period && si.Group == group, out substance);
        }

        /// <summary>
        /// Gets substances from a period and group
        /// </summary>
        /// <param name="period">Substance period to check (row)</param>
        /// <param name="group">Substance group to check (column)</param>
        /// <returns>True if registered; false otherwise</returns>
        /// <returns><see cref="SubstanceInfo"/> instances of found substances</returns>
        /// <exception cref="ArgumentOutOfRangeException">When a period is less than 1 or greater than 8 or when a group is less than 1 or greater than 18</exception>
        /// <exception cref="NoSubstanceException">When there is no such substance</exception>
        public static SubstanceInfo[] GetSubstances(int period, int group)
        {
            if (period <= 0 && period >= 9)
                throw new ArgumentOutOfRangeException(nameof(period), period, "Period (row) may not be less than 1 or greater than 8.");
            if (group <= 0 && group >= 19)
                throw new ArgumentOutOfRangeException(nameof(group), group, "Group (column) may not be less than 1 or greater than 18.");
            if (!AreSubstancesRegistered(period, group, out SubstanceInfo[] substance))
                throw new NoSubstanceException("There are no substances by this period-group position:" + $" {period}, {group}");
            return substance;
        }

        private static bool IsSubstanceRegisteredDelegated(Func<SubstanceInfo, bool> matcher, out SubstanceInfo substance)
        {
            if (matcher is null)
                throw new ArgumentNullException("Matcher is not provided", nameof(matcher));
            var substances = GetSubstances();
            substance = substances.SingleOrDefault(matcher.Invoke);
            return substance != null;
        }

        private static bool AreSubstancesRegisteredDelegated(Func<SubstanceInfo, bool> matcher, out SubstanceInfo[] substances)
        {
            if (matcher is null)
                throw new ArgumentNullException("Matcher is not provided", nameof(matcher));
            var allSubstances = GetSubstances();
            substances = allSubstances.Where(matcher.Invoke).ToArray();
            return substances != null && substances.Length > 0;
        }

        private static void PopulateSubstances()
        {
            // Get the manifest stream that points to the desired periodic table
            var stream = typeof(PeriodicTableParser).Assembly.GetManifestResourceStream("ChemiStar.PeriodicTableJSON.json");

            // Verify the schema
            var schemaStream = typeof(PeriodicTableParser).Assembly.GetManifestResourceStream("ChemiStar.periodicTableJSON.schema");
            var schemaReader = new StreamReader(schemaStream);
            var schemaJsonReader = new JsonTextReader(schemaReader);
            var schema = JSchema.Load(schemaJsonReader);
            var documentValidatingReader = new StreamReader(stream);
            var documentValidatingJsonReader = new JsonTextReader(documentValidatingReader);
            var documentValidating = JToken.Load(documentValidatingJsonReader);
            documentValidating.Validate(schema);
            stream.Seek(0, SeekOrigin.Begin);

            // Now, deserialize the substance info
            var document = JsonNode.Parse(stream)?["elements"];
            var substances = JsonSerializer.Deserialize<SubstanceInfo[]>(document) ??
                throw new Exception("Can't get a list of chemical substances.");
            cachedSubstances = substances;
        }

        static PeriodicTableParser()
        {
            if (cachedSubstances.Length == 0)
                PopulateSubstances();
        }
	}
}
