using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransfer : MonoBehaviour {
    public static SceneTransfer instance;
    [HideInInspector] public int doorId;
    [HideInInspector] public bool loaded;
    
    private int _savedHealth;
    private bool _savedHammerActive;
    private int _savedCogs;

    private void Awake() {
        if (instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Setup(Vector3 spawnPos) {
        if (loaded) return;
        
        Debug.Log($"Spawning @ door {doorId}");
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null) {
            Debug.LogWarning("No player found");
            return;
        }
        player.GetComponent<PlayerStats>().SetHealth(_savedHealth);
        player.GetComponent<GearManager>().hammerActiveOverridden = true;
        player.GetComponent<GearManager>().ToggleHammer(_savedHammerActive);
        player.position = spawnPos;
        UIManager.instance.syringeUI.AddCogs(_savedCogs);
        loaded = true;
    }

    public void WritePlayerState(int health, bool hammerActive, int cogs) {
        _savedHealth = health;
        _savedHammerActive = hammerActive;
        _savedCogs = cogs;
    }
}