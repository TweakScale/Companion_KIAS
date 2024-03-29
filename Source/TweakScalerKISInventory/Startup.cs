﻿/*
	This file is part of TweakScaleCompanion_KIAS
		© 2020-2023 LisiasT : http://lisias.net <me@lisias.net>

	TweakScaleCompanion_KIAS is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScaleCompanion_KIAS is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScaleCompanion_KIAS. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScaleCompanion_KIAS. If not, see <https://www.gnu.org/licenses/>.

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
			if (KSPe.Util.SystemTools.Type.Exists.ByQualifiedName("KISAPIv1.KISAPI"))
				using (KSPe.Util.SystemTools.Assembly.Loader a = new KSPe.Util.SystemTools.Assembly.Loader<KIAS.Startup>())
				{
					a.LoadAndStartup("TweakScalerLoadableSupportKISAPIv1");
					OK_TO_GO = true;
				}
		}
	}
}
