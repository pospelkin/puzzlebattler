using Libraries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match.Gem;

namespace Match {

	public class CMatcher : CFieldHandler {

		public CMatcher (CField field) : base(field) {}

		private List<List<CIcon>> trees;

		private List<List<int>> FindMatchesIndexes() {
			var union = new CWeightedUnion(GetSquare());

			for (int i = 0; i < GetSquare(); i++) {
				if (IsHorizontalMatch(i)) {
					union.unite(i, i + 1);
					union.unite(i, i + 2);
				}

				if (IsVerticalMatch(i)) {
					union.unite(i, i + field.width);
					union.unite(i, i + field.width*2);
				}
			}

			return union.GetTrees();
		}

		public CMatcher FindMatches() {
			var matches = FindMatchesIndexes();
			trees = new List<List<CIcon>>();

			foreach (List<int> match in matches) {
				var tree = new List<CIcon>();

				foreach (int index in match) {
					tree.Add( GetIconByIndex(index) );
				}

				trees.Add(tree);
			}

			return this;
		}

		protected bool IsHorizontalMatch (int index) {
			if (IsOutOfHorisontalRange(index)) {
				return false;
			} else {
				return IsMatch(index, index + 1, index + 2);
			}
		}

		protected bool IsVerticalMatch (int index) {
			if (IsOutOfVeticalRange(index)) {
				return false;
			} else {
				return IsMatch(index, index + field.width*1, index + field.width*2);
			}
		}

		public bool MatchesExists () {
			return GetMatches().Count > 0;
		}

		public List<List<CIcon>> GetMatches() {
			return trees;
		}

	}
}