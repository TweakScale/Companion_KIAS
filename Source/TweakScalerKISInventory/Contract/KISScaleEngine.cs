/*
	This file is part of TweakScalerKISInventory, a component of TweakScaleCompanion_KIS
	© 2020-21 LisiasT : http://lisias.net <support@lisias.net>

	TweakScaleCompanion_KIS is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScaleCompanion_KIS is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScaleCompanion_KIS. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScaleCompanion_KIS. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Reflection;
using TweakScale;

namespace TweakScaleCompanion.KIS.Contract.Inventory
{
	public static class ScaleEngine
	{
		public interface Interface
		{
			bool changeSlotQuantity { get; set; }
			void Restore();
			void ScaleTo(ScalingFactor factor);
			void Destroy();
		}

		internal static Interface CreateFor(Part part)
		{
			Type type = KSPe.Util.SystemTools.TypeFinder.FindByInterface(typeof(Interface));
			ConstructorInfo ctor = type.GetConstructor(new[] { typeof(Part) });
			return (Interface)ctor.Invoke(new object[] { part });
		}
	}
}