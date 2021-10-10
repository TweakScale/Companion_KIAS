﻿/*
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
using UnityEngine;
using KSPe.Annotations;

namespace TweakScaleCompanion.KIS.Inventory
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class Startup : MonoBehaviour
	{
		internal static bool OK_TO_GO = false;	// If we can't load the Integrator, there's no point on dry running the PartModule...

		[UsedImplicitly]
		private void Awake()
		{
			if (KSPe.Util.SystemTools.TypeFinder.ExistsByQualifiedName("KISAPIv1.KISAPI"))
				using (KSPe.Util.SystemTools.Assembly.Loader a = new KSPe.Util.SystemTools.Assembly.Loader(typeof(TweakScaleCompanion.KIS.Startup).Namespace.Replace(".", KSPe.IO.Path.DirectorySeparatorStr)))
				{
					a.LoadAndStartup("TweakScalerLoadableSupportKISAPIv1");
					OK_TO_GO = true;
				}
		}
	}
}