using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000079 RID: 121
	public class PathPlugin : ABSTweenPlugin<Vector3, Path, PathOptions>
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000AF2C File Offset: 0x0000912C
		public override void Reset(TweenerCore<Vector3, Path, PathOptions> t)
		{
			t.endValue.Destroy();
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void SetFrom(TweenerCore<Vector3, Path, PathOptions> t, bool isRelative)
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void SetFrom(TweenerCore<Vector3, Path, PathOptions> t, Path fromValue, bool setImmediately, bool isRelative)
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AF5D File Offset: 0x0000915D
		public static ABSTweenPlugin<Vector3, Path, PathOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<PathPlugin, Vector3, Path, PathOptions>();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000AF64 File Offset: 0x00009164
		public override Path ConvertToStartValue(TweenerCore<Vector3, Path, PathOptions> t, Vector3 value)
		{
			return t.endValue;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000AF6C File Offset: 0x0000916C
		public override void SetRelativeEndValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			if (t.endValue.isFinalized)
			{
				return;
			}
			Vector3 vector = t.getter();
			int num = t.endValue.wps.Length;
			for (int i = 0; i < num; i++)
			{
				t.endValue.wps[i] += vector;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public override void SetChangeValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			GameObject gameObject = t.target as GameObject;
			Transform transform = ((gameObject != null) ? gameObject.transform : ((Component)t.target).transform);
			if (t.plugOptions.orientType == OrientType.ToPath)
			{
				t.plugOptions.parent = transform.parent;
			}
			if (t.endValue.isFinalized)
			{
				t.changeValue = t.endValue;
				return;
			}
			Vector3 vector = t.getter();
			Path endValue = t.endValue;
			endValue.plugOptions = t.plugOptions;
			int num = endValue.wps.Length;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			if (num <= endValue.minInputWaypoints || !DOTweenUtils.Vector3AreApproximatelyEqual(endValue.wps[0], vector))
			{
				flag = true;
				num2++;
			}
			if (t.plugOptions.isClosedPath)
			{
				Vector3 vector2 = endValue.wps[num - 1];
				if (endValue.type == PathType.CubicBezier)
				{
					if (num < 3)
					{
						Debug.LogError("CubicBezier paths must contain waypoints in multiple of 3 excluding the starting point added automatically by DOTween (1: waypoint, 2: IN control point, 3: OUT control point — the minimum amount of waypoints for a single curve is 3)");
					}
					else
					{
						vector2 = endValue.wps[num - 3];
					}
				}
				if (vector2 != vector)
				{
					flag2 = true;
					num2++;
				}
			}
			Vector3[] array = new Vector3[num + num2];
			int num3 = (flag ? 1 : 0);
			if (flag)
			{
				array[0] = vector;
			}
			for (int i = 0; i < num; i++)
			{
				array[i + num3] = endValue.wps[i];
			}
			if (flag2)
			{
				array[array.Length - 1] = array[0];
			}
			endValue.wps = array;
			endValue.addedExtraStartWp = flag;
			endValue.addedExtraEndWp = flag2;
			endValue.FinalizePath(t.plugOptions.isClosedPath, t.plugOptions.lockPositionAxis, vector);
			t.plugOptions.startupRot = transform.rotation;
			t.plugOptions.startupZRot = transform.eulerAngles.z;
			t.changeValue = t.endValue;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public override float GetSpeedBasedDuration(PathOptions options, float unitsXSecond, Path changeValue)
		{
			return changeValue.length / unitsXSecond;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B1CC File Offset: 0x000093CC
		public override void EvaluateAndApply(PathOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Path startValue, Path changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental && !options.isClosedPath)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				if (num > 0)
				{
					changeValue = changeValue.CloneIncremental(num);
				}
			}
			float num2 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num3 = changeValue.ConvertToConstantPathPerc(num2);
			Vector3 point = changeValue.GetPoint(num3, false);
			changeValue.targetPosition = point;
			setter(point);
			if (options.mode != PathMode.Ignore && options.orientType != OrientType.None)
			{
				this.SetOrientation(options, t, changeValue, num3, point, updateNotice);
			}
			bool flag = !usingInversePosition;
			if (t.isBackwards)
			{
				flag = !flag;
			}
			int waypointIndexFromPerc = changeValue.GetWaypointIndexFromPerc(num2, flag);
			if (waypointIndexFromPerc != t.miscInt)
			{
				int miscInt = t.miscInt;
				t.miscInt = waypointIndexFromPerc;
				if (t.onWaypointChange != null)
				{
					bool flag2 = t.isBackwards;
					if (t.hasLoops && t.loopType == LoopType.Yoyo)
					{
						flag2 = (!t.isBackwards && t.completedLoops % 2 != 0) || (t.isBackwards && t.completedLoops % 2 == 0);
					}
					if (flag2)
					{
						for (int i = miscInt - 1; i > waypointIndexFromPerc - 1; i--)
						{
							if (i != waypointIndexFromPerc)
							{
								Tween.OnTweenCallback<int>(t.onWaypointChange, t, i);
							}
						}
					}
					else
					{
						for (int j = miscInt + 1; j < waypointIndexFromPerc; j++)
						{
							if (j != waypointIndexFromPerc)
							{
								Tween.OnTweenCallback<int>(t.onWaypointChange, t, j);
							}
						}
					}
					if (newCompletedSteps > 0 && !t.isComplete)
					{
						int num4;
						if (t.loopType == LoopType.Yoyo)
						{
							num4 = ((t.completedLoops % 2 != 0 && !t.isBackwards) ? (changeValue.wps.Length - 1) : 0);
						}
						else
						{
							num4 = ((!t.isBackwards) ? (changeValue.wps.Length - 1) : 0);
						}
						if (num4 != waypointIndexFromPerc)
						{
							Tween.OnTweenCallback<int>(t.onWaypointChange, t, num4);
						}
					}
					Tween.OnTweenCallback<int>(t.onWaypointChange, t, waypointIndexFromPerc);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B3D4 File Offset: 0x000095D4
		public void SetOrientation(PathOptions options, Tween t, Path path, float pathPerc, Vector3 tPos, UpdateNotice updateNotice)
		{
			GameObject gameObject = t.target as GameObject;
			Transform transform = ((gameObject != null) ? gameObject.transform : ((Component)t.target).transform);
			Quaternion quaternion = Quaternion.identity;
			Vector3 position = transform.position;
			if (updateNotice == UpdateNotice.RewindStep)
			{
				transform.rotation = options.startupRot;
			}
			switch (options.orientType)
			{
			case OrientType.ToPath:
			{
				Vector3 vector;
				if (path.type == PathType.Linear && options.lookAhead <= 0.0001f)
				{
					vector = tPos + path.wps[path.linearWPIndex] - path.wps[path.linearWPIndex - 1];
				}
				else
				{
					float num = pathPerc + options.lookAhead;
					if (num > 1f)
					{
						num = (options.isClosedPath ? (num - 1f) : ((path.type == PathType.Linear) ? 1f : 1.00001f));
					}
					vector = path.GetPoint(num, false);
				}
				if (path.type == PathType.Linear)
				{
					Vector3 vector2 = path.wps[path.wps.Length - 1];
					if (vector == vector2)
					{
						vector = ((tPos == vector2) ? (vector2 + (vector2 - path.wps[path.wps.Length - 2])) : vector2);
					}
				}
				Vector3 vector3 = transform.up;
				bool flag = options.parent != null;
				bool flag2 = options.useLocalPosition && flag;
				if (flag2)
				{
					vector = options.parent.TransformPoint(vector);
				}
				if (options.lockRotationAxis != AxisConstraint.None)
				{
					if ((options.lockRotationAxis & AxisConstraint.X) == AxisConstraint.X)
					{
						Vector3 vector4 = transform.InverseTransformPoint(vector);
						vector4.y = 0f;
						vector = transform.TransformPoint(vector4);
						vector3 = (flag2 ? options.parent.up : Vector3.up);
					}
					if ((options.lockRotationAxis & AxisConstraint.Y) == AxisConstraint.Y)
					{
						Vector3 vector5 = transform.InverseTransformPoint(vector);
						if (vector5.z < 0f)
						{
							vector5.z = -vector5.z;
						}
						vector5.x = 0f;
						vector = transform.TransformPoint(vector5);
					}
					if ((options.lockRotationAxis & AxisConstraint.Z) == AxisConstraint.Z)
					{
						if (flag2)
						{
							vector3 = options.parent.TransformDirection(Vector3.up);
						}
						else
						{
							vector3 = transform.TransformDirection(Vector3.up);
						}
						vector3.z = options.startupZRot;
					}
				}
				if (options.mode == PathMode.Full3D)
				{
					Vector3 vector6 = vector - position;
					if (vector6 == Vector3.zero)
					{
						vector6 = transform.forward;
					}
					if (flag)
					{
						vector6 = this.DivideVectorByVector(vector6, options.parent.localScale);
					}
					quaternion = Quaternion.LookRotation(vector6, vector3);
				}
				else
				{
					if (flag)
					{
						Vector3 vector7 = this.DivideVectorByVector(vector - position, options.parent.localScale);
						vector = position + vector7;
					}
					float num2 = 0f;
					float num3 = DOTweenUtils.Angle2D(position, vector);
					if (num3 < 0f)
					{
						num3 = 360f + num3;
					}
					if (options.mode == PathMode.Sidescroller2D)
					{
						num2 = (float)((vector.x < position.x) ? 180 : 0);
						if (num3 > 90f && num3 < 270f)
						{
							num3 = 180f - num3;
						}
					}
					quaternion = Quaternion.Euler(0f, num2, num3);
				}
				break;
			}
			case OrientType.LookAtTransform:
				if (options.lookAtTransform != null)
				{
					path.lookAtPosition = new Vector3?(options.lookAtTransform.position);
					quaternion = Quaternion.LookRotation(options.lookAtTransform.position - position, options.stableZRotation ? Vector3.up : transform.up);
				}
				break;
			case OrientType.LookAtPosition:
				path.lookAtPosition = new Vector3?(options.lookAtPosition);
				quaternion = Quaternion.LookRotation(options.lookAtPosition - position, options.stableZRotation ? Vector3.up : transform.up);
				break;
			}
			if (options.hasCustomForwardDirection)
			{
				quaternion *= options.forward;
			}
			DOTweenExternalCommand.Dispatch_SetOrientationOnPath(options, t, quaternion, transform);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B7DD File Offset: 0x000099DD
		private Vector3 DivideVectorByVector(Vector3 vector, Vector3 byVector)
		{
			return new Vector3(vector.x / byVector.x, vector.y / byVector.y, vector.z / byVector.z);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B80B File Offset: 0x00009A0B
		private Vector3 MultiplyVectorByVector(Vector3 vector, Vector3 byVector)
		{
			return new Vector3(vector.x * byVector.x, vector.y * byVector.y, vector.z * byVector.z);
		}

		// Token: 0x0400015E RID: 350
		public const float MinLookAhead = 0.0001f;
	}
}
