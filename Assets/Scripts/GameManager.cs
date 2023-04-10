using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace HumanGunCase.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager instance=null;
        private void Awake()
        {
            if (instance == null) instance = this;
           
        }
        public bool isGameStarted = false;
        public bool isFinished = false;
        public Animator playerAnimator;

        #endregion
        public void StartGame(Animator playerAnimator)
        {
            UIManager.instance.PanelController(false, true, false, false);
           
            playerAnimator.SetTrigger("isRun");
           
        }
        public void StarButton()
        {
            isGameStarted = true;
            GameManager.instance.StartGame(playerAnimator);

        }

        public void RetryLevel()
        {
          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

