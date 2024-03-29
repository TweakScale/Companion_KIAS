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
			Log.force("OnAwake {0}", this.InstanceId);
			base.OnAwake();
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0} {1}", this.InstanceId, state);
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
			Log.dbg("OnCopy {0} from {1:X}", this.InstanceId, fromModule.part.GetInstanceID());
			base.OnCopy(fromModule);
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0} {1}", this.InstanceId, null != node);
			base.OnLoad(node);
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0} {1}", this.InstanceId, null != node);
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}", this.InstanceId);
			this.inventoryScaler?.Destroy(); this.inventoryScaler = null;
		}

		#endregion

		#region Part Events Handlers

		internal void OnRescale(ScalingFactor factor)
		{
			Log.dbg("OnRescale {0} to {1}", this.InstanceId, factor.ToString());
			if (!this.enabled) return;
			if (null == this.inventoryScaler) return;	// Parts with changing ScaleTypes are triggering OnRescale before OnStart. We don't need to handle that now.

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

		private string _instanceId = null;
		public string InstanceId => _instanceId??(_instanceId = (string.Format("{0}:{1:X}", this.part.name, this.part.GetInstanceID())));
		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<TweakScalerKISInventory>("TweakScaleCompanion_KIAS", "TweakScalerKISInventory");
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
