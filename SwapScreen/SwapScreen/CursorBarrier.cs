using System;
using System.Collections.Generic;
using System.Text;

namespace SwapScreen
{
	/// <summary>
	/// Represents a barrier for cursor movement in one direction and one dimension.
	/// So you would need 4 to constrain to a screen.
	/// 
	/// TODO: is it worth making this abstarct?
	/// </summary>
	class CursorBarrier
	{
		protected bool active;
		protected int limit;
		protected int minForce;
		protected int totalForce;

		/// <summary>
		/// Constructs the barrier.
		/// </summary>
		/// <param name="active">Indicates if the barrier is active.  If false, the other parameters are ignored.</param>
		/// <param name="limit">This is the upper limit which we try to keep the cursor above. 
		/// Note this value is inclusive so the cursor is allowed to reach this value, but no higher.</param>
		/// <param name="minForce">This is the amount of force required to break through the barrier.
		/// If this is Int32.MaxValue then the barrier is solid and no amount of movement can break through it.
		/// Otherwise it represents the number of extra screen pixels the cursor has to move before
		/// we allow the cursor to break through the barrier.</param>
		public CursorBarrier(bool active, int limit, int minForce)
		{
			ChangeBarrier(active, limit, minForce);
		}

		/// <summary>
		/// Chnages the barrier values without the need to re-allocate a new barrier
		/// </summary>
		/// <param name="active">Indicates if the barrier is active.  If false, the other parameters are ignored.</param>
		/// <param name="limit">This is the lower limit which we try to keep the cursor above. 
		/// Note this value is inclusive so the cursor is allowed to reach this value, but no lower.</param>
		/// <param name="minForce">This is the amount of force required to break through the barrier.
		/// If this is Int32.MaxValue then the barrier is solid and no amount of movement can break through it.
		/// Otherwise it represents the number of extra screen pixels the cursor has to move before
		/// we allow the cursor to break through the barrier.</param>
		public void ChangeBarrier(bool active, int limit, int minForce)
		{
			this.active = active;
			this.limit = limit;
			this.minForce = minForce;
			this.totalForce = 0;
		}
	}
}
