using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000021 RID: 33
	[ExecuteAlways]
	[HelpURL("http://esotericsoftware.com/spine-unity#BoundingBoxFollower")]
	public class BoundingBoxFollower : MonoBehaviour
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004FDC File Offset: 0x000031DC
		public Slot Slot
		{
			get
			{
				return this.slot;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004FE4 File Offset: 0x000031E4
		public BoundingBoxAttachment CurrentAttachment
		{
			get
			{
				return this.currentAttachment;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004FEC File Offset: 0x000031EC
		public string CurrentAttachmentName
		{
			get
			{
				return this.currentAttachmentName;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004FF4 File Offset: 0x000031F4
		public PolygonCollider2D CurrentCollider
		{
			get
			{
				return this.currentCollider;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004FFC File Offset: 0x000031FC
		public bool IsTrigger
		{
			get
			{
				return this.isTrigger;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005004 File Offset: 0x00003204
		private void Start()
		{
			this.Initialize(false);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005010 File Offset: 0x00003210
		private void OnEnable()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRebuild;
				this.skeletonRenderer.OnRebuild += this.HandleRebuild;
			}
			this.Initialize(false);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005004 File Offset: 0x00003204
		private void HandleRebuild(SkeletonRenderer sr)
		{
			this.Initialize(false);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005060 File Offset: 0x00003260
		public void Initialize(bool overwrite = false)
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.Initialize(false, false);
			if (string.IsNullOrEmpty(this.slotName))
			{
				return;
			}
			if (!overwrite && this.colliderTable.Count > 0 && this.slot != null && this.skeletonRenderer.skeleton == this.slot.Skeleton && this.slotName == this.slot.Data.Name)
			{
				return;
			}
			this.slot = null;
			this.currentAttachment = null;
			this.currentAttachmentName = null;
			this.currentCollider = null;
			this.colliderTable.Clear();
			this.nameTable.Clear();
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			if (skeleton == null)
			{
				return;
			}
			this.slot = skeleton.FindSlot(this.slotName);
			if (this.slot == null)
			{
				if (BoundingBoxFollower.DebugMessages)
				{
					Debug.LogWarning(string.Format("Slot '{0}' not found for BoundingBoxFollower on '{1}'. (Previous colliders were disposed.)", this.slotName, base.gameObject.name));
				}
				return;
			}
			int slotIndex = this.slot.Data.Index;
			int requiredCollidersCount = 0;
			PolygonCollider2D[] colliders = base.GetComponents<PolygonCollider2D>();
			if (base.gameObject.activeInHierarchy)
			{
				foreach (Skin skin in skeleton.Data.Skins)
				{
					this.AddCollidersForSkin(skin, slotIndex, colliders, ref requiredCollidersCount);
				}
				if (skeleton.Skin != null)
				{
					this.AddCollidersForSkin(skeleton.Skin, slotIndex, colliders, ref requiredCollidersCount);
				}
			}
			this.DisposeExcessCollidersAfter(requiredCollidersCount);
			if (BoundingBoxFollower.DebugMessages && this.colliderTable.Count == 0)
			{
				if (base.gameObject.activeInHierarchy)
				{
					Debug.LogWarning("Bounding Box Follower not valid! Slot [" + this.slotName + "] does not contain any Bounding Box Attachments!");
					return;
				}
				Debug.LogWarning("Bounding Box Follower tried to rebuild as a prefab.");
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000524C File Offset: 0x0000344C
		private void AddCollidersForSkin(Skin skin, int slotIndex, PolygonCollider2D[] previousColliders, ref int collidersCount)
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
				if (BoundingBoxFollower.DebugMessages && attachment != null && boundingBoxAttachment == null)
				{
					Debug.Log("BoundingBoxFollower tried to follow a slot that contains non-boundingbox attachments: " + this.slotName);
				}
				if (boundingBoxAttachment != null && !this.colliderTable.ContainsKey(boundingBoxAttachment))
				{
					PolygonCollider2D bbCollider = ((collidersCount < previousColliders.Length) ? previousColliders[collidersCount] : base.gameObject.AddComponent<PolygonCollider2D>());
					collidersCount++;
					SkeletonUtility.SetColliderPointsLocal(bbCollider, this.slot, boundingBoxAttachment, 1f);
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

		// Token: 0x060000B3 RID: 179 RVA: 0x0000539C File Offset: 0x0000359C
		private void OnDisable()
		{
			if (this.clearStateOnDisable)
			{
				this.ClearState();
			}
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRebuild;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000053D4 File Offset: 0x000035D4
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

		// Token: 0x060000B5 RID: 181 RVA: 0x00005448 File Offset: 0x00003648
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

		// Token: 0x060000B6 RID: 182 RVA: 0x00005482 File Offset: 0x00003682
		private void LateUpdate()
		{
			if (this.slot != null && this.slot.Attachment != this.currentAttachment)
			{
				this.MatchAttachment(this.slot.Attachment);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000054B0 File Offset: 0x000036B0
		private void MatchAttachment(Attachment attachment)
		{
			BoundingBoxAttachment bbAttachment = attachment as BoundingBoxAttachment;
			if (BoundingBoxFollower.DebugMessages && attachment != null && bbAttachment == null)
			{
				Debug.LogWarning("BoundingBoxFollower tried to match a non-boundingbox attachment. It will treat it as null.");
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
			if (BoundingBoxFollower.DebugMessages)
			{
				Debug.LogFormat("Collider for BoundingBoxAttachment named '{0}' was not initialized. It is possibly from a new skin. currentAttachmentName will be null. You may need to call BoundingBoxFollower.Initialize(overwrite: true);", new object[] { bbAttachment.Name });
			}
		}

		// Token: 0x04000073 RID: 115
		internal static bool DebugMessages = true;

		// Token: 0x04000074 RID: 116
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x04000075 RID: 117
		[SpineSlot("", "skeletonRenderer", true, true, false)]
		public string slotName;

		// Token: 0x04000076 RID: 118
		public bool isTrigger;

		// Token: 0x04000077 RID: 119
		public bool usedByEffector;

		// Token: 0x04000078 RID: 120
		public bool usedByComposite;

		// Token: 0x04000079 RID: 121
		public bool clearStateOnDisable = true;

		// Token: 0x0400007A RID: 122
		private Slot slot;

		// Token: 0x0400007B RID: 123
		private BoundingBoxAttachment currentAttachment;

		// Token: 0x0400007C RID: 124
		private string currentAttachmentName;

		// Token: 0x0400007D RID: 125
		private PolygonCollider2D currentCollider;

		// Token: 0x0400007E RID: 126
		public readonly Dictionary<BoundingBoxAttachment, PolygonCollider2D> colliderTable = new Dictionary<BoundingBoxAttachment, PolygonCollider2D>();

		// Token: 0x0400007F RID: 127
		public readonly Dictionary<BoundingBoxAttachment, string> nameTable = new Dictionary<BoundingBoxAttachment, string>();
	}
}
