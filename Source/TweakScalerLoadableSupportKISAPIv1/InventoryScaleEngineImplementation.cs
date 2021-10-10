/*
	This file is part of TweakScaleKISLoadableSupport12, a component of TweakScaleCompanion_KIS
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
using KISP = KIS; // KIS Plugin
using TweakScalerKISInventory = TweakScaleCompanion.KIS.Inventory.TweakScalerKISInventory;

namespace TweakScaleCompanion.KIS.Contract
{
	public class InventoryScaleEngineImplementation : Inventory.ScaleEngine.Interface
	{
		private readonly Part part;
		bool _changeSlotsQuantity;

		bool Inventory.ScaleEngine.Interface.changeSlotQuantity
		{
			get => this._changeSlotsQuantity;
			set => this._changeSlotsQuantity = value;
		}

		public InventoryScaleEngineImplementation(Part part)
		{
			this.part = part;
			Log.dbg("InventoryScaleEngineImplementation Constructed for {0}", part.partName);
		}

		void Inventory.ScaleEngine.Interface.ScaleTo(ScalingFactor factor)
		{
			this.Scale(factor);
		}

		void Inventory.ScaleEngine.Interface.Restore()
		{
			TweakScale.TweakScale ts_part = this.part.Modules.GetModule<TweakScale.TweakScale>();
			ScalingFactor current = ts_part.ScalingFactor;
			this.Scale(current);
		}

		void Inventory.ScaleEngine.Interface.Destroy()
		{
		}

		private void Scale(ScalingFactor factor)
		{
			KISP.ModuleKISInventory prefab = this.part.partInfo.partPrefab.Modules.GetModule<KISP.ModuleKISInventory>();
			KISP.ModuleKISInventory part = this.part.Modules.GetModule<KISP.ModuleKISInventory>();

			TweakScale.TweakScale ts_prefab = this.part.partInfo.partPrefab.Modules.GetModule<TweakScale.TweakScale>();
			TweakScale.TweakScale ts_part = this.part.Modules.GetModule<TweakScale.TweakScale>();

			part.maxVolume = prefab.maxVolume * factor.absolute.cubic;
			ts_part.DryCost = (float)(ts_prefab.DryCost * factor.absolute.cubic);
			if (this._changeSlotsQuantity)
			{
				part.slotSize = prefab.slotSize;
				part.slotsX = (int)Math.Floor(prefab.slotsX * factor.absolute.linear);
				part.slotsY = (int)Math.Floor(prefab.slotsY * factor.absolute.linear);

				int slotsCount = part.slotsX * part.slotsY;
				if (slotsCount > prefab.slotsX * prefab.slotsY)
				{
					Log.dbg("before {0} {1}", part.maxVolume, ts_part.DryCost);
					part.maxVolume -= (float)(slotsCount * (0.0005 * part.maxVolume));      // Reduce volume by 0.05% per slot
					ts_part.DryCost += (float)(slotsCount * (0.001 * ts_part.DryCost));     // Add 0.1% of cost penalty per slot
					Log.dbg("after {0} {1}", part.maxVolume, ts_part.DryCost);
				}
			}
			else
			{
				part.slotSize = (int)Math.Floor(prefab.slotSize * factor.absolute.linear);
				part.slotsX = prefab.slotsX;
				part.slotsY = prefab.slotsY;
			}

			// FIXME: Resize the Inventory Window size!

			Log.dbg("Current size : {0} maxVolume, {1} slotsX, {2} slotsX, {3} dry cost; {4} currentScale; {5} defaultScale", part.maxVolume, part.slotsX, part.slotsY, ts_part.DryCost, ts_part.currentScale, ts_part.defaultScale);
		}

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<TweakScalerKISInventory>("TweakScaleCompanion_KIS", "TweakScalerKISInventory");
	}
}
