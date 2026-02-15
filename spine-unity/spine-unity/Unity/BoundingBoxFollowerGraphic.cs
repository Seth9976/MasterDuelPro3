using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000022 RID: 34
	[ExecuteAlways]
	[HelpURL("http://esotericsoftware.com/spine-unity#BoundingBoxFollowerGraphic")]
	public class BoundingBoxFollowerGraphic : MonoBehaviour
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000055B5 File Offset: 0x000037B5
		public Slot Slot
		{
			get
			{
				return this.slot;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000055BD File Offset: 0x000037BD
		public BoundingBoxAttachment CurrentAttachment
		{
			get
			{
				return this.currentAttachment;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000055C5 File Offset: 0x000037C5
		public string CurrentAttachmentName
		{
			get
			{
				return this.currentAttachmentName;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000055CD File Offset: 0x000037CD
		public PolygonCollider2D CurrentCollider
		{
			get
			{
				return this.currentCollider;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000055D5 File Offset: 0x000037D5
		public bool IsTrigger
		{
			get
			{
				return this.isTrigger;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000055DD File Offset: 0x000037DD
		private void Start()
		{
			this.Initialize(false);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000055E8 File Offset: 0x000037E8
		private void OnEnable()
		{
			if (this.skeletonGraphic != null)
			{
				this.skeletonGraphic.OnRebuild -= this.HandleRebuild;
				this.skeletonGraphic.OnRebuild += this.HandleRebuild;
			}
			this.Initialize(false);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000055DD File Offset: 0x000037DD
		private void HandleRebuild(SkeletonGraphic sr)
		{
			this.Initialize(false);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005638 File Offset: 0x00003838
		public void Initialize(bool overwrite = false)
		{
			if (this.skeletonGraphic == null)
			{
				return;
			}
			this.skeletonGraphic.Initialize(false);
			if (string.IsNullOrEmpty(this.slotName))
			{
				return;
			}
			if (!overwrite && this.colliderTable.Count > 0 && this.slot != null && this.skeletonGraphic.Skeleton == this.slot.Skeleton && this.slotName == this.slot.Data.Name)
			{
				return;
			}
			this.slot = null;
			this.currentAttachment = null;
			this.currentAttachmentName = null;
			this.currentCollider = null;
			this.colliderTable.Clear();
			this.nameTable.Clear();
			Skeleton skeleton = this.skeletonGraphic.Skeleton;
			if (skeleton == null)
			{
				return;
			}
			this.slot = skeleton.FindSlot(this.slotName);
			if (this.slot == null)
			{
				if (BoundingBoxFollowerGraphic.DebugMessages)
				{
					Debug.LogWarning(string.Format("Slot '{0}' not found for BoundingBoxFollowerGraphic on '{1}'. (Previous colliders were disposed.)", this.slotName, base.gameObject.name));
				}
				return;
			}
			int slotIndex = this.slot.Data.Index;
			int requiredCollidersCount = 0;
			PolygonCollider2D[] colliders = base.GetComponents<PolygonCollider2D>();
			if (base.gameObject.activeInHierarchy)
			{
				float scale = this.skeletonGraphic.MeshScale;
				foreach (Skin skin in skeleton.Data.Skins)
				{
					this.AddCollidersForSkin(skin, slotIndex, colliders, scale, ref requiredCollidersCount);
				}
				if (skeleton.Skin != null)
				{
					this.AddCollidersForSkin(skeleton.Skin, slotIndex, colliders, scale, ref requiredCollidersCount);
				}
			}
			this.DisposeExcessCollidersAfter(requiredCollidersCount);
			if (BoundingBoxFollowerGraphic.DebugMessages && this.colliderTable.Count == 0)
			{
				if (base.gameObject.activeInHierarchy)
				{
					Debug.LogWarning("Bounding Box Follower not valid! Slot [" + this.slotName + "] does not contain any Bounding Box Attachments!");
					return;
				}
				Debug.LogWarning("Bounding Box Follower tried to rebuild as a prefab.");
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005834 File Offset: 0x00003A34
		private void AddCollidersForSkin(Skin skin, int slotIndex, PolygonCollider2D[] previousColliders, float scale, ref int collidersCount)
		{
			if (skin == null)
			{
				return;
			}
			List<Skin.SkinEntry> skinEntries = new List<Skin.SkinEntry>();
			skin.GetAttachments(slotIndex, skinEntries);
			foreach (Skin.SkinEntry entry in skinEntries)
			{
				Attachment attachment = skin.GetAttachment(slotIndex, entry.Name);
				BoundingBoxAttachment boundingBoxAttachment = attachment as BoundingBoxAttachment;
				if (BoundingBoxFollowerGraphic.DebugMessages && attachment != null && boundingBoxAttachment == null)
				{
					Debug.Log("BoundingBoxFollowerGraphic tried to follow a slot that contains non-boundingbox attachments: " + this.slotName);
				}
				if (boundingBoxAttachment != null && !this.colliderTable.ContainsKey(boundingBoxAttachment))
				{
					PolygonCollider2D bbCollider = ((collidersCount < previousColliders.Length) ? previousColliders[collidersCount] : base.gameObject.AddComponent<PolygonCollider2D>());
					collidersCount++;
					SkeletonUtility.SetColliderPointsLocal(bbCollider, this.slot, boundingBoxAttachment, scale);
					bbCollider.isTrigger = this.isTrigger;
					bbCollider.usedByEffector = this.usedByEffector;
					bbCollider.usedByComposite = this.usedByComposite;
					bbCollider.enabled = false;
					bbCollider.hideFlags = HideFlags.NotEditable;
					this.colliderTable.Add(boundingBoxAttachment, bbCollider);
					this.nameTable.Add(boundingBoxAttachment, entry.Name);
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005978 File Offset: 0x00003B78
		private void OnDisable()
		{
			if (this.clearStateOnDisable)
			{
				this.ClearState();
			}
			if (this.skeletonGraphic != null)
			{
				this.skeletonGraphic.OnRebuild -= this.HandleRebuild;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void ClearState()
		{
			if (this.colliderTable != null)
			{
				foreach (PolygonCollider2D polygonCollider2D in this.colliderTable.Values)
				{
					polygonCollider2D.enabled = false;
				}
			}
			this.currentAttachment = null;
			this.currentAttachmentName = null;
			this.currentCollider = null;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005A24 File Offset: 0x00003C24
		private void DisposeExcessCollidersAfter(int requiredCount)
		{
			PolygonCollider2D[] colliders = base.GetComponents<PolygonCollider2D>();
			if (colliders.Length == 0)
			{
				return;
			}
			for (int i = requiredCount; i < colliders.Length; i++)
			{
				PolygonCollider2D collider = colliders[i];
				if (collider != null)
				{
					global::UnityEngine.Object.Destroy(collider);
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005A5E File Offset: 0x00003C5E
		private void LateUpdate()
		{
			if (this.slot != null && this.slot.Attachment != this.currentAttachment)
			{
				this.MatchAttachment(this.slot.Attachment);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005A8C File Offset: 0x00003C8C
		private void MatchAttachment(Attachment attachment)
		{
			BoundingBoxAttachment bbAttachment = attachment as BoundingBoxAttachment;
			if (BoundingBoxFollowerGraphic.DebugMessages && attachment != null && bbAttachment == null)
			{
				Debug.LogWarning("BoundingBoxFollowerGraphic tried to match a non-boundingbox attachment. It will treat it as null.");
			}
			if (this.currentCollider != null)
			{
				this.currentCollider.enabled = false;
			}
			if (bbAttachment == null)
			{
				this.currentCollider = null;
				this.currentAttachment = null;
				this.currentAttachmentName = null;
				return;
			}
			PolygonCollider2D foundCollider;
			this.colliderTable.TryGetValue(bbAttachment, out foundCollider);
			if (foundCollider != null)
			{
				this.currentCollider = foundCollider;
				this.currentCollider.enabled = true;
				this.currentAttachment = bbAttachment;
				this.currentAttachmentName = this.nameTable[bbAttachment];
				return;
			}
			this.currentCollider = null;
			this.currentAttachment = bbAttachment;
			this.currentAttachmentName = null;
			if (BoundingBoxFollowerGraphic.DebugMessages)
			{
				Debug.LogFormat("Collider for BoundingBoxAttachment named '{0}' was not initialized. It is possibly from a new skin. currentAttachmentName will be null. You may need to call BoundingBoxFollowerGraphic.Initialize(overwrite: true);", new object[] { bbAttachment.Name });
			}
		}

		// Token: 0x04000080 RID: 128
		internal static bool DebugMessages = true;

		// Token: 0x04000081 RID: 129
		public SkeletonGraphic skeletonGraphic;

		// Token: 0x04000082 RID: 130
		[SpineSlot("", "skeletonGraphic", true, true, false)]
		public string slotName;

		// Token: 0x04000083 RID: 131
		public bool isTrigger;

		// Token: 0x04000084 RID: 132
		public bool usedByEffector;

		// Token: 0x04000085 RID: 133
		public bool usedByComposite;

		// Token: 0x04000086 RID: 134
		public bool clearStateOnDisable = true;

		// Token: 0x04000087 RID: 135
		private Slot slot;

		// Token: 0x04000088 RID: 136
		private BoundingBoxAttachment currentAttachment;

		// Token: 0x04000089 RID: 137
		private string currentAttachmentName;

		// Token: 0x0400008A RID: 138
		private PolygonCollider2D currentCollider;

		// Token: 0x0400008B RID: 139
		public readonly Dictionary<BoundingBoxAttachment, PolygonCollider2D> colliderTable = new Dictionary<BoundingBoxAttachment, PolygonCollider2D>();

		// Token: 0x0400008C RID: 140
		public readonly Dictionary<BoundingBoxAttachment, string> nameTable = new Dictionary<BoundingBoxAttachment, string>();
	}
}
