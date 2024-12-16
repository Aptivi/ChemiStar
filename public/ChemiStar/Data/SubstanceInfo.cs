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
    /// Substance information
    /// </summary>
    [DebuggerDisplay("[{AtomicNumber}] [Period {Period}, Group {Group}] {Name}")]
    public class SubstanceInfo
    {
        [JsonInclude]
        private string name = "";
        [JsonInclude]
        private string appearance = "";
        [JsonInclude]
        private float atomic_mass = 0;
        [JsonInclude]
        private float? boil = 0;
        [JsonInclude]
        private string category = "";
        [JsonInclude]
        private float? density = 0;
        [JsonInclude]
        private string discovered_by = "";
        [JsonInclude]
        private float? melt = 0;
        [JsonInclude]
        private float? molar_heat = 0;
        [JsonInclude]
        private string named_by = "";
        [JsonInclude]
        private int number = 0;
        [JsonInclude]
        private int period = 0;
        [JsonInclude]
        private int group = 0;
        [JsonInclude]
        private string phase = "";
        [JsonInclude]
        private string source = "";
        [JsonInclude]
        private string bohr_model_image = "";
        [JsonInclude]
        private string bohr_model_3d = "";
        [JsonInclude]
        private string spectral_img = "";
        [JsonInclude]
        private string summary = "";
        [JsonInclude]
        private string symbol = "";
        [JsonInclude]
        private int xpos = 0;
        [JsonInclude]
        private int ypos = 0;
        [JsonInclude]
        private int wxpos = 0;
        [JsonInclude]
        private int wypos = 0;
        [JsonInclude]
        private int[] shells = [];
        [JsonInclude]
        private string electron_configuration = "";
        [JsonInclude]
        private string electron_configuration_semantic = "";
        [JsonInclude]
        private float? electron_affinity = 0;
        [JsonInclude]
        private float? electronegativity_pauling = 0;
        [JsonInclude]
        private float?[] ionization_energies = [];
        [JsonPropertyName("cpk-hex")]
        [JsonInclude]
        private string cpkhex = "";
        [JsonInclude]
        private SubstanceImage? image = null;
        [JsonInclude]
        private string block = "";

        /// <summary>
        /// Name of this substance
        /// </summary>
        [JsonIgnore]
        public string Name =>
            name;

        /// <summary>
        /// How does this substance appear or look like?
        /// </summary>
        [JsonIgnore]
        public string Appearance =>
            appearance;

        /// <summary>
        /// Atomic mass of this substance
        /// </summary>
        [JsonIgnore]
        public float AtomicMass =>
            atomic_mass;

        /// <summary>
        /// Temperature in Kelvin at which this substance boils
        /// </summary>
        [JsonIgnore]
        public float? BoilingTemperature =>
            boil;

        /// <summary>
        /// Category of this substance
        /// </summary>
        [JsonIgnore]
        public string Category => 
            category;

        /// <summary>
        /// Density of this substance in <c>g / l</c> for gases and <c>g / cm³</c> for other phases that you can consult using <see cref="phase">this property</see>.
        /// </summary>
        [JsonIgnore]
        public float? Density => 
            density;

        /// <summary>
        /// The name of the person or an organization who discovered this substance
        /// </summary>
        [JsonIgnore]
        public string Discoverer => 
            discovered_by;

        /// <summary>
        /// Temperature in Kelvin at which this substance melts
        /// </summary>
        [JsonIgnore]
        public float? MeltingTemperature => 
            melt;

        /// <summary>
        /// Molar heat in <c>J / (mol * K)</c>
        /// </summary>
        [JsonIgnore]
        public float? MolarHeat => 
            molar_heat;

        /// <summary>
        /// The name of the person or an organization who named this substance
        /// </summary>
        [JsonIgnore]
        public string NamedBy => 
            named_by;

        /// <summary>
        /// Atomic number of a substance
        /// </summary>
        [JsonIgnore]
        public int AtomicNumber => 
            number;

        /// <summary>
        /// Period of this substance (row of a periodic table)
        /// </summary>
        [JsonIgnore]
        public int Period => 
            period;

        /// <summary>
        /// Group of this substance (column of a periodic table)
        /// </summary>
        [JsonIgnore]
        public int Group => 
            group;

        /// <summary>
        /// Substance phase (solid/liquid/gas)
        /// </summary>
        [JsonIgnore]
        public SubstancePhase Phase => 
            phase == "Solid" ? SubstancePhase.Solid :
            phase == "Liquid" ? SubstancePhase.Liquid :
            phase == "Gas" ? SubstancePhase.Gas :
            SubstancePhase.Undetermined;

        /// <summary>
        /// Source URL that indicates where the information was obtained from
        /// </summary>
        [JsonIgnore]
        public string Source => 
            source;

        /// <summary>
        /// URL of an image of the Bohr representation of this substance
        /// </summary>
        [JsonIgnore]
        public string BohrModelImageUrl => 
            bohr_model_image;

        /// <summary>
        /// URL of a 3D model of the Bohr representation of this substance
        /// </summary>
        [JsonIgnore]
        public string BohrModel3DUrl => 
            bohr_model_3d;

        /// <summary>
        /// Spectral band image URL
        /// </summary>
        [JsonIgnore]
        public string SpectralImageUrl => 
            spectral_img;

        /// <summary>
        /// Summary of a substance
        /// </summary>
        [JsonIgnore]
        public string Summary => 
            summary;

        /// <summary>
        /// Symbol of a substance
        /// </summary>
        [JsonIgnore]
        public string Symbol => 
            symbol;

        /// <summary>
        /// X position
        /// </summary>
        [JsonIgnore]
        public int PosX => 
            xpos;

        /// <summary>
        /// Y position
        /// </summary>
        [JsonIgnore]
        public int PosY => 
            ypos;

        /// <summary>
        /// X position
        /// </summary>
        [JsonIgnore]
        public int WPosX => 
            wxpos;

        /// <summary>
        /// Y position
        /// </summary>
        [JsonIgnore]
        public int WPosY => 
            wypos;

        /// <summary>
        /// Substance shells
        /// </summary>
        [JsonIgnore]
        public int[] Shells => 
            shells;

        /// <summary>
        /// Electron configuration (orbitals)
        /// </summary>
        [JsonIgnore]
        public string ElectronConfiguration => 
            electron_configuration;

        /// <summary>
        /// Semantically coded electron configuration
        /// </summary>
        [JsonIgnore]
        public string SemanticElectronConfiguration => 
            electron_configuration_semantic;

        /// <summary>
        /// Electron affinity (energy required to detach an electron from the anion)
        /// </summary>
        [JsonIgnore]
        public float? ElectronAffinity => 
            electron_affinity;

        /// <summary>
        /// Electron negativity
        /// </summary>
        [JsonIgnore]
        public float? ElectronNegativity => 
            electronegativity_pauling;

        /// <summary>
        /// Successive ionization energies
        /// </summary>
        [JsonIgnore]
        public float?[] IonizationEnergies => 
            ionization_energies;

        /// <summary>
        /// Hexadecimal representation of the substance color without the # symbol
        /// </summary>
        [JsonIgnore]
        public string ColorHex => 
            cpkhex;

        /// <summary>
        /// Substance image information
        /// </summary>
        [JsonIgnore]
        public SubstanceImage? Image => 
            image;

        /// <summary>
        /// Chemical block
        /// </summary>
        [JsonIgnore]
        public string Block => 
            block;

    }
}
