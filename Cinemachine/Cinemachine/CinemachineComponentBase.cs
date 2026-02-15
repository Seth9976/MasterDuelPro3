using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200006F RID: 111
	[DocumentationSorting(DocumentationSortingAttribute.Level.API)]
	public abstract class CinemachineComponentBase : MonoBehaviour
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000123C4 File Offset: 0x000105C4
		public CinemachineVirtualCameraBase VirtualCamera
		{
			get
			{
				if (this.m_vcamOwner == null)
				{
					this.m_vcamOwner = base.GetComponent<CinemachineVirtualCameraBase>();
				}
				if (this.m_vcamOwner == null && base.transform.parent != null)
				{
					this.m_vcamOwner = base.transform.parent.GetComponent<CinemachineVirtualCameraBase>();
				}
				return this.m_vcamOwner;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00012428 File Offset: 0x00010628
		public Transform FollowTarget
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera;
				if (!(vcam == null))
				{
					return vcam.ResolveFollow(vcam.Follow);
				}
				return null;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00012454 File Offset: 0x00010654
		public Transform LookAtTarget
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera;
				if (!(vcam == null))
				{
					return vcam.ResolveLookAt(vcam.LookAt);
				}
				return null;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00012480 File Offset: 0x00010680
		public ICinemachineTargetGroup AbstractFollowTargetGroup
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera;
				if (!(vcam == null))
				{
					return vcam.AbstractFollowTargetGroup;
				}
				return null;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000124A5 File Offset: 0x000106A5
		public CinemachineTargetGroup FollowTargetGroup
		{
			get
			{
				return this.AbstractFollowTargetGroup as CinemachineTargetGroup;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000124B4 File Offset: 0x000106B4
		public Vector3 FollowTargetPosition
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera.FollowTargetAsVcam;
				if (vcam != null)
				{
					return vcam.State.FinalPosition;
				}
				Transform target = this.FollowTarget;
				if (target != null)
				{
					return TargetPositionCache.GetTargetPosition(target);
				}
				return Vector3.zero;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00012504 File Offset: 0x00010704
		public Quaternion FollowTargetRotation
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera.FollowTargetAsVcam;
				if (vcam != null)
				{
					return vcam.State.FinalOrientation;
				}
				Transform target = this.FollowTarget;
				if (target != null)
				{
					return TargetPositionCache.GetTargetRotation(target);
				}
				return Quaternion.identity;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00012551 File Offset: 0x00010751
		public ICinemachineTargetGroup AbstractLookAtTargetGroup
		{
			get
			{
				return this.VirtualCamera.AbstractLookAtTargetGroup;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001255E File Offset: 0x0001075E
		public CinemachineTargetGroup LookAtTargetGroup
		{
			get
			{
				return this.AbstractLookAtTargetGroup as CinemachineTargetGroup;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001256C File Offset: 0x0001076C
		public Vector3 LookAtTargetPosition
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera.LookAtTargetAsVcam;
				if (vcam != null)
				{
					return vcam.State.FinalPosition;
				}
				Transform target = this.LookAtTarget;
				if (target != null)
				{
					return TargetPositionCache.GetTargetPosition(target);
				}
				return Vector3.zero;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000125BC File Offset: 0x000107BC
		public Quaternion LookAtTargetRotation
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera.LookAtTargetAsVcam;
				if (vcam != null)
				{
					return vcam.State.FinalOrientation;
				}
				Transform target = this.LookAtTarget;
				if (target != null)
				{
					return TargetPositionCache.GetTargetRotation(target);
				}
				return Quaternion.identity;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001260C File Offset: 0x0001080C
		public CameraState VcamState
		{
			get
			{
				CinemachineVirtualCameraBase vcam = this.VirtualCamera;
				if (!(vcam == null))
				{
					return vcam.State;
				}
				return CameraState.Default;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002B7 RID: 695
		public abstract bool IsValid { get; }

		// Token: 0x060002B8 RID: 696 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void PrePipelineMutateCameraState(ref CameraState curState, float deltaTime)
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002B9 RID: 697
		public abstract CinemachineCore.Stage Stage { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool BodyAppliesAfterAim
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002BB RID: 699
		public abstract void MutateCameraState(ref CameraState curState, float deltaTime);

		// Token: 0x060002BC RID: 700 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime, ref CinemachineVirtualCameraBase.TransitionParams transitionParams)
		{
			return false;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000429A File Offset: 0x0000249A
		public virtual void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public virtual float GetMaxDampTime()
		{
			return 0f;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool RequiresUserInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000296 RID: 662
		protected const float Epsilon = 0.0001f;

		// Token: 0x04000297 RID: 663
		private CinemachineVirtualCameraBase m_vcamOwner;
	}
}
