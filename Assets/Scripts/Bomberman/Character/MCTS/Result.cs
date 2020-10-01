namespace Bomberman.Character.MCTS
{
	public class Result
	{
		public int Wins { get; set; }
		public int Games { get; set; }

		public float WinRate => (float)Games > 0 ? Wins / Games : 0;

		public Result(int wins, int games)
		{
			Wins = wins;
			Games = games;
		}
		
		public Result()
		{
			Wins = 0;
			Games = 0;
		}

		public static Result operator +(Result left, Result right)
		{
			return new Result(
				left.Wins + right.Wins,
				left.Games + right.Games
			);
		}
	}
}