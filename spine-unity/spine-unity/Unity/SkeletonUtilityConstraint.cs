using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000050 RID: 80
	[ExecuteAlways]
	[RequireComponent(typeof(SkeletonUtilityBone))]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonUtilityConstraint")]
	public abstract class SkeletonUtilityConstraint : MonoBehaviour
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000F513 File Offset: 0x0000D713
		protected virtual void OnEnable()
		{
			this.bone = base.GetComponent<SkeletonUtilityBone>();
			this.hierarchy = base.transform.GetComponentInParent<SkeletonUtility>();
			this.hierarchy.RegisterConstraint(this);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F53E File Offset: 0x0000D73E
		protected virtual void OnDisable()
		{
			this.hierarchy.UnregisterConstraint(this);
		}

		// Token: 0x060002D0 RID: 720
		public abstract void DoUpdate();

		// Token: 0x040001C6 RID: 454
		protected SkeletonUtilityBone bone;

		// Token: 0x040001C7 RID: 455
		protected SkeletonUtility hierarchy;
	}
}
