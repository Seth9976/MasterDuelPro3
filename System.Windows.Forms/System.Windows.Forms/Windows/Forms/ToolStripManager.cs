using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	/// <summary>Controls <see cref="T:System.Windows.Forms.ToolStrip" /> rendering and rafting, and the merging of <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, and <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E1 RID: 481
	public sealed class ToolStripManager
	{
		/// <summary>Gets or sets the default painting styles for the form.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00065A83 File Offset: 0x00063C83
		public static ToolStripRenderer Renderer
		{
			get
			{
				return ToolStripManager.renderer;
			}
		}

		/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of different types.</summary>
		/// <returns>true if the merge is successful; otherwise, false.</returns>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001467 RID: 5223 RVA: 0x00065A8C File Offset: 0x00063C8C
		[MonoLimitation("Only supports one level of merging, cannot merge the same ToolStrip multiple times")]
		public static bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetName");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException("Source and target ToolStrip must be different.");
			}
			if (!sourceToolStrip.AllowMerge || !targetToolStrip.AllowMerge)
			{
				return false;
			}
			if (sourceToolStrip.IsCurrentlyMerged || targetToolStrip.IsCurrentlyMerged)
			{
				return false;
			}
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj in sourceToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				switch (toolStripItem.MergeAction)
				{
				default:
					list.Add(toolStripItem);
					break;
				case MergeAction.Insert:
					if (toolStripItem.MergeIndex >= 0)
					{
						list.Add(toolStripItem);
					}
					break;
				case MergeAction.Replace:
				case MergeAction.Remove:
				case MergeAction.MatchOnly:
					foreach (object obj2 in targetToolStrip.Items)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
						if (toolStripItem.Text == toolStripItem2.Text)
						{
							list.Add(toolStripItem);
							break;
						}
					}
					break;
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			sourceToolStrip.BeginMerge();
			targetToolStrip.BeginMerge();
			sourceToolStrip.SuspendLayout();
			targetToolStrip.SuspendLayout();
			while (list.Count > 0)
			{
				ToolStripItem toolStripItem3 = list[0];
				list.Remove(toolStripItem3);
				switch (toolStripItem3.MergeAction)
				{
				default:
					ToolStrip.SetItemParent(toolStripItem3, targetToolStrip);
					continue;
				case MergeAction.Insert:
					ToolStripManager.RemoveItemFromParentToolStrip(toolStripItem3);
					if (toolStripItem3.MergeIndex != -1)
					{
						if (toolStripItem3.MergeIndex >= ToolStripManager.CountRealToolStripItems(targetToolStrip))
						{
							targetToolStrip.Items.AddNoOwnerOrLayout(toolStripItem3);
						}
						else
						{
							targetToolStrip.Items.InsertNoOwnerOrLayout(ToolStripManager.AdjustItemMergeIndex(targetToolStrip, toolStripItem3), toolStripItem3);
						}
						toolStripItem3.Parent = targetToolStrip;
						continue;
					}
					continue;
				case MergeAction.Replace:
				{
					using (IEnumerator enumerator = targetToolStrip.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj3 = enumerator.Current;
							ToolStripItem toolStripItem4 = (ToolStripItem)obj3;
							if (toolStripItem3.Text == toolStripItem4.Text)
							{
								ToolStripManager.RemoveItemFromParentToolStrip(toolStripItem3);
								targetToolStrip.Items.InsertNoOwnerOrLayout(targetToolStrip.Items.IndexOf(toolStripItem4), toolStripItem3);
								targetToolStrip.Items.RemoveNoOwnerOrLayout(toolStripItem4);
								targetToolStrip.HiddenMergedItems.Add(toolStripItem4);
								break;
							}
						}
						continue;
					}
					break;
				}
				case MergeAction.Remove:
					break;
				case MergeAction.MatchOnly:
					goto IL_02E7;
				}
				using (IEnumerator enumerator = targetToolStrip.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj4 = enumerator.Current;
						ToolStripItem toolStripItem5 = (ToolStripItem)obj4;
						if (toolStripItem3.Text == toolStripItem5.Text)
						{
							targetToolStrip.Items.RemoveNoOwnerOrLayout(toolStripItem5);
							targetToolStrip.HiddenMergedItems.Add(toolStripItem5);
							break;
						}
					}
					continue;
				}
				IL_02E7:
				foreach (object obj5 in targetToolStrip.Items)
				{
					ToolStripItem toolStripItem6 = (ToolStripItem)obj5;
					if (toolStripItem3.Text == toolStripItem6.Text)
					{
						if (toolStripItem6 is ToolStripMenuItem && toolStripItem3 is ToolStripMenuItem)
						{
							ToolStripDropDownItem toolStripDropDownItem = (ToolStripMenuItem)toolStripItem3;
							ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)toolStripItem6;
							ToolStripManager.Merge(toolStripDropDownItem.DropDown, toolStripMenuItem.DropDown);
							break;
						}
						break;
					}
				}
			}
			sourceToolStrip.ResumeLayout();
			targetToolStrip.ResumeLayout();
			sourceToolStrip.CurrentlyMergedWith = targetToolStrip;
			targetToolStrip.CurrentlyMergedWith = sourceToolStrip;
			return true;
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to its state before the merge and nullifying all previous merge operations.</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001468 RID: 5224 RVA: 0x00065E6C File Offset: 0x0006406C
		public static bool RevertMerge(ToolStrip targetToolStrip)
		{
			return targetToolStrip != null && ToolStripManager.RevertMerge(targetToolStrip, targetToolStrip.CurrentlyMergedWith);
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning both <see cref="T:System.Windows.Forms.ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <param name="targetToolStrip">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is null.</exception>
		// Token: 0x06001469 RID: 5225 RVA: 0x00065E80 File Offset: 0x00064080
		public static bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			if (sourceToolStrip == null)
			{
				return false;
			}
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj in targetToolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Owner == sourceToolStrip)
				{
					list.Add(toolStripItem);
				}
				else if (toolStripItem is ToolStripMenuItem)
				{
					foreach (object obj2 in (toolStripItem as ToolStripMenuItem).DropDownItems)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
						foreach (object obj3 in sourceToolStrip.Items)
						{
							ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj3;
							if (toolStripItem2.Owner == toolStripMenuItem.DropDown)
							{
								list.Add(toolStripItem2);
							}
						}
					}
				}
			}
			if (list.Count == 0 && targetToolStrip.HiddenMergedItems.Count == 0)
			{
				return false;
			}
			while (targetToolStrip.HiddenMergedItems.Count > 0)
			{
				targetToolStrip.RevertMergeItem(targetToolStrip.HiddenMergedItems[0]);
				targetToolStrip.HiddenMergedItems.RemoveAt(0);
			}
			sourceToolStrip.SuspendLayout();
			targetToolStrip.SuspendLayout();
			while (list.Count > 0)
			{
				sourceToolStrip.RevertMergeItem(list[0]);
				list.Remove(list[0]);
			}
			sourceToolStrip.ResumeLayout();
			targetToolStrip.ResumeLayout();
			sourceToolStrip.IsCurrentlyMerged = false;
			targetToolStrip.IsCurrentlyMerged = false;
			sourceToolStrip.CurrentlyMergedWith = null;
			targetToolStrip.CurrentlyMergedWith = null;
			return true;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00066050 File Offset: 0x00064250
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00066057 File Offset: 0x00064257
		internal static bool ActivatedByKeyboard
		{
			get
			{
				return ToolStripManager.activated_by_keyboard;
			}
			set
			{
				ToolStripManager.activated_by_keyboard = value;
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00066060 File Offset: 0x00064260
		internal static void AddToolStrip(ToolStrip ts)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				ToolStripManager.toolstrips.Add(new WeakReference(ts));
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000660AC File Offset: 0x000642AC
		private static int AdjustItemMergeIndex(ToolStrip ts, ToolStripItem tsi)
		{
			if (ts.Items[0] is MdiControlStrip.SystemMenuItem)
			{
				return tsi.MergeIndex + 1;
			}
			return tsi.MergeIndex;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000660D0 File Offset: 0x000642D0
		private static int CountRealToolStripItems(ToolStrip ts)
		{
			int num = 0;
			foreach (object obj in ts.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (!(toolStripItem is MdiControlStrip.ControlBoxMenuItem) && !(toolStripItem is MdiControlStrip.SystemMenuItem))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0006613C File Offset: 0x0006433C
		internal static ToolStrip GetNextToolStrip(ToolStrip ts, bool forward)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				List<ToolStrip> list2 = new List<ToolStrip>();
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					ToolStrip toolStrip = (ToolStrip)weakReference.Target;
					if (toolStrip != null)
					{
						list2.Add(toolStrip);
					}
				}
				int num = list2.IndexOf(ts);
				if (forward)
				{
					for (int i = num + 1; i < list2.Count; i++)
					{
						if (list2[i].TopLevelControl == ts.TopLevelControl && !(list2[i] is StatusStrip))
						{
							return list2[i];
						}
					}
					for (int j = 0; j < num; j++)
					{
						if (list2[j].TopLevelControl == ts.TopLevelControl && !(list2[j] is StatusStrip))
						{
							return list2[j];
						}
					}
				}
				else
				{
					for (int k = num - 1; k >= 0; k--)
					{
						if (list2[k].TopLevelControl == ts.TopLevelControl && !(list2[k] is StatusStrip))
						{
							return list2[k];
						}
					}
					for (int l = list2.Count - 1; l > num; l--)
					{
						if (list2[l].TopLevelControl == ts.TopLevelControl && !(list2[l] is StatusStrip))
						{
							return list2[l];
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00066314 File Offset: 0x00064514
		internal static bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				using (List<ToolStripMenuItem>.Enumerator enumerator = ToolStripManager.menu_items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ProcessCmdKey(ref m, keyData))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00066394 File Offset: 0x00064594
		internal static bool ProcessMenuKey(ref Message m)
		{
			if (Application.KeyboardCapture != null && Application.KeyboardCapture.OnMenuKey())
			{
				return true;
			}
			Form form = (Form)Control.FromHandle(m.HWnd).TopLevelControl;
			if (form == null)
			{
				return false;
			}
			if (form.MainMenuStrip != null && form.MainMenuStrip.OnMenuKey())
			{
				return true;
			}
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					ToolStrip toolStrip = (ToolStrip)weakReference.Target;
					if (toolStrip != null && toolStrip.TopLevelControl == form && toolStrip.OnMenuKey())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00066478 File Offset: 0x00064678
		internal static void SetActiveToolStrip(ToolStrip toolStrip, bool keyboard)
		{
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.KeyboardActive = false;
			}
			if (toolStrip == null)
			{
				ToolStripManager.activated_by_keyboard = false;
				return;
			}
			ToolStripManager.activated_by_keyboard = keyboard;
			toolStrip.KeyboardActive = true;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000664A4 File Offset: 0x000646A4
		internal static void AddToolStripMenuItem(ToolStripMenuItem tsmi)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				ToolStripManager.menu_items.Add(tsmi);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000664E8 File Offset: 0x000646E8
		internal static void RemoveToolStrip(ToolStrip ts)
		{
			List<WeakReference> list = ToolStripManager.toolstrips;
			lock (list)
			{
				foreach (WeakReference weakReference in ToolStripManager.toolstrips)
				{
					if (weakReference.Target == ts)
					{
						ToolStripManager.toolstrips.Remove(weakReference);
						break;
					}
				}
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00066570 File Offset: 0x00064770
		internal static void RemoveToolStripMenuItem(ToolStripMenuItem tsmi)
		{
			List<ToolStripMenuItem> list = ToolStripManager.menu_items;
			lock (list)
			{
				ToolStripManager.menu_items.Remove(tsmi);
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000665B8 File Offset: 0x000647B8
		internal static void FireAppClicked()
		{
			if (ToolStripManager.AppClicked != null)
			{
				ToolStripManager.AppClicked(null, EventArgs.Empty);
			}
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppClicked);
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000665E3 File Offset: 0x000647E3
		internal static void FireAppFocusChanged(Form form)
		{
			if (ToolStripManager.AppFocusChange != null)
			{
				ToolStripManager.AppFocusChange(form, EventArgs.Empty);
			}
			if (Application.KeyboardCapture != null)
			{
				Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00066610 File Offset: 0x00064810
		private static void RemoveItemFromParentToolStrip(ToolStripItem tsi)
		{
			if (tsi.Owner != null)
			{
				tsi.Owner.Items.RemoveNoOwnerOrLayout(tsi);
				if (tsi.Owner is ToolStripOverflow)
				{
					(tsi.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(tsi);
				}
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001479 RID: 5241 RVA: 0x00066660 File Offset: 0x00064860
		// (remove) Token: 0x0600147A RID: 5242 RVA: 0x00066694 File Offset: 0x00064894
		internal static event EventHandler AppClicked;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600147B RID: 5243 RVA: 0x000666C8 File Offset: 0x000648C8
		// (remove) Token: 0x0600147C RID: 5244 RVA: 0x000666FC File Offset: 0x000648FC
		internal static event EventHandler AppFocusChange;

		// Token: 0x04000C70 RID: 3184
		private static ToolStripRenderer renderer = new ToolStripProfessionalRenderer();

		// Token: 0x04000C71 RID: 3185
		private static ToolStripManagerRenderMode render_mode = ToolStripManagerRenderMode.Professional;

		// Token: 0x04000C72 RID: 3186
		private static bool visual_styles_enabled = Application.RenderWithVisualStyles;

		// Token: 0x04000C73 RID: 3187
		private static List<WeakReference> toolstrips = new List<WeakReference>();

		// Token: 0x04000C74 RID: 3188
		private static List<ToolStripMenuItem> menu_items = new List<ToolStripMenuItem>();

		// Token: 0x04000C75 RID: 3189
		private static bool activated_by_keyboard;
	}
}
