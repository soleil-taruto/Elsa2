using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons.Options;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	/// <summary>
	/// 自弾(プレイヤーの弾)
	/// </summary>
	public abstract class Shot
	{
		public double X;
		public double Y;
		public bool FacingLeft;
		public int AttackPoint;
		public bool 壁をすり抜ける;
		public bool 敵を貫通する;

		public Shot(double x, double y, bool facingLeft, int attackPoint, bool 壁をすり抜ける, bool 敵を貫通する)
		{
			this.X = x;
			this.Y = y;
			this.FacingLeft = facingLeft;
			this.AttackPoint = attackPoint;
			this.壁をすり抜ける = 壁をすり抜ける;
			this.敵を貫通する = 敵を貫通する;
		}

		/// <summary>
		/// この自弾を消滅させるか
		/// 敵に当たった場合、画面外に出た場合などこの自弾を消滅させたい場合 true をセットすること。
		/// これにより「フレームの最後に」自弾リストから除去される。
		/// </summary>
		public bool DeadFlag = false;

		/// <summary>
		/// 現在のフレームにおける当たり判定を保持する。
		/// -- Draw によって設定される。
		/// </summary>
		public DDCrash Crash = DDCrashUtils.None();

		private Func<bool> _draw = null;

		public void Draw()
		{
			if (_draw == null)
				_draw = SCommon.Supplier(this.E_Draw());

			if (!_draw())
				this.DeadFlag = true;
		}

		/// <summary>
		/// 現在のフレームにおける描画を行う。
		/// するべきこと：
		/// -- 行動
		/// -- 描画
		/// -- Crash を設定する。
		/// -- 必要に応じて DeadFlag を設定する。
		/// </summary>
		/// <returns>列挙：この自弾は生存しているか</returns>
		protected abstract IEnumerable<bool> E_Draw();

		public void Kill()
		{
			if (!this.DeadFlag)
			{
				this.DeadFlag = true;
				this.Killed();
			}
		}

		/// <summary>
		/// 何かと衝突して消滅した。
		/// </summary>
		protected virtual void Killed()
		{
			DDGround.EL.Add(SCommon.Supplier(Effects.テスト用_小爆発(this.X, this.Y)));
		}
	}
}
