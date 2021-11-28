using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float respawnDelay;
    private CharacterControl gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Allows access to variables inside CharacterControl script
        gamePlayer = FindObjectOfType<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Respawn function calls on respawn Coroutine
    /// </summary>
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    /// <summary>
    /// Respwans enemy at most recent checkpoint on a specified time delay
    /// </summary>
    private IEnumerator RespawnCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);
        yield
        return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.gameObject.SetActive(true);
    }
}