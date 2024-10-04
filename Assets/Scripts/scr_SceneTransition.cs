using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_Scene : MonoBehaviour
{
    [SerializeField] int NextStage;

    public void NextScenen() {
        SceneManager.LoadScene("scn_Stage1");
    }

    public void SetScene() {

        SceneManager.LoadScene(NextStage);
    }
}
