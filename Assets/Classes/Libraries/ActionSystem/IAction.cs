using UnityEngine;
using System.Collections;

namespace Libraries.ActionSystem {
	public interface IAction {
		bool Validation();

		void SetActionManager(CManager manager);
		void SetAnticipant(IAnticipant anticipant);
		void StartAction();

		int GetIndex();
	}
}