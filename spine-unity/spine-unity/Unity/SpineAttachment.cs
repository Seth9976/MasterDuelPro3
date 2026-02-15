using System;

namespace Spine.Unity
{
	// Token: 0x02000072 RID: 114
	public class SpineAttachment : SpineAttributeBase
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00012E8C File Offset: 0x0001108C
		public SpineAttachment(bool currentSkinOnly = true, bool returnAttachmentPath = false, bool placeholdersOnly = false, string slotField = "", string dataField = "", string skinField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.currentSkinOnly = currentSkinOnly;
			this.returnAttachmentPath = returnAttachmentPath;
			this.placeholdersOnly = placeholdersOnly;
			this.slotField = slotField;
			this.dataField = dataField;
			this.skinField = skinField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00012EF2 File Offset: 0x000110F2
		public static SpineAttachment.Hierarchy GetHierarchy(string fullPath)
		{
			return new SpineAttachment.Hierarchy(fullPath);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012EFC File Offset: 0x000110FC
		public static Attachment GetAttachment(string attachmentPath, SkeletonData skeletonData)
		{
			SpineAttachment.Hierarchy hierarchy = SpineAttachment.GetHierarchy(attachmentPath);
			if (string.IsNullOrEmpty(hierarchy.name))
			{
				return null;
			}
			SlotData slot = skeletonData.FindSlot(hierarchy.slot);
			if (slot == null)
			{
				return null;
			}
			return skeletonData.FindSkin(hierarchy.skin).GetAttachment(slot.Index, hierarchy.name);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012F4E File Offset: 0x0001114E
		public static Attachment GetAttachment(string attachmentPath, SkeletonDataAsset skeletonDataAsset)
		{
			return SpineAttachment.GetAttachment(attachmentPath, skeletonDataAsset.GetSkeletonData(true));
		}

		// Token: 0x0400021D RID: 541
		public bool returnAttachmentPath;

		// Token: 0x0400021E RID: 542
		public bool currentSkinOnly;

		// Token: 0x0400021F RID: 543
		public bool placeholdersOnly;

		// Token: 0x04000220 RID: 544
		public string skinField = "";

		// Token: 0x04000221 RID: 545
		public string slotField = "";

		// Token: 0x02000073 RID: 115
		public struct Hierarchy
		{
			// Token: 0x06000342 RID: 834 RVA: 0x00012F60 File Offset: 0x00011160
			public Hierarchy(string fullPath)
			{
				string[] chunks = fullPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
				if (chunks.Length == 0)
				{
					this.skin = "";
					this.slot = "";
					this.name = "";
					return;
				}
				if (chunks.Length < 2)
				{
					throw new Exception("Cannot generate Attachment Hierarchy from string! Not enough components! [" + fullPath + "]");
				}
				this.skin = chunks[0];
				this.slot = chunks[1];
				this.name = "";
				for (int i = 2; i < chunks.Length; i++)
				{
					this.name += chunks[i];
				}
			}

			// Token: 0x04000222 RID: 546
			public string skin;

			// Token: 0x04000223 RID: 547
			public string slot;

			// Token: 0x04000224 RID: 548
			public string name;
		}
	}
}
