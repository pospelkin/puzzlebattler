using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Libraries {

	public class CWeightedUnion {

		private class CBranch {
			public int mIndex;
			public int mSize = 1;
			public CBranch mParent;

			public CBranch(int index) {
				mIndex  = index;
				mParent = this;
			}

			public bool IsRoot () {
				return this == mParent;
			}
		}

		private bool mHasUnions = false;

		private CBranch[] mBranches;

		public CWeightedUnion(int elementsCount) {
			mBranches = new CBranch[elementsCount];

			for (int i = 0; i < mBranches.Length; i++) {
				mBranches[i] = new CBranch(i);
			}
		}

		public bool HasUnions() {
			return mHasUnions;
		}

		private CBranch FindRoot(CBranch aBranch) {
			while (!aBranch.IsRoot()) {
				aBranch = aBranch.mParent;
			}

			return aBranch;
		}

		public void unite(int aFirst, int aSecond) {
			CBranch fRoot = FindRoot( mBranches[aFirst] );
			CBranch sRoot = FindRoot( mBranches[aSecond] );

			if (fRoot.mIndex == sRoot.mIndex) return;

			sRoot.mParent = fRoot;
			fRoot.mSize += sRoot.mSize;

			mHasUnions = true;
		}

		public List<List<int>> GetTrees() {
			if (!HasUnions()) {
				return new List<List<int>>();
			}

			var mTrees = new Dictionary<int, List<int>>();

			foreach (CBranch branch in mBranches) {
				if (branch.IsRoot()) continue;

				int rootIndex = FindRoot(branch).mIndex;

				List<int> existsTree;

				if (mTrees.ContainsKey(rootIndex)) {
					existsTree = mTrees[rootIndex];
				} else {
					existsTree = new List<int>();
					existsTree.Add(rootIndex);
					mTrees.Add(rootIndex, existsTree);
				}

				mTrees[rootIndex].Add(branch.mIndex);
			}

			return new List<List<int>>(mTrees.Values);
		}

	}

}