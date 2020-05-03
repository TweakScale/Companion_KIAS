/*
	This file is part of TweakScalerKISInventory, a component of TweakScaleCompanion_KIS
	© 2020 LisiasT : http://lisias.net <support@lisias.net>

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
	along with TweakScaleCompanion_KIS If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using TweakScale;

namespace TweakScaleCompanion_KIS

{
	public class TweakScalerKISInventory : PartModule
	{

		#region KSP UI

		[KSPField(isPersistant = true, guiActiveEditor = true, guiName = "Inventory Scale Type")]
		[UI_Toggle(disabledText = "Resize Volume Only", enabledText = "Increase Slot Qtd.", scene = UI_Scene.Editor)]
		public bool increaseSlotsNumber = false; // TODO update the part when changed

		#endregion


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), state);
			base.OnStart(state);
		}

		public override void OnCopy(PartModule fromModule)
		{
			Log.dbg("OnCopy {0}:{1:X} from {2:X}", this.name, this.part.GetInstanceID(), fromModule.part.GetInstanceID());
			base.OnCopy(fromModule);
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion

		#region Part Events Handlers

		internal void OnRescale(ScalingFactor factor)
		{
			Log.dbg("OnRescale {0}:{1:X} to {2}", this.name, this.part.GetInstanceID(), factor.ToString());

			KIS.ModuleKISInventory prefab = this.part.partInfo.partPrefab.Modules.GetModule<KIS.ModuleKISInventory>();
			KIS.ModuleKISInventory part = this.part.Modules.GetModule<KIS.ModuleKISInventory>();

			part.maxVolume = prefab.maxVolume * factor.absolute.cubic;
			if (this.increaseSlotsNumber)
			{
				//part.slotSize = prefab.slotSize;
				part.slotsX = (int)Math.Floor(prefab.slotsX * factor.absolute.linear);
				part.slotsY = (int)Math.Floor(prefab.slotsY * factor.absolute.linear);

				int slotsCount = part.slotsX * part.slotsY;
				if (slotsCount > prefab.slotsX * prefab.slotsY)
				{
					this.part.partInfo.cost += (float)(slotsCount * (0.01 * this.part.partInfo.cost)); // Add 1% of cost penalty per slot
					part.maxVolume -= (float)(slotsCount * (0.001 * part.maxVolume));                  // Reduce volume by 0.1% per slot
				}
			}
			else
			{
				//part.slotSize = (int)Math.Floor(prefab.slotSize * factor.absolute.linear);
				part.slotsX = prefab.slotsX;
				part.slotsY = prefab.slotsY;
			}

			// FIXME: Resize the Inventory Window size!

			Log.dbg("Current size : {0} maxVolume, {1} slotsX, {2} slotsX, {3} cost", part.maxVolume, part.slotsX, part.slotsY, this.part.partInfo.cost);
		}

		#endregion

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<TweakScalerKISInventory>("TweakScaleCompanion_KIS", "TweakScalerKISInventory");
		static TweakScalerKISInventory()
		{
			Log.level =
#if DEBUG
				KSPe.Util.Log.Level.TRACE
#else
				KSPe.Util.Log.Level.INFO
#endif
				;
		}
	}

	public class Scaler : TweakScale.IRescalable<TweakScalerKISInventory>
	{
		private readonly TweakScalerKISInventory pm;

		public Scaler(TweakScalerKISInventory pm)
		{
			this.pm = pm;
		}

		public void OnRescale(ScalingFactor factor)
		{
			this.pm.OnRescale(factor);
		}
	}
}
