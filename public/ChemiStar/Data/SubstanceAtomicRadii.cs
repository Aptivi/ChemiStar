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

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChemiStar.Data
{
    /// <summary>
    /// Substance atomic radii
    /// </summary>
    [DebuggerDisplay("E: {string.Join(\", \", Empirical)}, " +
                     "C: {string.Join(\", \", Calculated)}, " +
                     "V: {string.Join(\", \", VanDerWaals)}, " +
                     "S: {string.Join(\", \", CovalentSingle)}, " +
                     "T: {string.Join(\", \", CovalentTriple)}, " +
                     "M: {string.Join(\", \", Metallic)}")]
    public class SubstanceAtomicRadii
    {
        [JsonInclude]
        private double[]? empirical = null;
        [JsonInclude]
        private double[]? calculated = null;
        [JsonInclude]
        private double[]? vanDerWaals = null;
        [JsonInclude]
        private double[]? covalentSingle = null;
        [JsonInclude]
        private double[]? covalentTriple = null;
        [JsonInclude]
        private double[]? metallic = null;

        /// <summary>
        /// Empirically measured atomic radius
        /// </summary>
        [JsonIgnore]
        public double[] Empirical =>
            empirical ?? [];

        /// <summary>
        /// Calculated atomic radius
        /// </summary>
        [JsonIgnore]
        public double[] Calculated =>
            calculated ?? [];

        /// <summary>
        /// van der Walls atomic radius
        /// </summary>
        [JsonIgnore]
        public double[] VanDerWaals =>
            vanDerWaals ?? [];

        /// <summary>
        /// Covalent atomic radius (single bond)
        /// </summary>
        [JsonIgnore]
        public double[] CovalentSingle =>
            covalentSingle ?? [];

        /// <summary>
        /// Covalent atomic radius (triple bond)
        /// </summary>
        [JsonIgnore]
        public double[] CovalentTriple =>
            covalentTriple ?? [];

        /// <summary>
        /// Metallic atomic radius
        /// </summary>
        [JsonIgnore]
        public double[] Metallic =>
            metallic ?? [];
    }
}
