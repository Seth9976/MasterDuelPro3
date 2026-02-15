using System;
using System.Collections.Generic;

namespace YgomSystem.Utility
{
	// Token: 0x0200054A RID: 1354
	public class TaskManager
	{
		// Token: 0x06002B23 RID: 11043 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GlobalPause(bool enable)
		{
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x00002739 File Offset: 0x00000939
		public TaskManager(int reserveCategoryNum = 10, int reserveTaskNum = 50, int executeTaskLimit = 1000)
		{
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Execute(int nCategory, bool bClear, float limitTime = -1f)
		{
			return false;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetExecuteIndex()
		{
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pause(TaskManager.ID tId, bool enable)
		{
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0000216D File Offset: 0x0000036D
		public void PauseCategory(int nCategory, bool enable)
		{
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0000216D File Offset: 0x0000036D
		public void PauseAll(bool enable)
		{
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID Add(int nCategory, TaskManager.Task tTask, object tParam = null, bool startTimer = false)
		{
			return null;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID AddNext(int nCategory, TaskManager.Task tTask, object tParam = null, bool startTimer = false)
		{
			return null;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID Insert(int nCategory, int nIndex, TaskManager.Task tTask, object tParam = null, bool startTimer = false)
		{
			return null;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID InsertNext(int nCategory, int nIndex, TaskManager.Task tTask, object tParam = null, bool startTimer = false)
		{
			return null;
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Remove(TaskManager.ID tId)
		{
			return false;
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RemoveAt(int nCategory, int nIndex)
		{
			return false;
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RemoveCategory(int nCategory)
		{
			return false;
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RemoveCategoryTask(int nCategory, TaskManager.Task tTask)
		{
			return false;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RemoveAll()
		{
			return false;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetLocalData(TaskManager.ID tId, object data)
		{
			return false;
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetTimeSpeed(TaskManager.ID tId, float speedRatio)
		{
			return false;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetTimeSpeed(int nCategory, float speedRatio)
		{
			return false;
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetParam(TaskManager.ID tId)
		{
			return null;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0000216A File Offset: 0x0000036A
		public object GetLocalData(TaskManager.ID tId)
		{
			return null;
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetTaskTime(TaskManager.ID tId)
		{
			return 0f;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTaskExecuteNum(TaskManager.ID tId)
		{
			return 0;
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID GetTaskId(int nCategory, TaskManager.Task tTask, int nNum)
		{
			return null;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0000216A File Offset: 0x0000036A
		public TaskManager.ID GetTaskIndexId(int nCategory, int nIndex)
		{
			return null;
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTaskIndex(TaskManager.ID tId)
		{
			return 0;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTaskName(TaskManager.ID tId)
		{
			return null;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTaskName(int nCategory, int nIndex = 1)
		{
			return null;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDebugInfo(int nCategory)
		{
			return null;
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTaskNum(int nCategory)
		{
			return 0;
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNextTaskNum(int nCategory)
		{
			return 0;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEmpty(int nCategory)
		{
			return false;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTaskType(int nCategory, TaskManager.Task tTask)
		{
			return false;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTaskType(int nCategory, int index, TaskManager.Task tTask)
		{
			return false;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCategoryPauseFlag(int nCategory, bool enable)
		{
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCategoryTimeSpeed(int nCategory, float speedRatio)
		{
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0000216A File Offset: 0x0000036A
		private TaskManager.TaskCategoryData GetTaskCategoryData(int nCategory)
		{
			return null;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0000216A File Offset: 0x0000036A
		private TaskManager.ID AddTaskData(bool bAddNextTaskQue, int nCategory, TaskManager.Task tTask, object tParam, bool startTimer)
		{
			return null;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0000216A File Offset: 0x0000036A
		private TaskManager.ID InsertTaskData(bool bAddNextTaskQue, int nCategory, int nIndex, TaskManager.Task tTask, object tParam, bool startTimer)
		{
			return null;
		}

		// Token: 0x04002A1D RID: 10781
		private static bool m_globalPause;

		// Token: 0x04002A1E RID: 10782
		private Dictionary<int, TaskManager.TaskCategoryData> m_tblTaskContainer;

		// Token: 0x04002A1F RID: 10783
		private TaskManager.TaskPool m_taskPool;

		// Token: 0x04002A20 RID: 10784
		private Dictionary<int, bool> m_pauseCategoryList;

		// Token: 0x04002A21 RID: 10785
		private Dictionary<int, float> m_timeSpeedCategoryList;

		// Token: 0x04002A22 RID: 10786
		private int m_executeTaskLimit;

		// Token: 0x04002A23 RID: 10787
		private bool m_executeTaskQueIndexReset;

		// Token: 0x0200054B RID: 1355
		// (Invoke) Token: 0x06002B4C RID: 11084
		public delegate bool Task(TaskManager.ID tThis, int fExecNum, float fExecSec, object tParam);

		// Token: 0x0200054C RID: 1356
		public class ID
		{
			// Token: 0x06002B4F RID: 11087 RVA: 0x00002739 File Offset: 0x00000939
			protected ID()
			{
			}

			// Token: 0x06002B50 RID: 11088 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCategory()
			{
				return 0;
			}

			// Token: 0x06002B51 RID: 11089 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsRemoved()
			{
				return false;
			}

			// Token: 0x04002A24 RID: 10788
			protected object m_tId;

			// Token: 0x04002A25 RID: 10789
			protected int m_nCategory;
		}

		// Token: 0x0200054D RID: 1357
		private class HighLevelID : TaskManager.ID
		{
			// Token: 0x06002B52 RID: 11090 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reset(int nCategory, TaskManager.TaskData tTaskData)
			{
			}

			// Token: 0x06002B53 RID: 11091 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}
		}

		// Token: 0x0200054E RID: 1358
		private class TaskData
		{
			// Token: 0x06002B55 RID: 11093 RVA: 0x00002739 File Offset: 0x00000939
			public TaskData()
			{
			}

			// Token: 0x06002B56 RID: 11094 RVA: 0x00002739 File Offset: 0x00000939
			public TaskData(int nCategory, TaskManager.Task tTaskVal, object tParamVal)
			{
			}

			// Token: 0x06002B57 RID: 11095 RVA: 0x0000216D File Offset: 0x0000036D
			public void Terminate()
			{
			}

			// Token: 0x06002B58 RID: 11096 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reset(int nCategory, TaskManager.Task tTaskVal, object tParamVal)
			{
			}

			// Token: 0x06002B59 RID: 11097 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool ExecuteTask()
			{
				return false;
			}

			// Token: 0x06002B5A RID: 11098 RVA: 0x0000216D File Offset: 0x0000036D
			public void Start()
			{
			}

			// Token: 0x06002B5B RID: 11099 RVA: 0x0000216D File Offset: 0x0000036D
			public void Pause(bool enable)
			{
			}

			// Token: 0x06002B5C RID: 11100 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetDeltaTimer()
			{
			}

			// Token: 0x06002B5D RID: 11101 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTimeSpeed(float speedRatio)
			{
			}

			// Token: 0x06002B5E RID: 11102 RVA: 0x0000216D File Offset: 0x0000036D
			public void Remove()
			{
			}

			// Token: 0x06002B5F RID: 11103 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetLocalData(object data)
			{
			}

			// Token: 0x06002B60 RID: 11104 RVA: 0x0000216A File Offset: 0x0000036A
			public object GetLocalData()
			{
				return null;
			}

			// Token: 0x06002B61 RID: 11105 RVA: 0x0000216A File Offset: 0x0000036A
			public object GetParam()
			{
				return null;
			}

			// Token: 0x06002B62 RID: 11106 RVA: 0x0000216A File Offset: 0x0000036A
			public TaskManager.ID GetId()
			{
				return null;
			}

			// Token: 0x06002B63 RID: 11107 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetTaskName()
			{
				return null;
			}

			// Token: 0x06002B64 RID: 11108 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetNowTime()
			{
				return 0f;
			}

			// Token: 0x06002B65 RID: 11109 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetExecNum()
			{
				return 0;
			}

			// Token: 0x06002B66 RID: 11110 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool CheckId(TaskManager.ID tID)
			{
				return false;
			}

			// Token: 0x06002B67 RID: 11111 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool CheckTask(TaskManager.Task tTask)
			{
				return false;
			}

			// Token: 0x06002B68 RID: 11112 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsTerminate()
			{
				return false;
			}

			// Token: 0x06002B69 RID: 11113 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsRemoved()
			{
				return false;
			}

			// Token: 0x06002B6A RID: 11114 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsPause()
			{
				return false;
			}

			// Token: 0x04002A26 RID: 10790
			private TaskManager.Task m_tTask;

			// Token: 0x04002A27 RID: 10791
			private object m_tParam;

			// Token: 0x04002A28 RID: 10792
			private object m_tLocalData;

			// Token: 0x04002A29 RID: 10793
			private TaskManager.HighLevelID m_tId;

			// Token: 0x04002A2A RID: 10794
			private int m_nExecNum;

			// Token: 0x04002A2B RID: 10795
			private bool m_pause;

			// Token: 0x04002A2C RID: 10796
			private float m_nowSec;

			// Token: 0x04002A2D RID: 10797
			private float m_prevSec;

			// Token: 0x04002A2E RID: 10798
			private float m_timeSpeed;
		}

		// Token: 0x0200054F RID: 1359
		private class TaskCategoryData
		{
			// Token: 0x06002B6B RID: 11115 RVA: 0x00002739 File Offset: 0x00000939
			public TaskCategoryData(int categoryId)
			{
			}

			// Token: 0x06002B6C RID: 11116 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Remove(TaskManager.ID tId, bool isReserve = false)
			{
				return false;
			}

			// Token: 0x06002B6D RID: 11117 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool RemoveAt(int nIndex, bool isReserve = false)
			{
				return false;
			}

			// Token: 0x06002B6E RID: 11118 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveTask(TaskManager.Task tTask, bool isReserve = false)
			{
			}

			// Token: 0x06002B6F RID: 11119 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveAll(bool isReserve = false)
			{
			}

			// Token: 0x06002B70 RID: 11120 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportNextTask()
			{
			}

			// Token: 0x06002B71 RID: 11121 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearRemoveTask()
			{
			}

			// Token: 0x06002B72 RID: 11122 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTaskTimeSpeed(float speedRatio)
			{
			}

			// Token: 0x06002B73 RID: 11123 RVA: 0x0000216D File Offset: 0x0000036D
			public void Pause(bool enable)
			{
			}

			// Token: 0x06002B74 RID: 11124 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetDeltaTimer()
			{
			}

			// Token: 0x06002B75 RID: 11125 RVA: 0x0000216A File Offset: 0x0000036A
			public List<TaskManager.TaskData> GetTaskQue()
			{
				return null;
			}

			// Token: 0x06002B76 RID: 11126 RVA: 0x0000216A File Offset: 0x0000036A
			public List<TaskManager.TaskData> GetNextTaskQue()
			{
				return null;
			}

			// Token: 0x06002B77 RID: 11127 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetTaskIndex(TaskManager.ID tId)
			{
				return 0;
			}

			// Token: 0x06002B78 RID: 11128 RVA: 0x0000216A File Offset: 0x0000036A
			public TaskManager.TaskData GetTaskData(TaskManager.ID tId)
			{
				return null;
			}

			// Token: 0x06002B79 RID: 11129 RVA: 0x0000216A File Offset: 0x0000036A
			public List<TaskManager.TaskData> GetTaskTypeDataList(TaskManager.Task tTask)
			{
				return null;
			}

			// Token: 0x06002B7A RID: 11130 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCategoryId()
			{
				return 0;
			}

			// Token: 0x06002B7B RID: 11131 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetTaskCount()
			{
				return 0;
			}

			// Token: 0x06002B7C RID: 11132 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsTaskType(TaskManager.Task tTask)
			{
				return false;
			}

			// Token: 0x06002B7D RID: 11133 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsTaskType(TaskManager.Task tTask, int index)
			{
				return false;
			}

			// Token: 0x06002B7E RID: 11134 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDebugString(List<TaskManager.TaskData> tTaskQue)
			{
				return null;
			}

			// Token: 0x06002B7F RID: 11135 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool CheckQueIndexEnable(int nIndex)
			{
				return false;
			}

			// Token: 0x04002A2F RID: 10799
			private List<TaskManager.TaskData> m_lstTaskQue;

			// Token: 0x04002A30 RID: 10800
			private List<TaskManager.TaskData> m_lstNextTaskQue;

			// Token: 0x04002A31 RID: 10801
			private int m_nCategoryId;
		}

		// Token: 0x02000550 RID: 1360
		private class TaskPool
		{
			// Token: 0x06002B80 RID: 11136 RVA: 0x00002739 File Offset: 0x00000939
			public TaskPool(int capacity)
			{
			}

			// Token: 0x06002B81 RID: 11137 RVA: 0x0000216A File Offset: 0x0000036A
			public TaskManager.TaskData New()
			{
				return null;
			}

			// Token: 0x06002B82 RID: 11138 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddReserve(int num)
			{
			}

			// Token: 0x04002A32 RID: 10802
			private List<TaskManager.TaskData> m_pool;

			// Token: 0x04002A33 RID: 10803
			private int m_nowIndex;
		}
	}
}
