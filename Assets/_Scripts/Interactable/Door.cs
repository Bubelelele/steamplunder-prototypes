using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

    [SerializeField] private bool doorActive = true;
    [SerializeField] private int doorId;
    [SerializeField] private Vector3 positionToPutPlayer;
    [SerializeField] private int sceneBuildIndex;
    [SerializeField] private string descriptionText;

    private void Start() {
        if (SceneTransfer.instance != null)
            if (SceneTransfer.instance.doorId == doorId) 
                SceneTransfer.instance.Setup(positionToPutPlayer);
    }

    public void Interact() {
        if (!doorActive) return;
        Debug.Log($"Loading Scene: {sceneBuildIndex}");
        GameManager.instance.player.GetComponent<PlayerStats>().SavePlayerState();
        SceneTransfer.instance.doorId = doorId;
        SceneTransfer.instance.loaded = false;
        GameManager.instance.LoadScene(sceneBuildIndex);
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        return descriptionText;
    }
}