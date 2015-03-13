using Libraries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match.Gem;

namespace Match {

	public class CSearcher : CFieldHandler {

		public struct Move {
			public CIcon from, to;
		}

		private List<Move> moves;

		public CSearcher (CField controller) : base(controller) {}

		public CSearcher FindMoves() {
			moves = new List<Move>();

			for (int i = 0; i < GetSquare(); i++) {
				if (!IsOutOfHorisontalRange(i)) {
					SearchAllMoves(i, i+1, i+2);
				}

				if (!IsOutOfVeticalRange(i)) {
					SearchAllMoves(i, i+field.width, i+field.width*2);
				}
			}

			return this;
		}

		protected void SearchAllMoves (int firstIndex, int secondIndex, int thirdIndex) {
			CIcon first  = GetIconByIndex(firstIndex);
			CIcon second = GetIconByIndex(secondIndex);
			CIcon third  = GetIconByIndex(thirdIndex);

			SearchTargetMoves(first, second, third);
			SearchTargetMoves(third, first, second);
			SearchTargetMoves(second, third, first);
		}

		protected void SearchTargetMoves (CIcon first, CIcon second, CIcon target) {
			if (first.color != second.color) return;

			foreach (CIcon cell in GetNeighbours(target)) {
				if (cell != first && cell != second && cell.color == first.color) {
					moves.Add(new Move(){
						from = target,
						to   = cell
					});
				}
			}
		}

		public bool MovesExists () {
			return GetMoves().Count > 0;
		}

		public List<Move> GetMoves() {
			return moves;
		}

	}
}