using UnityEngine;

public class _levelManager : MonoBehaviour
{
    #region Lazy Singleton
    private static _levelManager instance;
    public static _levelManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        //reference mark video which explains declaring the player in the GameManager then referencing it here
    }
}