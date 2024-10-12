using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    private void Start()
    {
         anim = GetComponent<Animator>(); 
    }
    public void loadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }
    
    IEnumerator Transition(string sceneName)
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
