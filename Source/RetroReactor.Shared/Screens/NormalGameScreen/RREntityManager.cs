using MonoSAMFramework.Portable.GameMath.Geometry;
using MonoSAMFramework.Portable.Input;
using MonoSAMFramework.Portable.Screens;
using MonoSAMFramework.Portable.Screens.Entities;

namespace RetroReactor.Shared.Screens.NormalGameScreen
{
	class RREntityManager : EntityManager
	{
		public RREntityManager(RRGameScreen screen) : base(screen)
		{
		}

		public override void DrawOuterDebug()
		{
			//
		}

		protected override FRectangle RecalculateBoundingBox()
		{
			return Owner.VAdapterGame.VirtualTotalBoundingBox;
		}

		protected override void OnBeforeUpdate(SAMTime gameTime, InputState state)
		{
			//
		}

		protected override void OnAfterUpdate(SAMTime gameTime, InputState state)
		{
			//
		}
	}
}
