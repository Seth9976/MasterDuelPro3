using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200052A RID: 1322
	public class LayerSetter : MonoBehaviour
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x0000216D File Offset: 0x0000036D
		public LayerSetter.Layer layer
		{
			get
			{
				return LayerSetter.Layer.Default;
			}
			set
			{
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x0000216D File Offset: 0x0000036D
		public bool autoUpdate
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x0000216D File Offset: 0x0000036D
		public bool setOnAwake
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0000216D File Offset: 0x0000036D
		public void Apply()
		{
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLayer(LayerSetter.Layer layer)
		{
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLayer(LayerSetter.Layer layer)
		{
			return 0;
		}

		// Token: 0x0400299C RID: 10652
		[SerializeField]
		private LayerSetter.Layer _layer;

		// Token: 0x0400299D RID: 10653
		[SerializeField]
		private bool _autoUpdate;

		// Token: 0x0400299E RID: 10654
		[SerializeField]
		private bool _setOnAwake;

		// Token: 0x0200052B RID: 1323
		public enum Layer
		{
			// Token: 0x040029A0 RID: 10656
			Default,
			// Token: 0x040029A1 RID: 10657
			Overlay3D,
			// Token: 0x040029A2 RID: 10658
			Overlay2D,
			// Token: 0x040029A3 RID: 10659
			OverlayEffect3D,
			// Token: 0x040029A4 RID: 10660
			OverlayEffect2D
		}
	}
}
