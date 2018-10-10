using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XBOXParty;

namespace TemplateGame
{
    public class TemplateGameManager : MonoBehaviour
    {
        private List<int> _positions;

        void GetGlobalGameManagerInstance()
        {
            GlobalGameManager instance = GlobalGameManager.Instance;
        }

        private void Awake()
        {
            _positions = new List<int>();

            //If we don't change this player 1 will always be first, player 2 2nd and so on...
            int playerCount = GlobalGameManager.Instance.PlayerCount;
            for (int i = 0; i < playerCount; ++i)
            {
                _positions.Add(i);
            }

            //Bind button
            InputManager.Instance.BindButton("TemplateGame_Submit", 0, ControllerButtonCode.A, ButtonState.OnPress);
			InputManager.Instance.BindButton("TemplateGame_Submit", KeyCode.A, ButtonState.OnPress);
        }

        

        public void Update()
        {
            if (InputManager.Instance.GetButton("TemplateGame_Submit"))
            {
                Submit();
            }
        }

        public void Submit()
        {
            GlobalGameManager.Instance.SubmitGameResults(_positions);
        }
    }
}
