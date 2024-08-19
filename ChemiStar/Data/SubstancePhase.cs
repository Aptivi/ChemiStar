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

namespace ChemiStar.Data
{
    /// <summary>
    /// Substance phase
    /// </summary>
    public enum SubstancePhase
    {
        /// <summary>
        /// This substance is a solid
        /// </summary>
        Solid,
        /// <summary>
        /// This substance is a liquid
        /// </summary>
        Liquid,
        /// <summary>
        /// This substance is a gas
        /// </summary>
        Gas,
        /// <summary>
        /// This substance's phase is undetermined. It could possibly either a gas, a liquid, or a solid object.
        /// </summary>
        Undetermined,
    }
}
