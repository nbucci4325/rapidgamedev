using UnityEngine;

public class _levelManager : MonoBehaviour
{
    //#region Lazy Singleton
    //private static _levelManager instance;
    //public static _levelManager Instance => instance;

    //private void Awake()
    //{
    //    instance = this;
    //}
    //#endregion

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Activated");
            _gameManager.Instance.levelComplete();
        }
    }
}