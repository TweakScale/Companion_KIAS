/*
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
using System;
using System.Linq;
using KSPe.Annotations;
using UnityEngine;

namespace TweakScaleCompanion.KIAS
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class Startup : MonoBehaviour
	{
		[UsedImplicitly]
		private void Start()
		{
			Log.force("Version {0}", Version.Text);

			try
			{
				KSPe.Util.Installation.Check<Startup>();
			}
			catch (KSPe.Util.InstallmentException e)
			{
				Log.error(e.ToShortMessage());
				KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
			}

			this.checkDependencies();
			this.checkConflicts();
		}

		private void checkConflicts()
		{
			if (KSPe.Util.SystemTools.Assembly.Exists.ByName("TweakScaleCompanion-KIS") || System.IO.Directory.Exists(KSPe.IO.Hierarchy.GAMEDATA.Solve("TweakScaleCompanion", "KIS")))
				GUI.ConflictShowStopperAlertBox.Show("Deprecated TweakScale Companion for Kerbal Inventory System (KIS)");
		}

		private void checkDependencies()
		{
			try
			{
				AssemblyLoader.LoadedAssembly assembly = AssemblyLoader.loadedAssemblies.Where(a => a.assembly.GetName().Name == "Scale").First();
				if (-1 == assembly.assembly.GetName().Version.CompareTo(new System.Version(2, 4, 7)))
					GUI.UnmetRequirementsShowStopperAlertBox.Show("TweakScale v2.4.7 or superior");
			}
			catch (Exception e) when (e is NullReferenceException || e is InvalidOperationException)
			{
				GUI.UnmetRequirementsShowStopperAlertBox.Show("TweakScale v2.4.7 or superior");
			}
		}
	}
}
