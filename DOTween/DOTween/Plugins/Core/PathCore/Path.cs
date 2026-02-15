using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public class Path
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000FC8D File Offset: 0x0000DE8D
		internal int minInputWaypoints
		{
			get
			{
				return this._decoder.minInputWaypoints;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public Path(PathType type, Vector3[] waypoints, int subdivisionsXSegment, Color? gizmoColor = null)
		{
			this.type = type;
			this.subdivisionsXSegment = subdivisionsXSegment;
			if (gizmoColor != null)
			{
				this.gizmoColor = gizmoColor.Value;
			}
			this.AssignWaypoints(waypoints, true);
			this.AssignDecoder(type);
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(new TweenCallback(this.Draw));
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FD25 File Offset: 0x0000DF25
		internal Path()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FD54 File Offset: 0x0000DF54
		internal void FinalizePath(bool isClosedPath, AxisConstraint lockPositionAxes, Vector3 currTargetVal)
		{
			if (lockPositionAxes != AxisConstraint.None)
			{
				bool flag = (lockPositionAxes & AxisConstraint.X) == AxisConstraint.X;
				bool flag2 = (lockPositionAxes & AxisConstraint.Y) == AxisConstraint.Y;
				bool flag3 = (lockPositionAxes & AxisConstraint.Z) == AxisConstraint.Z;
				for (int i = 0; i < this.wps.Length; i++)
				{
					Vector3 vector = this.wps[i];
					this.wps[i] = new Vector3(flag ? currTargetVal.x : vector.x, flag2 ? currTargetVal.y : vector.y, flag3 ? currTargetVal.z : vector.z);
				}
			}
			this._decoder.FinalizePath(this, this.wps, isClosedPath);
			this.isFinalized = true;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FDFB File Offset: 0x0000DFFB
		internal Vector3 GetPoint(float perc, bool convertToConstantPerc = false)
		{
			if (convertToConstantPerc)
			{
				perc = this.ConvertToConstantPathPerc(perc);
			}
			return this._decoder.GetPoint(perc, this.wps, this, this.controlPoints);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FE24 File Offset: 0x0000E024
		internal float ConvertToConstantPathPerc(float perc)
		{
			if (this.type == PathType.Linear)
			{
				return perc;
			}
			if (perc > 0f && perc < 1f)
			{
				if (this.length <= 0f)
				{
					return perc;
				}
				float num = this.length * perc;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				int num6 = this.lengthsTable.Length;
				int i = 0;
				while (i < num6)
				{
					if (this.lengthsTable[i] > num)
					{
						num4 = this.timesTable[i];
						num5 = this.lengthsTable[i];
						if (i > 0)
						{
							num3 = this.lengthsTable[i - 1];
							break;
						}
						break;
					}
					else
					{
						num2 = this.timesTable[i];
						i++;
					}
				}
				perc = num2 + (num - num3) / (num5 - num3) * (num4 - num2);
			}
			if (perc > 1f)
			{
				perc = 1f;
			}
			else if (perc < 0f)
			{
				perc = 0f;
			}
			return perc;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000FF0C File Offset: 0x0000E10C
		internal int GetWaypointIndexFromPerc(float perc, bool isMovingForward)
		{
			if (perc >= 1f)
			{
				return this.wps.Length - 1;
			}
			if (perc <= 0f)
			{
				return 0;
			}
			float num = this.length * perc;
			float num2 = 0f;
			int i = 0;
			int num3 = this.wpLengths.Length;
			while (i < num3)
			{
				num2 += this.wpLengths[i];
				if (i == num3 - 1)
				{
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				else if (num2 >= num)
				{
					if (num2 <= num)
					{
						return i;
					}
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FF88 File Offset: 0x0000E188
		internal static Vector3[] GetDrawPoints(Path p, int drawSubdivisionsXSegment)
		{
			int num = p.wps.Length;
			if (p.type == PathType.Linear)
			{
				return p.wps;
			}
			int num2 = num * drawSubdivisionsXSegment;
			Vector3[] array = new Vector3[num2 + 1];
			for (int i = 0; i <= num2; i++)
			{
				float num3 = (float)i / (float)num2;
				Vector3 point = p.GetPoint(num3, false);
				array[i] = point;
			}
			return array;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		internal static void RefreshNonLinearDrawWps(Path p)
		{
			int num = p.wps.Length * 10;
			if (p.nonLinearDrawWps == null || p.nonLinearDrawWps.Length != num + 1)
			{
				p.nonLinearDrawWps = new Vector3[num + 1];
			}
			for (int i = 0; i <= num; i++)
			{
				float num2 = (float)i / (float)num;
				Vector3 point = p.GetPoint(num2, false);
				p.nonLinearDrawWps[i] = point;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010048 File Offset: 0x0000E248
		internal void Destroy()
		{
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Remove(new TweenCallback(this.Draw));
			}
			this.wps = null;
			this.wpLengths = (this.timesTable = (this.lengthsTable = null));
			this.nonLinearDrawWps = null;
			this.isFinalized = false;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000100A4 File Offset: 0x0000E2A4
		internal Path CloneIncremental(int loopIncrement)
		{
			if (this._incrementalClone != null)
			{
				if (this._incrementalIndex == loopIncrement)
				{
					return this._incrementalClone;
				}
				this._incrementalClone.Destroy();
			}
			int num = this.wps.Length;
			Vector3 vector = this.wps[num - 1] - this.wps[0];
			Vector3[] array = new Vector3[this.wps.Length];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.wps[i] + vector * (float)loopIncrement;
			}
			int num2 = this.controlPoints.Length;
			ControlPoint[] array2 = new ControlPoint[num2];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = this.controlPoints[j] + vector * (float)loopIncrement;
			}
			Vector3[] array3 = null;
			if (this.nonLinearDrawWps != null)
			{
				int num3 = this.nonLinearDrawWps.Length;
				array3 = new Vector3[num3];
				for (int k = 0; k < num3; k++)
				{
					array3[k] = this.nonLinearDrawWps[k] + vector * (float)loopIncrement;
				}
			}
			this._incrementalClone = new Path();
			this._incrementalIndex = loopIncrement;
			this._incrementalClone.type = this.type;
			this._incrementalClone.subdivisionsXSegment = this.subdivisionsXSegment;
			this._incrementalClone.subdivisions = this.subdivisions;
			this._incrementalClone.wps = array;
			this._incrementalClone.controlPoints = array2;
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(new TweenCallback(this._incrementalClone.Draw));
			}
			this._incrementalClone.length = this.length;
			this._incrementalClone.wpLengths = this.wpLengths;
			this._incrementalClone.timesTable = this.timesTable;
			this._incrementalClone.lengthsTable = this.lengthsTable;
			this._incrementalClone._decoder = this._decoder;
			this._incrementalClone.nonLinearDrawWps = array3;
			this._incrementalClone.targetPosition = this.targetPosition;
			this._incrementalClone.lookAtPosition = this.lookAtPosition;
			this._incrementalClone.isFinalized = true;
			return this._incrementalClone;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000102EC File Offset: 0x0000E4EC
		internal void AssignWaypoints(Vector3[] newWps, bool cloneWps = false)
		{
			if (cloneWps)
			{
				int num = newWps.Length;
				this.wps = new Vector3[num];
				for (int i = 0; i < num; i++)
				{
					this.wps[i] = newWps[i];
				}
				return;
			}
			this.wps = newWps;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010334 File Offset: 0x0000E534
		internal void AssignDecoder(PathType pathType)
		{
			this.type = pathType;
			if (pathType == PathType.Linear)
			{
				if (Path._linearDecoder == null)
				{
					Path._linearDecoder = new LinearDecoder();
				}
				this._decoder = Path._linearDecoder;
				return;
			}
			if (pathType != PathType.CubicBezier)
			{
				if (Path._catmullRomDecoder == null)
				{
					Path._catmullRomDecoder = new CatmullRomDecoder();
				}
				this._decoder = Path._catmullRomDecoder;
				return;
			}
			if (Path._cubicBezierDecoder == null)
			{
				Path._cubicBezierDecoder = new CubicBezierDecoder();
			}
			this._decoder = Path._cubicBezierDecoder;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000103A7 File Offset: 0x0000E5A7
		internal void Draw()
		{
			Path.Draw(this);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000103B0 File Offset: 0x0000E5B0
		private static void Draw(Path p)
		{
			if (p.timesTable == null)
			{
				return;
			}
			Color color = p.gizmoColor;
			color.a *= 0.5f;
			Gizmos.color = p.gizmoColor;
			int num = p.wps.Length;
			if (p._changed || (p.type != PathType.Linear && p.nonLinearDrawWps == null))
			{
				p._changed = false;
				if (p.type != PathType.Linear)
				{
					Path.RefreshNonLinearDrawWps(p);
				}
			}
			if (p.type == PathType.Linear)
			{
				Vector3 vector = Path.ConvertToDrawPoint(p.wps[0], p.plugOptions);
				for (int i = 0; i < num; i++)
				{
					Vector3 vector2 = Path.ConvertToDrawPoint(p.wps[i], p.plugOptions);
					Gizmos.DrawLine(vector2, vector);
					vector = vector2;
				}
			}
			else
			{
				Vector3 vector = Path.ConvertToDrawPoint(p.nonLinearDrawWps[0], p.plugOptions);
				int num2 = p.nonLinearDrawWps.Length;
				for (int j = 1; j < num2; j++)
				{
					Vector3 vector3 = Path.ConvertToDrawPoint(p.nonLinearDrawWps[j], p.plugOptions);
					Gizmos.DrawLine(vector3, vector);
					vector = vector3;
				}
			}
			Gizmos.color = color;
			for (int k = 0; k < num; k++)
			{
				Gizmos.DrawSphere(Path.ConvertToDrawPoint(p.wps[k], p.plugOptions), 0.075f);
			}
			if (p.lookAtPosition != null)
			{
				Vector3 value = p.lookAtPosition.Value;
				Gizmos.DrawLine(p.targetPosition, value);
				Gizmos.DrawWireSphere(value, 0.075f);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001052C File Offset: 0x0000E72C
		private static Vector3 ConvertToDrawPoint(Vector3 wp, PathOptions plugOptions)
		{
			if (!plugOptions.useLocalPosition || plugOptions.parent == null)
			{
				return wp;
			}
			return plugOptions.parent.TransformPoint(wp);
		}

		// Token: 0x040001AA RID: 426
		private static CatmullRomDecoder _catmullRomDecoder;

		// Token: 0x040001AB RID: 427
		private static LinearDecoder _linearDecoder;

		// Token: 0x040001AC RID: 428
		private static CubicBezierDecoder _cubicBezierDecoder;

		// Token: 0x040001AD RID: 429
		public float[] wpLengths;

		// Token: 0x040001AE RID: 430
		[SerializeField]
		public Vector3[] wps;

		// Token: 0x040001AF RID: 431
		[SerializeField]
		internal PathType type;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		internal int subdivisionsXSegment;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		internal int subdivisions;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		internal ControlPoint[] controlPoints;

		// Token: 0x040001B3 RID: 435
		[SerializeField]
		internal float length;

		// Token: 0x040001B4 RID: 436
		[SerializeField]
		internal bool isFinalized;

		// Token: 0x040001B5 RID: 437
		[SerializeField]
		internal float[] timesTable;

		// Token: 0x040001B6 RID: 438
		[SerializeField]
		internal float[] lengthsTable;

		// Token: 0x040001B7 RID: 439
		internal int linearWPIndex = -1;

		// Token: 0x040001B8 RID: 440
		internal bool addedExtraStartWp;

		// Token: 0x040001B9 RID: 441
		internal bool addedExtraEndWp;

		// Token: 0x040001BA RID: 442
		internal PathOptions plugOptions;

		// Token: 0x040001BB RID: 443
		private Path _incrementalClone;

		// Token: 0x040001BC RID: 444
		private int _incrementalIndex;

		// Token: 0x040001BD RID: 445
		private ABSPathDecoder _decoder;

		// Token: 0x040001BE RID: 446
		private bool _changed;

		// Token: 0x040001BF RID: 447
		internal Vector3[] nonLinearDrawWps;

		// Token: 0x040001C0 RID: 448
		internal Vector3 targetPosition;

		// Token: 0x040001C1 RID: 449
		internal Vector3? lookAtPosition;

		// Token: 0x040001C2 RID: 450
		internal Color gizmoColor = new Color(1f, 1f, 1f, 0.7f);
	}
}
