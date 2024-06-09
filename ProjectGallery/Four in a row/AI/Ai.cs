using System;
using Four_in_a_row.Controls;

namespace Four_in_a_row.Ai
{
    public static class Minimax
    {
       /* public static int Evaluate(BoardState state, int depth, int maxDepth, bool maximizingPlayer, int alpha, int beta)
        {
            if (depth == maxDepth || state.IsGameOver())
            {
                return state.Evaluate();
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (var child in state.GetChildren(maximizingPlayer))
                {
                    int eval = Evaluate(child, depth + 1, maxDepth, false, alpha, beta);
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var child in state.GetChildren(!maximizingPlayer))
                {
                    int eval = Evaluate(child, depth + 1, maxDepth, true, alpha, beta);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }

        public static Move BestMove(BoardState state, int maxDepth)
        {
            int bestScore = int.MinValue;
            Move bestMove = null;
            foreach (var move in state.GetPossibleMoves())
            {
                var newState = state.ApplyMove(move);
                int score = Evaluate(newState, 0, maxDepth, false, int.MinValue, int.MaxValue);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
            return bestMove;
        }*/
    }
}