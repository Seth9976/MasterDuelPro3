using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;

namespace Willow
{
	// Token: 0x0200154C RID: 5452
	public class CustomTimelineObject : MonoBehaviour, ITimeControl
	{
		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06009E0F RID: 40463 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E10 RID: 40464 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSyncPosition
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06009E11 RID: 40465 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E12 RID: 40466 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSyncRotation
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06009E13 RID: 40467 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E14 RID: 40468 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSyncScale
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06009E15 RID: 40469 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E16 RID: 40470 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isLookAtCamera
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06009E17 RID: 40471 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E18 RID: 40472 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isIgnoreInitRotation
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06009E19 RID: 40473 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E1A RID: 40474 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isIgnoreInitScale
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06009E1B RID: 40475 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E1C RID: 40476 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRelayChild
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06009E1D RID: 40477 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform myTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06009E1E RID: 40478 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform transformChild
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06009E1F RID: 40479 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform transformSync
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06009E20 RID: 40480 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject targetCameraObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06009E21 RID: 40481 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject targetParentObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06009E22 RID: 40482 RVA: 0x0000216A File Offset: 0x0000036A
		private PositionConstraint positionConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06009E23 RID: 40483 RVA: 0x0000216A File Offset: 0x0000036A
		private RotationConstraint rotationConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06009E24 RID: 40484 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009E25 RID: 40485 RVA: 0x0000216D File Offset: 0x0000036D
		public static Camera cameraSubstituteOfMain
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06009E26 RID: 40486 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnControlTimeStart()
		{
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnControlTimeStop()
		{
		}

		// Token: 0x06009E28 RID: 40488 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTime(double time)
		{
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnValidate()
		{
		}

		// Token: 0x06009E2A RID: 40490 RVA: 0x0000216D File Offset: 0x0000036D
		private static void GetComponentRoots<T>(Transform t, ICollection<T> roots) where T : Component
		{
		}

		// Token: 0x06009E2B RID: 40491 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetParentObject(GameObject parent)
		{
		}

		// Token: 0x06009E2C RID: 40492 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateTransform(Transform target, bool isPosition, bool isRotation, bool isScale)
		{
			return false;
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x0000216D File Offset: 0x0000036D
		private void LookAtCamera(Transform target)
		{
		}

		// Token: 0x06009E2E RID: 40494 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearConstrain()
		{
		}

		// Token: 0x06009E2F RID: 40495 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetConstrain(bool isActive)
		{
		}

		// Token: 0x0400DDB9 RID: 56761
		private GameObject m_parentObject;

		// Token: 0x0400DDBA RID: 56762
		private Transform m_transform;

		// Token: 0x0400DDBB RID: 56763
		private Transform m_transformParent;

		// Token: 0x0400DDBC RID: 56764
		private Transform m_transformCamera;

		// Token: 0x0400DDBD RID: 56765
		private Transform m_transformAnim;

		// Token: 0x0400DDBE RID: 56766
		private Transform m_transformChild;

		// Token: 0x0400DDBF RID: 56767
		private bool m_init;

		// Token: 0x0400DDC0 RID: 56768
		private PositionConstraint m_positionConstraint;

		// Token: 0x0400DDC1 RID: 56769
		private RotationConstraint m_rotationConstraint;
	}
}
