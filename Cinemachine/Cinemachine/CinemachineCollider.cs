using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x0200001A RID: 26
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineCollider.html")]
	public class CinemachineCollider : CinemachineExtension
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004BDE File Offset: 0x00002DDE
		public bool IsTargetObscured(ICinemachineCamera vcam)
		{
			return base.GetExtraState<CinemachineCollider.VcamExtraState>(vcam).targetObscured;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004BEC File Offset: 0x00002DEC
		public bool CameraWasDisplaced(ICinemachineCamera vcam)
		{
			return this.GetCameraDisplacementDistance(vcam) > 0f;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004BFC File Offset: 0x00002DFC
		public float GetCameraDisplacementDistance(ICinemachineCamera vcam)
		{
			return base.GetExtraState<CinemachineCollider.VcamExtraState>(vcam).previousDisplacement.magnitude;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004C10 File Offset: 0x00002E10
		private void OnValidate()
		{
			this.m_DistanceLimit = Mathf.Max(0f, this.m_DistanceLimit);
			this.m_MinimumOcclusionTime = Mathf.Max(0f, this.m_MinimumOcclusionTime);
			this.m_CameraRadius = Mathf.Max(0f, this.m_CameraRadius);
			this.m_MinimumDistanceFromTarget = Mathf.Max(0.01f, this.m_MinimumDistanceFromTarget);
			this.m_OptimalTargetDistance = Mathf.Max(0f, this.m_OptimalTargetDistance);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C8B File Offset: 0x00002E8B
		protected override void OnDestroy()
		{
			RuntimeUtility.DestroyScratchCollider();
			base.OnDestroy();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004C98 File Offset: 0x00002E98
		public List<List<Vector3>> DebugPaths
		{
			get
			{
				List<List<Vector3>> list = new List<List<Vector3>>();
				foreach (CinemachineCollider.VcamExtraState v in base.GetAllExtraStates<CinemachineCollider.VcamExtraState>())
				{
					if (v.debugResolutionPath != null && v.debugResolutionPath.Count > 0)
					{
						list.Add(v.debugResolutionPath);
					}
				}
				return list;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004D10 File Offset: 0x00002F10
		public override float GetMaxDampTime()
		{
			return Mathf.Max(this.m_Damping, Mathf.Max(this.m_DampingWhenOccluded, this.m_SmoothingTime));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004D30 File Offset: 0x00002F30
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			List<CinemachineCollider.VcamExtraState> states = base.GetAllExtraStates<CinemachineCollider.VcamExtraState>();
			for (int i = 0; i < states.Count; i++)
			{
				states[i].previousCameraPosition += positionDelta;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004D70 File Offset: 0x00002F70
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Body)
			{
				CinemachineCollider.VcamExtraState extra = base.GetExtraState<CinemachineCollider.VcamExtraState>(vcam);
				extra.targetObscured = false;
				List<Vector3> debugResolutionPath = extra.debugResolutionPath;
				if (debugResolutionPath != null)
				{
					debugResolutionPath.RemoveRange(0, extra.debugResolutionPath.Count);
				}
				if (this.m_AvoidObstacles)
				{
					Vector3 initialCamPos = state.CorrectedPosition;
					Quaternion dampingBypass = Quaternion.Euler(state.PositionDampingBypass);
					extra.previousDisplacement = dampingBypass * extra.previousDisplacement;
					Vector3 displacement = this.PreserveLineOfSight(ref state, ref extra);
					if (this.m_MinimumOcclusionTime > 0.0001f)
					{
						float now = CinemachineCore.CurrentTime;
						if (displacement.AlmostZero())
						{
							extra.occlusionStartTime = 0f;
						}
						else
						{
							if (extra.occlusionStartTime <= 0f)
							{
								extra.occlusionStartTime = now;
							}
							if (now - extra.occlusionStartTime < this.m_MinimumOcclusionTime)
							{
								displacement = extra.previousDisplacement;
							}
						}
					}
					if (this.m_SmoothingTime > 0.0001f && state.HasLookAt)
					{
						Vector3 pos = initialCamPos + displacement;
						Vector3 dir = pos - state.ReferenceLookAt;
						float distance = dir.magnitude;
						if (distance > 0.0001f)
						{
							dir /= distance;
							if (!displacement.AlmostZero())
							{
								extra.UpdateDistanceSmoothing(distance);
							}
							distance = extra.ApplyDistanceSmoothing(distance, this.m_SmoothingTime);
							displacement += state.ReferenceLookAt + dir * distance - pos;
						}
					}
					if (displacement.AlmostZero())
					{
						extra.ResetDistanceSmoothing(this.m_SmoothingTime);
					}
					Vector3 cameraPos = initialCamPos + displacement;
					Vector3 lookAt = (state.HasLookAt ? state.ReferenceLookAt : cameraPos);
					displacement += this.RespectCameraRadius(cameraPos, lookAt);
					float dampTime = this.m_DampingWhenOccluded;
					if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid && this.m_DampingWhenOccluded + this.m_Damping > 0.0001f)
					{
						float sqrMagnitude = displacement.sqrMagnitude;
						dampTime = ((sqrMagnitude > extra.previousDisplacement.sqrMagnitude) ? this.m_DampingWhenOccluded : this.m_Damping);
						if (sqrMagnitude < 0.0001f)
						{
							dampTime = extra.previousDampTime - Damper.Damp(extra.previousDampTime, dampTime, deltaTime);
						}
						if (dampTime > 0f)
						{
							bool bodyAfterAim = false;
							if (vcam is CinemachineVirtualCamera)
							{
								CinemachineComponentBase body = (vcam as CinemachineVirtualCamera).GetCinemachineComponent(CinemachineCore.Stage.Body);
								bodyAfterAim = body != null && body.BodyAppliesAfterAim;
							}
							Vector3 prevDisplacement = (bodyAfterAim ? extra.previousDisplacement : (lookAt + dampingBypass * extra.previousCameraOffset - initialCamPos));
							displacement = prevDisplacement + Damper.Damp(displacement - prevDisplacement, dampTime, deltaTime);
						}
					}
					state.PositionCorrection += displacement;
					cameraPos = state.CorrectedPosition;
					if (state.HasLookAt && base.VirtualCamera.PreviousStateIsValid)
					{
						Vector3 dir2 = extra.previousCameraPosition - state.ReferenceLookAt;
						Vector3 dir3 = cameraPos - state.ReferenceLookAt;
						if (dir2.sqrMagnitude > 0.0001f && dir3.sqrMagnitude > 0.0001f)
						{
							state.PositionDampingBypass = UnityVectorExtensions.SafeFromToRotation(dir2, dir3, state.ReferenceUp).eulerAngles;
						}
					}
					extra.previousDisplacement = displacement;
					extra.previousCameraOffset = cameraPos - lookAt;
					extra.previousCameraPosition = cameraPos;
					extra.previousDampTime = dampTime;
				}
			}
			if (stage == CinemachineCore.Stage.Aim)
			{
				CinemachineCollider.VcamExtraState extraState = base.GetExtraState<CinemachineCollider.VcamExtraState>(vcam);
				extraState.targetObscured = CinemachineCollider.IsTargetOffscreen(state) || this.CheckForTargetObstructions(state);
				if (extraState.targetObscured)
				{
					state.ShotQuality *= 0.2f;
				}
				if (!extraState.previousDisplacement.AlmostZero())
				{
					state.ShotQuality *= 0.8f;
				}
				float nearnessBoost = 0f;
				if (this.m_OptimalTargetDistance > 0f && state.HasLookAt)
				{
					float distance2 = Vector3.Magnitude(state.ReferenceLookAt - state.FinalPosition);
					if (distance2 <= this.m_OptimalTargetDistance)
					{
						float threshold = this.m_OptimalTargetDistance / 2f;
						if (distance2 >= threshold)
						{
							nearnessBoost = 0.2f * (distance2 - threshold) / (this.m_OptimalTargetDistance - threshold);
						}
					}
					else
					{
						distance2 -= this.m_OptimalTargetDistance;
						float threshold2 = this.m_OptimalTargetDistance * 3f;
						if (distance2 < threshold2)
						{
							nearnessBoost = 0.2f * (1f - distance2 / threshold2);
						}
					}
					state.ShotQuality *= 1f + nearnessBoost;
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000051D4 File Offset: 0x000033D4
		private Vector3 PreserveLineOfSight(ref CameraState state, ref CinemachineCollider.VcamExtraState extra)
		{
			Vector3 displacement = Vector3.zero;
			if (state.HasLookAt && this.m_CollideAgainst != 0 && this.m_CollideAgainst != this.m_TransparentLayers)
			{
				Vector3 cameraPos = state.CorrectedPosition;
				Vector3 lookAtPos = state.ReferenceLookAt;
				RaycastHit hitInfo = default(RaycastHit);
				displacement = this.PullCameraInFrontOfNearestObstacle(cameraPos, lookAtPos, this.m_CollideAgainst & ~this.m_TransparentLayers, ref hitInfo);
				Vector3 pos = cameraPos + displacement;
				if (hitInfo.collider != null)
				{
					extra.AddPointToDebugPath(pos);
					if (this.m_Strategy != CinemachineCollider.ResolutionStrategy.PullCameraForward)
					{
						Vector3 targetToCamera = cameraPos - lookAtPos;
						pos = this.PushCameraBack(pos, targetToCamera, hitInfo, lookAtPos, new Plane(state.ReferenceUp, cameraPos), targetToCamera.magnitude, this.m_MaximumEffort, ref extra);
					}
				}
				displacement = pos - cameraPos;
			}
			return displacement;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000052BC File Offset: 0x000034BC
		private Vector3 PullCameraInFrontOfNearestObstacle(Vector3 cameraPos, Vector3 lookAtPos, int layerMask, ref RaycastHit hitInfo)
		{
			Vector3 displacement = Vector3.zero;
			Vector3 dir = cameraPos - lookAtPos;
			float targetDistance = dir.magnitude;
			if (targetDistance > 0.0001f)
			{
				dir /= targetDistance;
				float minDistanceFromTarget = Mathf.Max(this.m_MinimumDistanceFromTarget, 0.0001f);
				if (targetDistance < minDistanceFromTarget + 0.0001f)
				{
					displacement = dir * (minDistanceFromTarget - targetDistance);
				}
				else
				{
					float rayLength = targetDistance - minDistanceFromTarget;
					if (this.m_DistanceLimit > 0.0001f)
					{
						rayLength = Mathf.Min(this.m_DistanceLimit, rayLength);
					}
					Ray ray = new Ray(cameraPos - rayLength * dir, dir);
					rayLength += 0.001f;
					if (rayLength > 0.0001f)
					{
						if (this.m_Strategy == CinemachineCollider.ResolutionStrategy.PullCameraForward && this.m_CameraRadius >= 0.0001f)
						{
							if (RuntimeUtility.SphereCastIgnoreTag(lookAtPos + dir * this.m_CameraRadius, this.m_CameraRadius, dir, out hitInfo, rayLength - this.m_CameraRadius, layerMask, in this.m_IgnoreTag))
							{
								displacement = hitInfo.point + hitInfo.normal * this.m_CameraRadius - cameraPos;
							}
						}
						else if (RuntimeUtility.RaycastIgnoreTag(ray, out hitInfo, rayLength, layerMask, in this.m_IgnoreTag))
						{
							float adjustment = Mathf.Max(0f, hitInfo.distance - 0.001f);
							displacement = ray.GetPoint(adjustment) - cameraPos;
						}
					}
				}
			}
			return displacement;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005418 File Offset: 0x00003618
		private Vector3 PushCameraBack(Vector3 currentPos, Vector3 pushDir, RaycastHit obstacle, Vector3 lookAtPos, Plane startPlane, float targetDistance, int iterations, ref CinemachineCollider.VcamExtraState extra)
		{
			Vector3 dir = Vector3.zero;
			if (!this.GetWalkingDirection(currentPos, pushDir, obstacle, ref dir))
			{
				return currentPos;
			}
			Ray ray = new Ray(currentPos, dir);
			float distance = this.GetPushBackDistance(ray, startPlane, targetDistance, lookAtPos);
			if (distance <= 0.0001f)
			{
				return currentPos;
			}
			float clampedDistance = CinemachineCollider.ClampRayToBounds(ray, distance, obstacle.collider.bounds);
			distance = Mathf.Min(distance, clampedDistance + 0.001f);
			RaycastHit hitInfo;
			Vector3 pos;
			if (RuntimeUtility.RaycastIgnoreTag(ray, out hitInfo, distance, this.m_CollideAgainst & ~this.m_TransparentLayers, in this.m_IgnoreTag))
			{
				float adjustment = hitInfo.distance - 0.001f;
				pos = ray.GetPoint(adjustment);
				extra.AddPointToDebugPath(pos);
				if (iterations > 1)
				{
					pos = this.PushCameraBack(pos, dir, hitInfo, lookAtPos, startPlane, targetDistance, iterations - 1, ref extra);
				}
				return pos;
			}
			pos = ray.GetPoint(distance);
			dir = pos - lookAtPos;
			float d = dir.magnitude;
			RaycastHit raycastHit;
			if (d < 0.0001f || RuntimeUtility.RaycastIgnoreTag(new Ray(lookAtPos, dir), out raycastHit, d - 0.001f, this.m_CollideAgainst & ~this.m_TransparentLayers, in this.m_IgnoreTag))
			{
				return currentPos;
			}
			ray = new Ray(pos, dir);
			extra.AddPointToDebugPath(pos);
			distance = this.GetPushBackDistance(ray, startPlane, targetDistance, lookAtPos);
			if (distance > 0.0001f)
			{
				if (!RuntimeUtility.RaycastIgnoreTag(ray, out hitInfo, distance, this.m_CollideAgainst & ~this.m_TransparentLayers, in this.m_IgnoreTag))
				{
					pos = ray.GetPoint(distance);
					extra.AddPointToDebugPath(pos);
				}
				else
				{
					float adjustment2 = hitInfo.distance - 0.001f;
					pos = ray.GetPoint(adjustment2);
					extra.AddPointToDebugPath(pos);
					if (iterations > 1)
					{
						pos = this.PushCameraBack(pos, dir, hitInfo, lookAtPos, startPlane, targetDistance, iterations - 1, ref extra);
					}
				}
			}
			return pos;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000055EC File Offset: 0x000037EC
		private bool GetWalkingDirection(Vector3 pos, Vector3 pushDir, RaycastHit obstacle, ref Vector3 outDir)
		{
			Vector3 normal2 = obstacle.normal;
			float nearbyDistance = 0.0050000004f;
			int numFound = Physics.SphereCastNonAlloc(pos, nearbyDistance, pushDir.normalized, this.m_CornerBuffer, 0f, this.m_CollideAgainst & ~this.m_TransparentLayers, QueryTriggerInteraction.Ignore);
			if (numFound > 1)
			{
				for (int i = 0; i < numFound; i++)
				{
					if (!(this.m_CornerBuffer[i].collider == null) && (this.m_IgnoreTag.Length <= 0 || !this.m_CornerBuffer[i].collider.CompareTag(this.m_IgnoreTag)))
					{
						Type type = this.m_CornerBuffer[i].collider.GetType();
						if (type == typeof(BoxCollider) || type == typeof(SphereCollider) || type == typeof(CapsuleCollider))
						{
							Vector3 d = this.m_CornerBuffer[i].collider.ClosestPoint(pos) - pos;
							if (d.magnitude > 1E-05f && this.m_CornerBuffer[i].collider.Raycast(new Ray(pos, d), out this.m_CornerBuffer[i], nearbyDistance))
							{
								if (!(this.m_CornerBuffer[i].normal - obstacle.normal).AlmostZero())
								{
									normal2 = this.m_CornerBuffer[i].normal;
									break;
								}
								break;
							}
						}
					}
				}
			}
			Vector3 dir = Vector3.Cross(obstacle.normal, normal2);
			if (dir.AlmostZero())
			{
				dir = Vector3.ProjectOnPlane(pushDir, obstacle.normal);
			}
			else
			{
				float dot = Vector3.Dot(dir, pushDir);
				if (Mathf.Abs(dot) < 0.0001f)
				{
					return false;
				}
				if (dot < 0f)
				{
					dir = -dir;
				}
			}
			if (dir.AlmostZero())
			{
				return false;
			}
			outDir = dir.normalized;
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000057F8 File Offset: 0x000039F8
		private float GetPushBackDistance(Ray ray, Plane startPlane, float targetDistance, Vector3 lookAtPos)
		{
			float maxDistance = targetDistance - (ray.origin - lookAtPos).magnitude;
			if (maxDistance < 0.0001f)
			{
				return 0f;
			}
			if (this.m_Strategy == CinemachineCollider.ResolutionStrategy.PreserveCameraDistance)
			{
				return maxDistance;
			}
			float distance;
			if (!startPlane.Raycast(ray, out distance))
			{
				distance = 0f;
			}
			distance = Mathf.Min(maxDistance, distance);
			if (distance < 0.0001f)
			{
				return 0f;
			}
			float angle = Mathf.Abs(UnityVectorExtensions.Angle(startPlane.normal, ray.direction) - 90f);
			if (angle < 0.1f)
			{
				distance = Mathf.Lerp(0f, distance, angle / 0.1f);
			}
			return distance;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000589C File Offset: 0x00003A9C
		private static float ClampRayToBounds(Ray ray, float distance, Bounds bounds)
		{
			float d;
			if (Vector3.Dot(ray.direction, Vector3.up) > 0f)
			{
				if (new Plane(Vector3.down, bounds.max).Raycast(ray, out d) && d > 0.0001f)
				{
					distance = Mathf.Min(distance, d);
				}
			}
			else if (Vector3.Dot(ray.direction, Vector3.down) > 0f && new Plane(Vector3.up, bounds.min).Raycast(ray, out d) && d > 0.0001f)
			{
				distance = Mathf.Min(distance, d);
			}
			if (Vector3.Dot(ray.direction, Vector3.right) > 0f)
			{
				if (new Plane(Vector3.left, bounds.max).Raycast(ray, out d) && d > 0.0001f)
				{
					distance = Mathf.Min(distance, d);
				}
			}
			else if (Vector3.Dot(ray.direction, Vector3.left) > 0f && new Plane(Vector3.right, bounds.min).Raycast(ray, out d) && d > 0.0001f)
			{
				distance = Mathf.Min(distance, d);
			}
			if (Vector3.Dot(ray.direction, Vector3.forward) > 0f)
			{
				if (new Plane(Vector3.back, bounds.max).Raycast(ray, out d) && d > 0.0001f)
				{
					distance = Mathf.Min(distance, d);
				}
			}
			else if (Vector3.Dot(ray.direction, Vector3.back) > 0f && new Plane(Vector3.forward, bounds.min).Raycast(ray, out d) && d > 0.0001f)
			{
				distance = Mathf.Min(distance, d);
			}
			return distance;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005A5C File Offset: 0x00003C5C
		private Vector3 RespectCameraRadius(Vector3 cameraPos, Vector3 lookAtPos)
		{
			Vector3 result = Vector3.zero;
			if (this.m_CameraRadius < 0.0001f || this.m_CollideAgainst == 0)
			{
				return result;
			}
			Vector3 dir = cameraPos - lookAtPos;
			float distance = dir.magnitude;
			if (distance > 0.0001f)
			{
				dir /= distance;
			}
			int numObstacles = Physics.OverlapSphereNonAlloc(cameraPos, this.m_CameraRadius, CinemachineCollider.s_ColliderBuffer, this.m_CollideAgainst, QueryTriggerInteraction.Ignore);
			if (numObstacles == 0 && this.m_TransparentLayers != 0 && distance > this.m_MinimumDistanceFromTarget + 0.0001f)
			{
				float d = distance - this.m_MinimumDistanceFromTarget;
				RaycastHit hitInfo;
				if (RuntimeUtility.RaycastIgnoreTag(new Ray(lookAtPos + dir * this.m_MinimumDistanceFromTarget, dir), out hitInfo, d, this.m_CollideAgainst, in this.m_IgnoreTag))
				{
					Collider c = hitInfo.collider;
					if (!c.Raycast(new Ray(cameraPos, -dir), out hitInfo, d))
					{
						CinemachineCollider.s_ColliderBuffer[numObstacles++] = c;
					}
				}
			}
			if ((numObstacles > 0 && distance == 0f) || distance > this.m_MinimumDistanceFromTarget)
			{
				SphereCollider scratchCollider = RuntimeUtility.GetScratchCollider();
				scratchCollider.radius = this.m_CameraRadius;
				Vector3 newCamPos = cameraPos;
				for (int i = 0; i < numObstacles; i++)
				{
					Collider c2 = CinemachineCollider.s_ColliderBuffer[i];
					if (this.m_IgnoreTag.Length <= 0 || !c2.CompareTag(this.m_IgnoreTag))
					{
						if (distance > this.m_MinimumDistanceFromTarget)
						{
							dir = newCamPos - lookAtPos;
							float d2 = dir.magnitude;
							if (d2 > 0.0001f)
							{
								dir /= d2;
								Ray ray = new Ray(lookAtPos, dir);
								RaycastHit hitInfo;
								if (c2.Raycast(ray, out hitInfo, d2 + this.m_CameraRadius))
								{
									newCamPos = ray.GetPoint(hitInfo.distance) - dir * 0.001f;
								}
							}
						}
						Vector3 offsetDir;
						float offsetDistance;
						if (Physics.ComputePenetration(scratchCollider, newCamPos, Quaternion.identity, c2, c2.transform.position, c2.transform.rotation, out offsetDir, out offsetDistance))
						{
							newCamPos += offsetDir * offsetDistance;
						}
					}
				}
				result = newCamPos - cameraPos;
			}
			if (distance > 0.0001f && this.m_MinimumDistanceFromTarget > 0.0001f)
			{
				float minDistance = Mathf.Max(this.m_MinimumDistanceFromTarget, this.m_CameraRadius) + 0.001f;
				if ((cameraPos + result - lookAtPos).magnitude < minDistance)
				{
					result = lookAtPos - cameraPos + dir * minDistance;
				}
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005CE0 File Offset: 0x00003EE0
		private bool CheckForTargetObstructions(CameraState state)
		{
			if (state.HasLookAt)
			{
				Vector3 referenceLookAt = state.ReferenceLookAt;
				Vector3 pos = state.CorrectedPosition;
				Vector3 dir = referenceLookAt - pos;
				float distance = dir.magnitude;
				if (distance < Mathf.Max(this.m_MinimumDistanceFromTarget, 0.0001f))
				{
					return true;
				}
				RaycastHit raycastHit;
				if (RuntimeUtility.RaycastIgnoreTag(new Ray(pos, dir.normalized), out raycastHit, distance - this.m_MinimumDistanceFromTarget, this.m_CollideAgainst & ~this.m_TransparentLayers, in this.m_IgnoreTag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005D68 File Offset: 0x00003F68
		private static bool IsTargetOffscreen(CameraState state)
		{
			if (state.HasLookAt)
			{
				Vector3 dir = state.ReferenceLookAt - state.CorrectedPosition;
				dir = Quaternion.Inverse(state.CorrectedOrientation) * dir;
				if (state.Lens.Orthographic)
				{
					if (Mathf.Abs(dir.y) > state.Lens.OrthographicSize)
					{
						return true;
					}
					if (Mathf.Abs(dir.x) > state.Lens.OrthographicSize * state.Lens.Aspect)
					{
						return true;
					}
				}
				else
				{
					float fov = state.Lens.FieldOfView / 2f;
					if (UnityVectorExtensions.Angle(dir.ProjectOntoPlane(Vector3.right), Vector3.forward) > fov)
					{
						return true;
					}
					fov = 57.29578f * Mathf.Atan(Mathf.Tan(fov * 0.017453292f) * state.Lens.Aspect);
					if (UnityVectorExtensions.Angle(dir.ProjectOntoPlane(Vector3.up), Vector3.forward) > fov)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400006D RID: 109
		[Header("Obstacle Detection")]
		[Tooltip("Objects on these layers will be detected")]
		public LayerMask m_CollideAgainst = 1;

		// Token: 0x0400006E RID: 110
		[TagField]
		[Tooltip("Obstacles with this tag will be ignored.  It is a good idea to set this field to the target's tag")]
		public string m_IgnoreTag = string.Empty;

		// Token: 0x0400006F RID: 111
		[Tooltip("Objects on these layers will never obstruct view of the target")]
		public LayerMask m_TransparentLayers = 0;

		// Token: 0x04000070 RID: 112
		[Tooltip("Obstacles closer to the target than this will be ignored")]
		public float m_MinimumDistanceFromTarget = 0.1f;

		// Token: 0x04000071 RID: 113
		[Space]
		[Tooltip("When enabled, will attempt to resolve situations where the line of sight to the target is blocked by an obstacle")]
		[FormerlySerializedAs("m_PreserveLineOfSight")]
		public bool m_AvoidObstacles = true;

		// Token: 0x04000072 RID: 114
		[Tooltip("The maximum raycast distance when checking if the line of sight to this camera's target is clear.  If the setting is 0 or less, the current actual distance to target will be used.")]
		[FormerlySerializedAs("m_LineOfSightFeelerDistance")]
		public float m_DistanceLimit;

		// Token: 0x04000073 RID: 115
		[Tooltip("Don't take action unless occlusion has lasted at least this long.")]
		public float m_MinimumOcclusionTime;

		// Token: 0x04000074 RID: 116
		[Tooltip("Camera will try to maintain this distance from any obstacle.  Try to keep this value small.  Increase it if you are seeing inside obstacles due to a large FOV on the camera.")]
		public float m_CameraRadius = 0.1f;

		// Token: 0x04000075 RID: 117
		[Tooltip("The way in which the Collider will attempt to preserve sight of the target.")]
		public CinemachineCollider.ResolutionStrategy m_Strategy = CinemachineCollider.ResolutionStrategy.PreserveCameraHeight;

		// Token: 0x04000076 RID: 118
		[Range(1f, 10f)]
		[Tooltip("Upper limit on how many obstacle hits to process.  Higher numbers may impact performance.  In most environments, 4 is enough.")]
		public int m_MaximumEffort = 4;

		// Token: 0x04000077 RID: 119
		[Range(0f, 2f)]
		[Tooltip("Smoothing to apply to obstruction resolution.  Nearest camera point is held for at least this long")]
		public float m_SmoothingTime;

		// Token: 0x04000078 RID: 120
		[Range(0f, 10f)]
		[Tooltip("How gradually the camera returns to its normal position after having been corrected.  Higher numbers will move the camera more gradually back to normal.")]
		[FormerlySerializedAs("m_Smoothing")]
		public float m_Damping;

		// Token: 0x04000079 RID: 121
		[Range(0f, 10f)]
		[Tooltip("How gradually the camera moves to resolve an occlusion.  Higher numbers will move the camera more gradually.")]
		public float m_DampingWhenOccluded;

		// Token: 0x0400007A RID: 122
		[Header("Shot Evaluation")]
		[Tooltip("If greater than zero, a higher score will be given to shots when the target is closer to this distance.  Set this to zero to disable this feature.")]
		public float m_OptimalTargetDistance;

		// Token: 0x0400007B RID: 123
		private const float k_PrecisionSlush = 0.001f;

		// Token: 0x0400007C RID: 124
		private RaycastHit[] m_CornerBuffer = new RaycastHit[4];

		// Token: 0x0400007D RID: 125
		private const float k_AngleThreshold = 0.1f;

		// Token: 0x0400007E RID: 126
		private static Collider[] s_ColliderBuffer = new Collider[5];

		// Token: 0x0200001B RID: 27
		public enum ResolutionStrategy
		{
			// Token: 0x04000080 RID: 128
			PullCameraForward,
			// Token: 0x04000081 RID: 129
			PreserveCameraHeight,
			// Token: 0x04000082 RID: 130
			PreserveCameraDistance
		}

		// Token: 0x0200001C RID: 28
		private class VcamExtraState
		{
			// Token: 0x060000A6 RID: 166 RVA: 0x0000429A File Offset: 0x0000249A
			public void AddPointToDebugPath(Vector3 p)
			{
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x00005EDE File Offset: 0x000040DE
			public float ApplyDistanceSmoothing(float distance, float smoothingTime)
			{
				if (this.m_SmoothedTime != 0f && smoothingTime > 0.0001f && CinemachineCore.CurrentTime - this.m_SmoothedTime < smoothingTime)
				{
					return Mathf.Min(distance, this.m_SmoothedDistance);
				}
				return distance;
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x00005F12 File Offset: 0x00004112
			public void UpdateDistanceSmoothing(float distance)
			{
				if (this.m_SmoothedDistance == 0f || distance < this.m_SmoothedDistance)
				{
					this.m_SmoothedDistance = distance;
					this.m_SmoothedTime = CinemachineCore.CurrentTime;
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x00005F3C File Offset: 0x0000413C
			public void ResetDistanceSmoothing(float smoothingTime)
			{
				if (CinemachineCore.CurrentTime - this.m_SmoothedTime >= smoothingTime)
				{
					this.m_SmoothedDistance = (this.m_SmoothedTime = 0f);
				}
			}

			// Token: 0x04000083 RID: 131
			public Vector3 previousDisplacement;

			// Token: 0x04000084 RID: 132
			public Vector3 previousCameraOffset;

			// Token: 0x04000085 RID: 133
			public Vector3 previousCameraPosition;

			// Token: 0x04000086 RID: 134
			public float previousDampTime;

			// Token: 0x04000087 RID: 135
			public bool targetObscured;

			// Token: 0x04000088 RID: 136
			public float occlusionStartTime;

			// Token: 0x04000089 RID: 137
			public List<Vector3> debugResolutionPath;

			// Token: 0x0400008A RID: 138
			private float m_SmoothedDistance;

			// Token: 0x0400008B RID: 139
			private float m_SmoothedTime;
		}
	}
}
