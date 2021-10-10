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
using TweakScale;

namespace TweakScaleCompanion.KIS.Inventory
{
	public class TweakScalerKISInventory : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActiveEditor = true, guiName = "Inventory Scale Type")]
		[UI_Toggle(disabledText = "Resize Volume Only", enabledText = "Increase Slot Qtd.", scene = UI_Scene.Editor)]
		public bool changeSlotQuantity = false;

		#endregion


		private Contract.Inventory.ScaleEngine.Interface inventoryScaler;


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.force("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), state);
			base.OnStart(state);

			BaseField field = this.Fields["changeSlotQuantity"];
			UI_Control uiControl = (field.uiControlEditor as UI_Toggle);
			uiControl.onFieldChanged += this.OnChangeSlotQuantity;

			if (!Startup.OK_TO_GO)
			{
				Log.warn("KIS Loadable Support not found. TweakScalerKISInventory is unavailable");
				this.enabled = uiControl.controlEnabled = false;
				return;
			}

			this.inventoryScaler = Contract.Inventory.ScaleEngine.CreateFor(this.part);
			this.inventoryScaler.changeSlotQuantity = this.changeSlotQuantity;
			uiControl.controlEnabled = this.enabled = true;
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
			this.inventoryScaler?.Destroy(); this.inventoryScaler = null;
		}

		#endregion

		#region Part Events Handlers

		internal void OnRescale(ScalingFactor factor)
		{
			Log.dbg("OnRescale {0}:{1:X} to {2}", this.name, this.part.GetInstanceID(), factor.ToString());
			if (!this.enabled) return;
			this.inventoryScaler.ScaleTo(factor);
			GameEvents.onEditorShipModified.Fire(EditorLogic.fetch.ship);
		}

		#endregion


		#region Event Handler

		private void OnChangeSlotQuantity(BaseField field, Object obj)
		{
			this.inventoryScaler.changeSlotQuantity = this.changeSlotQuantity;
			this.inventoryScaler.Restore();
			GameEvents.onEditorShipModified.Fire(EditorLogic.fetch.ship);
		}

		#endregion

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<TweakScalerKISInventory>("TweakScaleCompanion_KIS", "TweakScalerKISInventory");
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
